// src/components/cart/shoppingcart.jsx

import React, { useEffect, useState, useRef } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { fetchDiamondById, fetchRingById, fetchPendantById } from '../../../javascript/apiService';
import { getCartItems, addToCart, removeFromCart, clearCart } from '../../../javascript/cartService';

import CheckoutBox from './checkoutbox';
import SnackbarCart from './SnackbarCart';

import styles from '../css/shoppingcart.module.css';

const ShoppingCart = () => {
    const navigate = useNavigate();

    const location = useLocation();
    const params = new URLSearchParams(location.search);

    // Diamond ID for loose diamond
    const diamondId = params.get('diamondId');

    // Diamond ID, Ring ID, and Pendant ID for pairing
    const diamondIdPairProduct = params.get('d');
    const ringId = params.get('r');
    const pendantId = params.get('p');

    // Cart state
    const [cart, setCart] = useState([]);

    // Error state
    const [error, setError] = useState(null);

    // Snackbar state
    const [snackbarOpen, setSnackbarOpen] = useState({
        open: false,
        id: Date.now(),
    });
    const [snackbarMessage, setSnackbarMessage] = useState('');
    const [snackbarSeverity, setSnackbarSeverity] = useState('success');

    const [chooseAnother, setChooseAnother] = useState(location.state?.chooseAnother);
    const [oldDiamondId, setOldDiamondId] = useState(location.state?.oldDiamondId);
    const [oldRingId, setOldRingId] = useState(location.state?.oldRingId);
    const [oldPendantId, setOldPendantId] = useState(location.state?.oldPendantId);

    const [rechoose, setRechoose] = useState(location.state?.rechoose);
    const [rechooseType, setRechooseType] = useState(location.state?.rechooseType);

    // Function to fetch details of a diamond and a ring/pendant to create a pairing
    async function fetchPairingDetails(diamondId, productId, productType) {
        try {
            const diamond = await fetchDiamondById(diamondId);
            let product;

            switch (productType) {
                case 'ring':
                    product = await fetchRingById(productId);
                    break;
                case 'pendant':
                    product = await fetchPendantById(productId);
                    break;
                // Add more cases for future types
                // case 'earring':
                //     product = await fetchEarringById(productId);
                //     break;
                // case 'bracelet':
                //     product = await fetchBraceletById(productId);
                //     break;
                default:
                    throw new Error(`Unsupported product type: ${productType}`);
            }

            return {
                type: 'pairing',
                pId: `${diamondId}-${productId}`, // Unique ID for the pairing
                diamond,
                productType: productType,
                [productType]: product,
                diamondId: diamond.dProductId,
                productId: productId,
                price: diamond.price + product.price // Combined price
            };
        } catch (error) {
            throw new Error('Failed to fetch pairing details');
        }
    }

    const effectRan = useRef(false); // Ref to track if effect has run

    // useEffect for adding a diamond to the cart
    useEffect(() => {
        if (effectRan.current) {
            async function addDiamondToCart() {
                try {
                    if (diamondId) {
                        const diamond = await fetchDiamondById(diamondId);
                        const currentCart = getCartItems();

                        const diamondAlreadyInCart = currentCart.some(
                            item => item.details?.dProductId === diamond.dProductId && item.type === 'diamond'
                        );

                        const diamondInPairing = currentCart.some(item =>
                            item.type === 'pairing' && item.diamond.dProductId === diamond.dProductId
                        );

                        if (!diamondAlreadyInCart && !diamondInPairing) {
                            addToCart({
                                type: 'diamond',
                                details: diamond
                            });
                            showSnackbar('The diamond was added successfully', 'success', Date.now());
                        } else if (diamondAlreadyInCart) {
                            showSnackbar('The added diamond is already in cart', 'info', Date.now());
                        } else if (diamondInPairing) {
                            showSnackbar('The added diamond is already in another jewelry', 'info', Date.now());
                        }

                        setCart(getCartItems());
                        navigate('/cart', { replace: true });
                    }
                } catch (error) {
                    console.error("Error adding diamond to cart:", error);
                    setError(error.message);
                }
            }
            addDiamondToCart();
        }
        effectRan.current = true; // Set to true after first run

    }, [diamondId, navigate]);

    const pairingEffectRan = useRef(false); // Ref to track if the pairing effect has run

    // useEffect for adding a pairing to the cart (with ring or pendant)
    useEffect(() => {
        if (pairingEffectRan.current) {
            async function addPairingToCart() {
                try {
                    if (diamondIdPairProduct && (ringId || pendantId)) {
                        // Determine the product type
                        let productType, productId;
                        if (ringId) {
                            productType = 'ring';
                            productId = ringId;
                        } else if (pendantId) {
                            productType = 'pendant';
                            productId = pendantId;
                        }
                        // Add more cases here for future types like 'earring', 'bracelet', etc.

                        const pairing = await fetchPairingDetails(diamondIdPairProduct, productId, productType);
                        let currentCart = getCartItems();

                        const pairingHasDuplicatedDiamond = currentCart.some(
                            item => item.type === 'diamond' && item.details?.dProductId === pairing.diamond.dProductId
                        );

                        const pairingHasPairingWithDuplicateDiamond = currentCart.some(
                            item => item.type === 'pairing' && item.diamond.dProductId === pairing.diamond.dProductId
                        );

                        addToCart({
                            ...pairing,
                            chooseAnother: chooseAnother,
                            oldDiamondId: oldDiamondId,
                            oldRingId: oldRingId,
                            oldPendantId: oldPendantId
                        }, chooseAnother, oldDiamondId, oldRingId, oldPendantId);

                        setCart(getCartItems());

                        if (!chooseAnother) {
                            if (!rechoose) {
                                if (!pairingHasDuplicatedDiamond && !pairingHasPairingWithDuplicateDiamond) {
                                    showSnackbar(`Added ${pairing.productType === "ring" ? "ring"
                                        : pairing.productType === "pendant" ? "pendant"
                                            : "earring"} successfully`, 'success', Date.now());
                                } else if (pairingHasDuplicatedDiamond) {
                                    showSnackbar(`Replaced existing diamond with ${pairing.productType === "ring" ? "ring"
                                        : pairing.productType === "pendant" ? "pendant"
                                            : "earring"}`, 'info', Date.now());
                                } else if (pairingHasPairingWithDuplicateDiamond) {
                                    showSnackbar(`Replaced existing jewelry with new ${pairing.productType === "ring" ? "ring"
                                        : pairing.productType === "pendant" ? "pendant"
                                            : "earring"}`, 'info', Date.now());
                                }
                            } else {
                                if (rechooseType === 'diamond') {
                                    showSnackbar('Chosen the existing diamond in your jewelry', 'info', Date.now());
                                } else if (rechooseType === 'ring') {
                                    showSnackbar('Chosen the existing ring in your jewelry', 'info', Date.now());
                                } else if (rechooseType === 'pendant') {
                                    showSnackbar('Chosen the existing pendant in your jewelry', 'info', Date.now());
                                }
                            }
                        } else {
                            if (oldDiamondId) {
                                showSnackbar('Replaced diamond in your jewelry with a new diamond', 'success', Date.now());
                            } else if (oldRingId) {
                                showSnackbar('Replaced ring in your jewelry with a new ring', 'success', Date.now());
                            } else if (oldPendantId) {
                                showSnackbar('Replaced pendant in your jewelry with a new pendant', 'success', Date.now());
                            }
                        }
                        navigate('/cart', { replace: true });
                    }
                } catch (error) {
                    console.error("Error adding pairing to cart:", error);
                    setError(error.message);
                }
            }

            addPairingToCart();
        }
        pairingEffectRan.current = true; // Set to true after first run
    }, [diamondIdPairProduct, ringId, pendantId, navigate]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        setCart(getCartItems());
    }, []);

    // Function to remove an item from the cart
    const handleRemoveFromCart = (item, itemId, itemType) => {
        removeFromCart(itemId, itemType);
        showSnackbar(`${itemType === "diamond" ? "Diamond" : item.productType === "ring" ? "Ring" : item.productType === "pendant" ? "Pendant" : "Earring"} removed from cart`, 'info', Date.now());
        setCart(getCartItems());
    };

    // Function to clear the cart
    const handleClearCart = () => {
        clearCart();
        setCart([]);
        showSnackbar('Cart cleared', 'info', Date.now());
    };

    // Function to close the snackbar
    const handleSnackbarClose = () => {
        setSnackbarOpen({
            ...snackbarOpen,
            open: false,
        });
    };

    // Function to show a snackbar message
    const showSnackbar = (message, severity, id) => {
        setSnackbarMessage(message);
        setSnackbarSeverity(severity);
        setSnackbarOpen({ open: true, id: id });
    };

    // Function to calculate the total price of the cart
    const calculateTotalPrice = () => {
        return cart.reduce((total, item) => {
            return item.type === 'pairing' ? total += item.price : total += item.details.price;
        }, 0); // Initialize total with 0
    };

    const getRingName = (ring) => {
        let RingName = '';

        // Helper function to return non-null values or an empty string
        const safeValue = (value) => value ? value : '';

        // Build the ring name based on the type
        if (ring.ringType === 'Solitaire') {
            RingName = `${safeValue(ring.ringSubtype)} ${safeValue(ring.frameType)} ${safeValue(ring.ringType)} Engagement Ring in ${safeValue(ring.metalType)}`.trim();
        } else if (ring.ringType === 'Halo') {
            RingName = `${safeValue(ring.ringSubtype)} ${safeValue(ring.ringType)} Diamond Engagement Ring in ${safeValue(ring.metalType)}`.trim();
        } else if (ring.ringType === 'Sapphire sidestone') {
            RingName = `${safeValue(ring.ringSubtype)} Sapphire and Diamond Engagement Ring in ${safeValue(ring.metalType)}`.trim();
        } else if (ring.ringType === 'Three-stone') {
            RingName = `${safeValue(ring.ringSubtype)} ${safeValue(ring.ringType)} Diamond Engagement Ring in ${safeValue(ring.metalType)}`.trim();
        }

        // Add optional attributes like stoneCut or specialFeatures
        if (ring.stoneCut) {
            RingName = `${ring.stoneCut} ${RingName}`.trim();
        }
        if (ring.specialFeatures) {
            RingName = `${RingName} featuring ${ring.specialFeatures}`.trim();
        }

        // Remove any extra spaces
        RingName = RingName.replace(/\s+/g, ' ').trim();

        return RingName;
    };

    function handleClickLink(item) {
        navigate(`/diamond/${item.details.dProductId}`);
    }

    function handleClickDiamondInPairingLink(item) {
        if (item.ring) {
            navigate(`/ring/${item.ring.ringId}/choose-diamond/${item.diamond.dProductId}`, { state: { fromCart: true } });
        } else if (item.pendant) {
            navigate(`/pendant/${item.pendant.pendantId}/choose-diamond/${item.diamond.dProductId}`, { state: { fromCart: true } });
        }
    }

    function handleClickProductInPairing(item, productType) {
        if (productType === 'ring') {
            navigate(`/diamond/${item.diamond.dProductId}/choose-ring/${item.ring.ringId}`, { state: { fromCart: true } });
        } else if (productType === 'pendant') {
            navigate(`/diamond/${item.diamond.dProductId}/choose-pendant/${item.pendant.pendantId}`, { state: { fromCart: true } });
        }
    }

    return (
        <>
            {error ? (
                <p className={styles.errorMessage}>Error: {error}</p>
            ) : cart.length === 0 ? (
                <p className={styles.emptyCartMessage}>Your cart is empty.</p>
            ) : (
                <>
                    <div className={styles.shoppingCartAndCheckoutBoxContainer}>
                        <div className={styles.shoppingCartContainer}>
                            <h1>Shopping Cart</h1>
                            <ul className={styles.shoppingCartList}>
                                {cart.map((item, index) => {
                                    const uniqueKey = item.details?.dProductId || item.pId || index;
                                    return (
                                        <li key={`${uniqueKey}-${index}`} className={styles.shoppingCartItem}>
                                            {item.type === 'diamond' && (
                                                <div className={styles.cartItemDetails}>
                                                    <img src='/src/images/diamond.png' alt="Diamond" className={styles.cartItemImage} />
                                                    <div className={styles.cartItemInfo}>
                                                        <p><a onClick={() => handleClickLink(item)} style={{ cursor: 'pointer' }}>{item.details.caratWeight} Carat {item.details.color}-{item.details.clarity} {item.details.cut} Cut {item.details.shape} Diamond</a> - ${item.details.price}</p>
                                                        <p>Carat: {item.details.caratWeight} - Color: {item.details.color}</p>
                                                        <p>Clarity: {item.details.clarity} - Cut: {item.details.cut}</p>
                                                        <p>Diamond Type: {item.details.shape}</p>
                                                    </div>
                                                </div>
                                            )}
                                            {item.type === 'pairing' && (
                                                <div className={styles.cartItemDetails}>
                                                    <img src={item.ring ? '/src/images/ring.png' : '/src/images/pendant.png'} alt={item.ring ? "Ring" : "Pendant"} className={styles.cartItemImage} />
                                                    <div className={styles.cartItemInfo}>
                                                        <p>
                                                            {item.ring ? getRingName(item.ring) : item.pendant.name} - {item.diamond.caratWeight} Carat {item.diamond.color}-{item.diamond.clarity} {item.diamond.cut} Cut {item.diamond.shape} Diamond
                                                        </p>
                                                        <p>Diamond: <a onClick={() => handleClickDiamondInPairingLink(item)} style={{ cursor: 'pointer' }}>
                                                            {item.diamond.caratWeight} Carat {item.diamond.color}-{item.diamond.clarity} {item.diamond.cut} Cut {item.diamond.shape} Diamond
                                                        </a> - ${item.diamond.price}
                                                        </p>
                                                        {item.ring && (
                                                            <p>Ring: <a onClick={() => handleClickProductInPairing(item, 'ring')} style={{ cursor: 'pointer' }}>{getRingName(item.ring)}</a> - ${item.ring.price}</p>
                                                        )}
                                                        {item.pendant && (
                                                            <p>Pendant: <a onClick={() => handleClickProductInPairing(item, 'pendant')} style={{ cursor: 'pointer' }}>{item.pendant.name}</a> - ${item.pendant.price}</p>
                                                        )}
                                                        <p>Total Price: ${item.price}</p>
                                                    </div>
                                                </div>
                                            )
                                            }
                                            <button onClick={() => handleRemoveFromCart(item, item.details?.dProductId || item.pId, item.type)} className={styles.cartItemRemoveButton}>Remove</button>
                                        </li>
                                    );
                                })}
                            </ul>
                            <button onClick={handleClearCart} className={styles.clearCartButton}>Clear Cart</button>
                        </div>
                        <CheckoutBox totalPrice={calculateTotalPrice()} />
                    </div>
                </>
            )
            }
            <SnackbarCart
                open={snackbarOpen.open}
                handleClose={handleSnackbarClose}
                message={snackbarMessage}
                severity={snackbarSeverity}
                snackbarKey={snackbarOpen.id}
            />
        </>

    );
};

export default ShoppingCart;
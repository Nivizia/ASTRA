import React, { useEffect, useState, useRef } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { fetchDiamondById, fetchRingById, fetchPendantById } from '../../../javascript/apiService';
import { getCartItems, addToCart, removeFromCart, clearCart } from '../../../javascript/cartService';

import OrderButton from '../orderButton/orderButton'; import SnackbarCart from './SnackbarCart';

import '../css/shoppingcart.css';

const ShoppingCart = () => {
    const location = useLocation();
    const navigate = useNavigate();
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
                [productType]: product,
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
                            showSnackbar('Diamond added successfully', 'success', Date.now());
                        } else if (diamondAlreadyInCart) {
                            showSnackbar('Diamond already in cart', 'info', Date.now());
                        } else if (diamondInPairing) {
                            showSnackbar('Diamond already in pairing', 'info', Date.now());
                        }

                        setCart(getCartItems());
                        navigate('/cart', { replace: true });

                        console.log(diamondAlreadyInCart, diamondInPairing);
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

                        const pairingDiamondHasDiamondInCart = currentCart.some(
                            item => item.type === 'diamond' && item.details?.dProductId === pairing.diamond.dProductId
                        );

                        const pairingDiamondHasPairingInCart = currentCart.some(
                            item => item.type === 'pairing' && item.diamond.dProductId === pairing.diamond.dProductId
                        );
                        addToCart(pairing);
                        setCart(getCartItems());
                        
                        if (!pairingDiamondHasDiamondInCart && !pairingDiamondHasPairingInCart) {
                            showSnackbar('Added jewelry successfully', 'success', Date.now());
                        } else if (pairingDiamondHasDiamondInCart) {
                            showSnackbar('Replaced existing diamond with jewelry', 'info', Date.now());
                        } else if (pairingDiamondHasPairingInCart) {
                            showSnackbar('Replaced existing jewelry with new jewelry', 'info', Date.now());
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

    const handleRemoveFromCart = (itemId, itemType) => {
        removeFromCart(itemId, itemType);
        showSnackbar('Product removed', 'info', Date.now());
        setCart(getCartItems());

    };

    const handleClearCart = () => {
        clearCart();
        setCart([]);
        showSnackbar('Cart cleared', 'info', Date.now());
    };

    const handleSnackbarClose = () => {
        setSnackbarOpen({
            ...snackbarOpen,
            open: false,
        });
    };

    const showSnackbar = (message, severity, id) => {
        setSnackbarMessage(message);
        setSnackbarSeverity(severity);
        setSnackbarOpen({ open: true, id: id }); // Open a new snackbar.
    };

    return (
        <>
            {error ? (
                <p className="error-message">Error: {error}</p>
            ) : cart.length === 0 ? (
                <p className="empty-cart-message">Your cart is empty.</p>
            ) : (
                <div className="shopping-cart-container">
                    <h1>Shopping Cart</h1>
                    <ul className="shopping-cart-list">
                        {cart.map((item, index) => {
                            const uniqueKey = item.details?.dProductId || item.pId || index;
                            return (
                                <li key={`${uniqueKey}-${index}`} className="shopping-cart-item">
                                    {item.type === 'diamond' && (
                                        <div className="cart-item-details">
                                            <img src='/src/images/diamond.png' alt="Diamond" className="cart-item-image" />
                                            <div className="cart-item-info">
                                                <p>{item.details.caratWeight} Carat {item.details.color}-{item.details.clarity} {item.details.cut} Cut {item.details.dType} Diamond - ${item.details.price}</p>
                                                <p>Carat: {item.details.caratWeight} - Color: {item.details.color}</p>
                                                <p>Clarity: {item.details.clarity} - Cut: {item.details.cut}</p>
                                                <p>Diamond Type: {item.details.dType}</p>
                                            </div>
                                        </div>
                                    )}
                                    {item.type === 'pairing' && (
                                        <div className="cart-item-details">
                                            <img src={item.ring ? '/src/images/ring.png' : '/src/images/pendant.png'} alt={item.ring ? "Ring" : "Pendant"} className="cart-item-image" />
                                            <div className="cart-item-info">
                                                <p>
                                                    {item.ring ? item.ring.name : item.pendant.name} - {item.diamond.caratWeight} Carat {item.diamond.color}-{item.diamond.clarity} {item.diamond.cut} Cut {item.diamond.dType} Diamond
                                                </p>
                                                <p>Diamond: {item.diamond.caratWeight} Carat {item.diamond.color}-{item.diamond.clarity} {item.diamond.cut} Cut {item.diamond.dType} Diamond - ${item.diamond.price}</p>
                                                {item.ring && (
                                                    <p>Ring: {item.ring.name} - ${item.ring.price}</p>
                                                )}
                                                {item.pendant && (
                                                    <p>Pendant: {item.pendant.name} - ${item.pendant.price}</p>
                                                )}
                                                <p>Total Price: ${item.price}</p>
                                            </div>
                                        </div>
                                    )}
                                    <button onClick={() => handleRemoveFromCart(item.details?.dProductId || item.pId, item.type)} className="cart-item-remove-button">Remove</button>
                                </li>
                            );
                        })}
                    </ul>
                    <button onClick={handleClearCart} className="clear-cart-button">Clear Cart</button>
                    <OrderButton />

                </div>
            )}
            <SnackbarCart
                open={snackbarOpen.open}
                handleClose={handleSnackbarClose}
                message={snackbarMessage}
                severity={snackbarSeverity}
                key={snackbarOpen.id}
            />
        </>
    );
};

export default ShoppingCart;
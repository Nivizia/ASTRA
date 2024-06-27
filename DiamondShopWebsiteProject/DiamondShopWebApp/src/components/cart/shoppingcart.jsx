import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { fetchDiamondById, fetchRingById, fetchPendantById } from '../../../javascript/apiService';
import { getCartItems, addToCart, removeFromCart, clearCart } from '../../../javascript/cartService';
import '../css/shoppingcart.css'; // Import the CSS file

const ShoppingCart = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const params = new URLSearchParams(location.search);
    const diamondId = params.get('diamondId'); // Get the diamondId from the URL for direct add to cart

    const diamondIdPairProduct = params.get('d'); // Get the diamondId from the URL choosing diamond first
    const ringId = params.get('r'); // Get the ringId from the URL choosing diamond first
    const pendantId = params.get('p'); // Get the pendantId from the URL choosing diamond first

    const [cart, setCart] = useState([]);
    const [error, setError] = useState(null);

    // Function to fetch details of a diamond and a ring/pendant to create a pairing
    async function fetchPairingDetails(diamondId, productId, isRing = true) {
        try {
            const diamond = await fetchDiamondById(diamondId);
            const product = isRing ? await fetchRingById(productId) : await fetchPendantById(productId);
            const type = isRing ? 'ring' : 'pendant';
            return {
                type: 'pairing',
                pId: `${diamondId}-${productId}`, // Unique ID for the pairing
                diamond,
                [type]: product,
                price: diamond.price + product.price // Combined price
            };
        } catch (error) {
            throw new Error('Failed to fetch pairing details');
        }
    }

    // useEffect for adding a diamond to the cart
    useEffect(() => {
        async function addDiamondToCart() {
            try {
                if (diamondId) {
                    const diamond = await fetchDiamondById(diamondId);
                    console.log('Fetched Diamond:', diamond);
                    const currentCart = getCartItems();
                    console.log('Current Cart:', currentCart);

                    const diamondAlreadyInCart = currentCart.some(
                        item => item.details?.dProductId === diamond.dProductId && item.type === 'diamond'
                    );

                    if (!diamondAlreadyInCart) {
                        addToCart({
                            type: 'diamond',
                            details: diamond
                        });
                        setCart(getCartItems());
                    }

                    // Change the URL to remove the diamondId parameter
                    navigate('/cart', { replace: true });
                }
            } catch (error) {
                console.error("Error adding diamond to cart:", error);
                setError(error.message);
            }
        }

        if (diamondId) {
            addDiamondToCart();
        }
    }, [diamondId, navigate]);

    // useEffect for adding a pairing to the cart (with ring or pendant)
    useEffect(() => {
        async function addPairingToCart() {
            try {
                if (diamondIdPairProduct && (ringId || pendantId)) {
                    const pairing = await fetchPairingDetails(diamondIdPairProduct, ringId || pendantId, Boolean(ringId));
                    console.log('Fetched Pairing:', pairing);
                    let currentCart = getCartItems();

                    // Remove any standalone diamond that matches the diamond in the pairing
                    currentCart = currentCart.filter(item => {
                        if (item.type === 'diamond') {
                            return item.details.dProductId !== pairing.diamond.dProductId;
                        }
                        return true;
                    });

                    const pairingAlreadyInCart = currentCart.some(
                        item => item.pId === pairing.pId && item.type === 'pairing'
                    );

                    if (!pairingAlreadyInCart) {
                        addToCart(pairing);
                        setCart(getCartItems());
                    }

                    navigate('/cart', { replace: true });
                }
            } catch (error) {
                console.error("Error adding pairing to cart:", error);
                setError(error.message);
            }
        }

        if (diamondIdPairProduct && (ringId || pendantId)) {
            addPairingToCart();
        }
    }, [diamondIdPairProduct, ringId, pendantId, navigate]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        setCart(getCartItems());
    }, []);

    const handleRemoveFromCart = (itemId, itemType) => {
        removeFromCart(itemId, itemType);
        setCart(getCartItems());
        console.log(`Removed item from cart: ${itemId} of type ${itemType}`);
    };

    const handleClearCart = () => {
        clearCart();
        setCart([]);
    };

    if (error) {
        return <p className="error-message">Error: {error}</p>;
    }

    if (cart.length === 0) {
        return <p className="empty-cart-message">Your cart is empty.</p>;
    }

    return (
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
        </div>
    );
};

export default ShoppingCart;

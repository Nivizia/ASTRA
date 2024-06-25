import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom'; // Import useNavigate to change the URL
import { fetchDiamondById, fetchRingById } from '../../../javascript/apiService';
import { getCartItems, addToCart, removeFromCart, clearCart } from '../../../javascript/cartService';
import '../css/shoppingcart.css'; // Import the CSS file

const ShoppingCart = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const params = new URLSearchParams(location.search);
    const diamondId = params.get('diamondId'); // Get the diamondId from the URL for direct add to cart

    const diamondIdPairProduct = params.get('d'); // Get the diamondId from the URL choosing diamond first
    const ringId = params.get('r'); // Get the ringId from the URL choosing diamond first

    console.log(`${diamondId}, ${diamondIdPairProduct}, ${ringId}`); // Log the values for debugging

    const [cart, setCart] = useState([]);
    const [error, setError] = useState(null);

    // Function to fetch details of a diamond and a ring to create a pairing
    async function fetchPairingDetails(diamondIdPairProduct, ringId) {
        try {
            const diamond = await fetchDiamondById(diamondIdPairProduct);
            const ring = await fetchRingById(ringId);
            return {
                type: 'pairing',
                pId: `${diamondIdPairProduct}-${ringId}`, // Unique ID for the pairing
                diamond,
                ring,
                price: diamond.price + ring.price // Combined price
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
                    console.log('Fetched Diamond:', diamond); // Inspect diamond attributes
                    const currentCart = getCartItems();
                    console.log('Current Cart:', currentCart); // Inspect current cart items

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
                console.error("Error adding diamond to cart:", error); // Log the error
                setError(error.message);
            }
        }

        // Only attempt to add a diamond if a diamondId is present
        if (diamondId) {
            addDiamondToCart();
        }
    }, [diamondId, navigate]); // Add navigate to the dependency array

    // useEffect for adding a pairing to the cart
    useEffect(() => {
        async function addPairingToCart() {
            try {
                if (diamondIdPairProduct && ringId) {
                    const pairing = await fetchPairingDetails(diamondIdPairProduct, ringId);
                    console.log('Fetched Pairing:', pairing); // Inspect pairing attributes
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
                console.error("Error adding pairing to cart:", error); // Log the error
                setError(error.message);
            }
        }

        if (diamondIdPairProduct && ringId) {
            addPairingToCart();
        }
    }, [diamondIdPairProduct, ringId, navigate]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        setCart(getCartItems());
    }, []);

    const handleRemoveFromCart = (itemId, itemType) => {
        removeFromCart(itemId, itemType);
        setCart(getCartItems()); // Update state to re-render the component
        console.log(`Removed item from cart: ${itemId} of type ${itemType}`); // Log the removed item ID and type
    };

    const handleClearCart = () => {
        clearCart();
        setCart([]); // Update state to re-render the component
    };

    if (error) {
        return <p className="error-message">Error: {error}</p>; // Display the error message
    }

    if (cart.length === 0) {
        return <p className="empty-cart-message">Your cart is empty.</p>; // Display message if the cart is empty
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
                                        <p>Name: {item.details.caratWeight} Carat {item.details.color}-{item.details.clarity} {item.details.cut} Cut {item.details.dType} Diamond - ${item.details.price.toFixed(2)}</p>
                                        <p>Carat: {item.details.caratWeight} - Color: {item.details.color}</p>
                                        <p>Clarity: {item.details.clarity} - Cut: {item.details.cut}</p>
                                        <p>Diamond Type: {item.details.dType}</p>
                                    </div>
                                </div>
                            )}
                            {item.type === 'pairing' && (
                                <div className="cart-item-details">
                                    <img src='/src/images/ring.png' alt="Diamond" className="cart-item-image" />
                                    <div className="cart-item-info">
                                        <p>{item.ring.name} ring with {item.diamond.caratWeight} Carat {item.diamond.color}-{item.diamond.clarity} {item.diamond.cut} Cut {item.diamond.dType} Diamond
                                        </p>
                                        <p>Diamond: {item.diamond.caratWeight} Carat {item.diamond.color}-{item.diamond.clarity} {item.diamond.cut} Cut {item.diamond.dType} Diamond - ${item.diamond.price.toFixed(2)}</p>
                                        <p>Ring: {item.ring.name} - ${item.ring.price.toFixed(2)}</p>
                                        <p>Total Price: ${item.price.toFixed(2)}</p>
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

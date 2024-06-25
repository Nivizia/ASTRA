import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { fetchDiamondById } from '../../../javascript/apiService';
import { getCartItems, addToCart, removeFromCart, clearCart } from '../../../javascript/cartService';
import '../css/shoppingcart.css'; // Import the CSS file

const ShoppingCart = () => {
    const location = useLocation();
    const params = new URLSearchParams(location.search);
    const diamondId = params.get('diamondId');

    const [cart, setCart] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function addDiamondToCart() {
            try {
                if (diamondId) {
                    const diamond = await fetchDiamondById(diamondId);
                    const currentCart = getCartItems();
                    
                    const diamondAlreadyInCart = currentCart.some(
                        item => item.details.dProductId === diamond.dProductId && item.type === 'diamond'
                    );

                    if (!diamondAlreadyInCart) {
                        addToCart({
                            type: 'diamond',
                            details: diamond
                        });
                        setCart(getCartItems());
                    }
                }
            } catch (error) {
                setError(error.message);
            }
        }
        addDiamondToCart();
    }, [diamondId]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        setCart(getCartItems());
    }, []);

    const handleRemoveFromCart = (itemId) => {
        removeFromCart(itemId);
        setCart(getCartItems()); // Update state to re-render the component
    };

    const handleClearCart = () => {
        clearCart();
        setCart([]); // Update state to re-render the component
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
                {cart.map((item, index) => (
                    <li key={item.details.dProductId} className="shopping-cart-item">
                        {item.type === 'diamond' && (
                            <div className="cart-item-details">
                                <img src='/src/images/diamond.png' alt="Diamond" className="cart-item-image" />
                                <div className="cart-item-info">
                                    <p>{item.details.name} - ${item.details.price.toFixed(2)}</p>
                                    <p>Carat: {item.details.caratWeight}</p>
                                    <p>Clarity: {item.details.clarity}</p>
                                    <p>Color: {item.details.color}</p>
                                    <p>Cut: {item.details.cut}</p>
                                </div>
                            </div>
                        )}
                        <button onClick={() => handleRemoveFromCart(item.details.dProductId)} className="cart-item-remove-button">Remove</button>
                    </li>
                ))}
            </ul>
            <button onClick={handleClearCart} className="clear-cart-button">Clear Cart</button>
        </div>
    );
};

export default ShoppingCart;

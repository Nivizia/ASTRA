import React, { useState, useEffect } from 'react';

import { getCartItems, addToCart, removeFromCart, clearCart } from '../../javascript/cartService';

const ShoppingCart = () => {
    const [cart, setCart] = useState([]);

    useEffect(() => {
        // Load cart items from local storage when the component mounts
        setCart(getCartItems());
    }, []);

    const handleAddToCart = (item) => {
        addToCart(item);
        setCart(getCartItems()); // Update state to re-render the component
    };

    const handleRemoveFromCart = (itemId) => {
        removeFromCart(itemId);
        setCart(getCartItems()); // Update state to re-render the component
    };

    const handleClearCart = () => {
        clearCart();
        setCart([]); // Update state to re-render the component
    };

    return (
        <div>
            <h1>Shopping Cart</h1>
            <ul>
                {cart.map(item => (
                    <li key={item.id}>
                        {item.name} - ${item.price}
                        <button onClick={() => handleRemoveFromCart(item.id)}>Remove</button>
                    </li>
                ))}
            </ul>
            <button onClick={handleClearCart}>Clear Cart</button>
        </div>
    );
};

export default ShoppingCart;

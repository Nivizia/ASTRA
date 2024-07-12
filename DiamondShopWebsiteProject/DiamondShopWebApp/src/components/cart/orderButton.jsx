// src/components/cart/orderButton.jsx

import React, { useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import styles from '../css/orderButton.module.css';

const OrderButton = () => {
    const { user } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleOrderClick = () => {
        if (!user) {
            navigate('/login', { state: { fromCart: true } }); // Redirect to the login page if there is no user
            return;
        }
        navigate('/checkout', { state: { fromCart: true } }); // Navigate to the order confirmation page
    };

    return (
        <button onClick={handleOrderClick} className={styles.orderButton}>
            Place Order
        </button>
    );
};

export default OrderButton;
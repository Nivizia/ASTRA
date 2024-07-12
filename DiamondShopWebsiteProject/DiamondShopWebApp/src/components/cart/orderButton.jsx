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
            navigate('/login?cart=true'); // Redirect to login page with cart query parameter
            return;
        }
        navigate('/order-confirmation', { state: { fromCart: true } }); // Navigate to the order confirmation page
    };

    return (
        <button onClick={handleOrderClick} className={styles.orderButton}>
            Place Order
        </button>
    );
};

export default OrderButton;
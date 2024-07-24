import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useLocation } from 'react-router-dom';

const OrderConfirmation = () => {
    const [message, setMessage] = useState('');
    const location = useLocation();

    useEffect(() => {
        // Extract the token from the URL
        const params = new URLSearchParams(location.search);
        const token = params.get('t');

        if (token) {
            // Send a request to confirm the order
            axios.put(`http://astradiamonds.com:5212/DiamondAPI/Models/Orders/Confirm/${token}`)
                .then(response => {
                    setMessage('Your order has been confirmed successfully!');
                })
                .catch(error => {
                    console.error('There was an error confirming the order:', error);
                    setMessage('Failed to confirm your order. Please try again later.');
                });
        } else {
            setMessage('Invalid or missing token.');
        }
    }, [location.search]);

    return (
        <div className="confirmation-container">
            <h1>Order Confirmation</h1>
            <p>{message}</p>
        </div>
    );
};

export default OrderConfirmation;

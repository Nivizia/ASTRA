import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useLocation, Link } from 'react-router-dom';
import styles from '../css/EmailConfirmation.module.css';

const EmailConfirmation = () => {
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
        <div className={styles.confirmationContainer}>
            <h1>Order Confirmation</h1>
            <p>{message}</p>
            {message && (
                <Link to="/" className={styles.homeButton}>Go to Home</Link>
            )}
        </div>
    );
};

export default EmailConfirmation;
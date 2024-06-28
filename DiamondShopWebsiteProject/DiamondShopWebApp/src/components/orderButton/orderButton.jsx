import React, { useState, useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import CircularIndeterminate from '../loading';
import styles from '../css/orderButton.module.css';

const OrderButton = () => {
    const { user } = useContext(AuthContext);
    const [loading, setLoading] = useState(false); // Add loading state
    const navigate = useNavigate();

    const handleOrderClick = () => {
        setLoading(true); // Set loading to true when order starts
        try {
            if (user) {
                setTimeout(() => {
                    navigate('/order-confirmation');
                    setLoading(false); // Set loading to false after navigation delay
                }, 1000);
            } else {
                navigate('/');
                setLoading(false); // Set loading to false immediately for this synchronous operation
            }
        } catch (error) {
            console.error('Error during order:', error);
            setLoading(false); // Ensure loading is set to false if an error occurs
        }
    };

    return (
        <button onClick={handleOrderClick} className={styles.orderButton} disabled={loading}>
            {loading ? <CircularIndeterminate size={24} /> : 'Place Order'}
        </button>
    );
};

export default OrderButton;

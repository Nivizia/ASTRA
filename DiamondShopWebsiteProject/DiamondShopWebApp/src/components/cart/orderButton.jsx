import React, { useState, useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import CircularIndeterminate from '../misc/loading';
import { getCartItems, clearCart } from '../../../javascript/cartService';
import { createOrder } from '../../../javascript/apiService';
import styles from '../css/orderButton.module.css';
import { LinearProgress } from '@mui/material';

const OrderButton = () => {
    const { user, logout } = useContext(AuthContext);
    const [loading, setLoading] = useState(false);
    const [errorLoading, setErrorLoading] = useState(false);
    const [error, setError] = useState(null); // Error state
    const navigate = useNavigate();

    const handleOrderClick = async () => {
        setLoading(true);
        setError(null); // Clear any previous error

        if (!user) {
            navigate('/login?cart=true'); // Redirect to login page with cart query parameter
            setLoading(false);
            return;
        }

        // Prepare the order details from cart
        const cartItems = getCartItems();
        const orderDetails = {
            customerId: user.sub, // User ID from Token decoding
            totalAmount: cartItems.reduce((total, item) => item.type === 'pairing' ? total + item.price : total + item.details.price, 0),
            orderItems: cartItems.map(item => {
                if (item.type === 'diamond') {
                    return {
                        productId: item.details.dProductId,
                        price: item.details.price,
                        productType: "Diamond",
                        createRingPairingDTO: null,
                        createPendantPairingDTO: null,
                        createEarringPairingDTO: null
                    };
                } else if (item.type === 'pairing') {
                    if (item.productType === 'ring') {
                        return {
                            productId: item.diamond.dProductId,
                            price: item.price,
                            productType: "RingPairing",
                            createRingPairingDTO: {
                                ringId: item.productId,
                                diamondId: item.diamond.dProductId
                            },
                            createPendantPairingDTO: null,
                            createEarringPairingDTO: null
                        };
                    } else if (item.productType === 'pendant') {
                        return {
                            productId: item.diamond.dProductId,
                            price: item.price,
                            productType: "PendantPairing",
                            createRingPairingDTO: null,
                            createPendantPairingDTO: {
                                pendantId: item.productId,
                                diamondId: item.diamond.dProductId
                            },
                            createEarringPairingDTO: null
                        };
                    }
                }
            })
        };

        try {
            const orderResponse = await createOrder(orderDetails);
            if (orderResponse.success) {
                console.log('Order created successfully:', orderResponse);
                clearCart(); // Clear the cart after placing the order
                navigate('/order-confirmation'); // Navigate to the order confirmation page
            } else {
                if (orderResponse.noUser) {
                    console.log('User not found, displaying error and logging out...');
                    setError('Failed to place order! User does not exist.');
                    setErrorLoading(true);

                    // Wait for 3 seconds before logging out and navigating
                    setTimeout(() => {
                        logout(); // Log the user out
                        navigate('/login?cart=true'); // Redirect to the login page
                        setErrorLoading(false);
                    }, 3000);
                } else {
                    setError(`Failed to place order! ${orderResponse.message}`);
                }
            }
        } catch (error) {
            console.error('Unexpected error:', error);
            setError(`Failed to place order! ${error.message || 'Unknown error'}`);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <button onClick={handleOrderClick} className={styles.orderButton} disabled={loading}>
                {loading ? <CircularIndeterminate size={24} /> : 'Place Order'}
            </button>
            {error && <p className={styles.errorMessage}>{error}</p>} {/* Display error message */}
            {errorLoading ? (
                <>
                    <p>Redirecting to login...</p>
                    <LinearProgress />
                </>
            ) : null}
        </div>
    );
};

export default OrderButton;

// src/components/cart/orderConfirmation.jsx

import { useState, useEffect, useContext } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { AuthContext } from '../../contexts/AuthContext';
import CircularIndeterminate from '../misc/loading';
import { getCartItems, clearCart } from '../../../javascript/cartService';
import { createOrder } from '../../../javascript/apiService';
import { LinearProgress } from '@mui/material';
import styles from '../css/orderConfirmation.module.css';

const OrderConfirmation = () => {
    const { user, logout } = useContext(AuthContext);
    const location = useLocation();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [fromCart, setFromCart] = useState(location.state?.fromCart);
    const [paymentMethod, setPaymentMethod] = useState('cash');
    const [receivingMethod, setReceivingMethod] = useState('delivery');
    const [orderLoading, setOrderLoading] = useState(false);
    const [error, setError] = useState(null);
    const [errorLoading, setErrorLoading] = useState(false);

    useEffect(() => {
        if (!fromCart) {
            setLoading(true);
            setTimeout(() => {
                navigate('/');
                setLoading(false);
                setFromCart(false);
            }, 2000);
        } else {
            setLoading(false);
        }
    }, [fromCart, navigate]);

    const handlePlaceOrder = async () => {
        setOrderLoading(true);
        setError(null); // Clear any previous error

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
                navigate('/order-confirmation', { state: { fromCart: false } }); // Navigate to the order confirmation page
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
            setOrderLoading(false);
        }
    };

    return (
        <>
            {fromCart ? (
                <div className={styles.orderConfirmationContainer}>
                    <h1 className={styles.orderConfirmationTitle}>Order Confirmation</h1>
                    <div className={styles.orderConfirmationDetails}>
                        <div className={styles.userInfo}>
                            <h2>User Information</h2>
                            <div className={styles.userInfoRow}>
                                <div className={styles.userInfoLabel}>First Name:</div>
                                <div className={styles.userInfoValue}>{user.FirstName}</div>
                            </div>
                            <div className={styles.userInfoRow}>
                                <div className={styles.userInfoLabel}>Last Name:</div>
                                <div className={styles.userInfoValue}>{user.LastName}</div>
                            </div>
                            <div className={styles.userInfoRow}>
                                <div className={styles.userInfoLabel}>Phone:</div>
                                <div className={styles.userInfoValue}>{user.phone}</div>
                            </div>
                            <div className={styles.userInfoRow}>
                                <div className={styles.userInfoLabel}>Email:</div>
                                <div className={styles.userInfoValue}>{user.Email}</div>
                            </div>
                        </div>
                        <div className={styles.paymentMethod}>
                            <h2 className={styles.paymentMethodTitle}>Payment Method</h2>
                            <label className={styles.paymentOption}>
                                <input
                                    type="radio"
                                    value="cash"
                                    checked={paymentMethod === 'cash'}
                                    onChange={(e) => setPaymentMethod(e.target.value)}
                                />
                                Cash
                            </label>
                            <label className={styles.paymentOption}>
                                <input
                                    type="radio"
                                    value="card"
                                    checked={paymentMethod === 'card'}
                                    onChange={(e) => setPaymentMethod(e.target.value)}
                                />
                                Card
                            </label>
                        </div>
                        <div className={styles.receivingMethod}>
                            <h2 className={styles.receivingMethodTitle}>Receiving Method</h2>
                            <label className={styles.receivingOption}>
                                <input
                                    type="radio"
                                    value="delivery"
                                    checked={receivingMethod === 'delivery'}
                                    onChange={(e) => setReceivingMethod(e.target.value)}
                                />
                                Delivery
                            </label>
                            <label className={styles.receivingOption}>
                                <input
                                    type="radio"
                                    value="store"
                                    checked={receivingMethod === 'store'}
                                    onChange={(e) => setReceivingMethod(e.target.value)}
                                />
                                Take at Store
                            </label>
                        </div>
                    </div>
                    <button onClick={handlePlaceOrder} className={styles.orderButton} disabled={orderLoading}>
                        {orderLoading ? <CircularIndeterminate size={24} /> : 'Place Order'}
                    </button>
                    {error && <p className={styles.errorMessage}>{error}</p>} {/* Display error message */}
                    {errorLoading ? (
                        <>
                            <p>Redirecting to login...</p>
                            <LinearProgress />
                        </>
                    ) : null}
                </div>
            ) : (
                <>
                    <CircularIndeterminate size={56} />
                    <p>You are being redirected to home page.</p>
                </>
            )}
        </>
    )
};

export default OrderConfirmation;
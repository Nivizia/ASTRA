// src/components/cart/checkout

import { useState, useEffect, useContext } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { AuthContext } from '../../contexts/AuthContext';
import { getCartItems, clearCart } from '../../../javascript/cartService';
import { createOrder } from '../../../javascript/apiService';
import { LinearProgress } from '@mui/material';

import CircularIndeterminate from '../misc/loading';

import styles from '../css/checkout.module.css';

const CheckOut = () => {
    const { user, logout } = useContext(AuthContext);
    const location = useLocation();
    const navigate = useNavigate();
    const [loading, setLoading] = useState(true);
    const [fromCart, setFromCart] = useState(location.state?.fromCart);
    const [paymentMethod, setPaymentMethod] = useState('cash');
    const [receivingMethod, setReceivingMethod] = useState('delivery');
    const [error, setError] = useState(null);

    useEffect(() => {
        if (!fromCart) {
            setLoading(true);
            setTimeout(() => {
                setLoading(false);
                setFromCart(false);
                navigate('/');
            }, 2000);
        } else {
            setLoading(false);
        }
    }, [fromCart, navigate]);

    const handlePlaceOrder = async () => {
        setLoading(true);
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
                navigate('/order-confirmation'); // Navigate to the order confirmation page
            } else {
                if (orderResponse.noUser) {
                    console.log('User not found, displaying error and logging out...');
                    setError('Failed to place order! User does not exist.');
                    setTimeout(() => {
                        logout(); // Log the user out
                        navigate('/login', { state: { fromCart: true } }); // Redirect to the login page if there is no user
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
        <>
            {loading ? (
                <>
                    <CircularIndeterminate size={56} />
                    {error ? (
                        <>
                            <p className={styles.errorMessage}>{error}</p>
                            <p>Redirecting to login...</p>
                            <LinearProgress />
                        </>
                    ) : (
                        <p>You are being redirected to home page.</p>
                    )}
                </>
            ) : (
                <div className={styles.checkoutContainer}>
                    <h1 className={styles.checkoutTitle}>Order Confirmation</h1>
                    <div className={styles.checkoutDetails}>
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
                    <button onClick={handlePlaceOrder} className={styles.orderButton} disabled={loading}>
                        {loading ? <CircularIndeterminate size={24} /> : 'Place Order'}
                    </button>
                    {error && <p className={styles.errorMessage}>{error}</p>} {/* Display error message */}
                </div>
            )}
        </>
    );
};

export default CheckOut;

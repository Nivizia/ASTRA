// src/components/cart/checkout

import { useState, useEffect, useContext } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { AuthContext } from '../../contexts/AuthContext';
import { getCartItems, clearCart } from '../../../javascript/cartService';
import { createOrder } from '../../../javascript/apiService';
import { LinearProgress, Tooltip, TextField } from '@mui/material';

import ToggleButton from '@mui/material/ToggleButton';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import Switch from '@mui/material/Switch';

import CircularIndeterminate from '../misc/loading';

import styles from '../css/checkout.module.css';

const CheckOut = () => {
    const { user, logout } = useContext(AuthContext);
    const location = useLocation();
    const navigate = useNavigate();
    const [allowed, setAllowed] = useState(location.state?.allowed);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const [paymentMethod, setPaymentMethod] = useState('cash');
    const [receivingMethod, setReceivingMethod] = useState('takeatstore');
    const [buyAsGift, setBuyAsGift] = useState(false);

    const [giftFirstName, setGiftFirstName] = useState('');
    const [giftLastName, setGiftLastName] = useState('');
    const [giftEmail, setGiftEmail] = useState('');
    const [giftPhoneNumber, setGiftPhoneNumber] = useState('');

    useEffect(() => {
        if (!allowed) {
            setLoading(true);
            setTimeout(() => {
                setLoading(false);
                setAllowed(false);
                navigate('/');
            }, 2000);
        } else {
            setLoading(false);
        }
    }, [allowed, navigate]);

    function getUserInfo() {
        if (buyAsGift) {
            return {
                FirstName: giftFirstName,
                LastName: giftLastName,
                Email: giftEmail,
                PhoneNumber: giftPhoneNumber,
            };
        } else {
            return {
                FirstName: user.FirstName ?? '',
                LastName: user.LastName ?? '',
                Email: user.Email ?? '',
                PhoneNumber: user.PhoneNumber ?? '',
            };
        }
    };

    const handlePlaceOrder = async () => {
        setLoading(true);
        setError(null); // Clear any previous error

        // Prepare the order details from cart
        const cartItems = getCartItems();
        const userInfo = getUserInfo(); // Get the user info based on the buyAsGift state
        const orderDetails = {
            customerId: user.sub, // User ID from Token decoding
            totalAmount: cartItems.reduce((total, item) => item.type === 'pairing' ? total + item.price : total + item.details.price, 0),
            orderFirstName: userInfo.FirstName,
            orderLastName: userInfo.LastName,
            orderEmail: userInfo.Email,
            orderPhoneNumber: userInfo.PhoneNumber,
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
            }),
            recipientInfo: buyAsGift ? {
                firstName: giftFirstName,
                lastName: giftLastName,
                email: giftEmail,
                phone: giftPhoneNumber
            } : null
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

    if (!user) {
        navigate("/login");
    }

    const handleSetPaymentMethod = (event, newPaymentMethod) => {
        if (newPaymentMethod !== null) {
            setPaymentMethod(newPaymentMethod);
        }
    };

    const handleSetReceivingMethod = (event, newReceivingMethod) => {
        if (newReceivingMethod !== null) {
            setReceivingMethod(newReceivingMethod);
        }
    }

    const handleSetBuyAsGift = (event) => {
        setBuyAsGift(event.target.checked);
        console.log(buyAsGift);
    }

    const handleClickUpdateProfile = () => {
        navigate('/profile', { state: { fromCheckout: true } });
    }

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
                    <h1 className={styles.checkoutTitle}>CHECKOUT</h1>
                    <div className={styles.checkoutDetails}>
                        <div className={styles.userInfo}>
                            <h2>User Information</h2>
                            {!buyAsGift ? (
                                <>
                                    <div className={styles.userInfoRow}>
                                        <div className={styles.userInfoLabel}>First Name:</div>
                                        <Tooltip title="Please update your first name if it is incorrect" arrow placement="right">
                                            <div className={styles.userInfoValue}>{user.FirstName}</div>
                                        </Tooltip>
                                    </div>
                                    <div className={styles.userInfoRow}>
                                        <div className={styles.userInfoLabel}>Last Name:</div>
                                        <Tooltip title="Please update your last name if it is incorrect" arrow placement="right">
                                            <div className={styles.userInfoValue}>{user.LastName}</div>
                                        </Tooltip>
                                    </div>
                                    {user.phone ? (
                                        <div className={styles.userInfoRow}>
                                            <div className={styles.userInfoLabel}>Phone:</div>
                                            <Tooltip title="Please update your phone number if it is incorrect" arrow placement="right">
                                                <div className={styles.userInfoValue}>{user.phone}</div>
                                            </Tooltip>
                                        </div>
                                    ) : null
                                    }
                                    <div className={styles.userInfoRow}>
                                        <div className={styles.userInfoLabel}>Email:</div>
                                        <Tooltip title="Please update your email if it is incorrect" arrow placement="right">
                                            <div className={styles.userInfoValue}>{user.Email}</div>
                                        </Tooltip>
                                    </div>
                                    <p className={styles.warningNotification}>Please make sure your user information is correctly set because we will use it as a verification method</p>
                                    <p className={styles.warningNotification}>Update user information <a onClick={() => handleClickUpdateProfile()}>here</a></p>
                                </>
                            ) : (
                                <>
                                    <TextField
                                        label="Recipient First Name"
                                        variant="outlined"
                                        fullWidth
                                        margin="normal"
                                        required
                                        value={giftFirstName}
                                        onChange={(e) => setGiftFirstName(e.target.value)}
                                    />
                                    <TextField
                                        label="Recipient Last Name"
                                        variant="outlined"
                                        fullWidth
                                        margin="normal"
                                        required
                                        value={giftLastName}
                                        onChange={(e) => setGiftLastName(e.target.value)}
                                    />
                                    <TextField
                                        label="Recipient Email"
                                        variant="outlined"
                                        fullWidth
                                        margin="normal"
                                        required
                                        value={giftEmail}
                                        onChange={(e) => setGiftEmail(e.target.value)}
                                    />
                                    <TextField
                                        label="Recipient Phone"
                                        variant="outlined"
                                        fullWidth
                                        margin="normal"
                                        required
                                        value={giftPhoneNumber}
                                        onChange={(e) => setGiftPhoneNumber(e.target.value)}
                                    />
                                </>
                            )}
                        </div>
                        <div className={styles.buyAsAGiftContainer}>
                            <h4>Buy as a gift: </h4>
                            <Switch
                                checked={buyAsGift}
                                onChange={handleSetBuyAsGift}
                                inputProps={{ 'aria-label': 'controlled' }}
                            />
                        </div>
                        <div className={styles.paymentMethod}>
                            <h2 className={styles.paymentMethodTitle}>Payment Method</h2>
                            <ToggleButtonGroup
                                value={paymentMethod}
                                exclusive
                                onChange={handleSetPaymentMethod}
                                aria-label="text alignment"
                            >
                                <ToggleButton value="cash" aria-label="cash" className={styles.paymentOption}>
                                    Cash
                                </ToggleButton>
                                <Tooltip title="Card payment is not available at the moment" arrow>
                                    <span>
                                        <ToggleButton value="card" aria-label="card" className={styles.paymentOption} disabled>
                                            Card
                                        </ToggleButton>
                                    </span>
                                </Tooltip>
                            </ToggleButtonGroup>
                        </div>
                        <div className={styles.receivingMethod}>
                            <h2 className={styles.receivingMethodTitle}>Receiving Method</h2>
                            <ToggleButtonGroup
                                value={receivingMethod}
                                exclusive
                                onChange={handleSetReceivingMethod}
                                aria-label="text alignment"
                            >
                                <ToggleButton value="takeatstore" aria-label="takeatstore" className={styles.receivingOption}>
                                    Take at store
                                </ToggleButton>
                                <Tooltip title="Delivery is not available at the moment" arrow>
                                    <span>
                                        <ToggleButton value="Delivery" aria-label="delivery" className={styles.receivingOption} disabled>
                                            Delivery
                                        </ToggleButton>
                                    </span>
                                </Tooltip>
                            </ToggleButtonGroup>
                        </div>
                    </div>
                    {!buyAsGift ? (
                        // Buy for self case
                        <button onClick={handlePlaceOrder} className={styles.orderButton} disabled={loading}>
                            {loading ? <CircularIndeterminate size={24} /> : 'Place Order'}
                        </button>
                    ) : (
                        // Buy as gift case
                        // Disable the button if any of the gift fields are empty
                        !giftFirstName || !giftLastName || !giftEmail || !giftPhoneNumber ? (
                            <Tooltip title="Please fill all the fields above" arrow>
                                <span>
                                    <button className={styles.orderButton} disabled>Place Order</button>
                                </span>
                            </Tooltip>

                        ) : (
                            // Enable the button if all the gift fields are filled
                            <button onClick={handlePlaceOrder} className={styles.orderButton} disabled={loading}>
                                {loading ? <CircularIndeterminate size={24} /> : 'Place Order'}
                            </button>
                        )
                    )
                    }
                    {error && <p className={styles.errorMessage}>{error}</p>} {/* Display error message */}
                </div>
            )}
        </>
    );
};

export default CheckOut;
import React, { useState, useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import { useNavigate } from 'react-router-dom';
import CircularIndeterminate from '../loading';
import { getCartItems } from '../../../javascript/cartService';
import { createOrder } from '../../../javascript/apiService';
import styles from '../css/orderButton.module.css';

const OrderButton = () => {
    const { user } = useContext(AuthContext);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null); // Error state
    const navigate = useNavigate();

    const handleOrderClick = async () => {
        setLoading(true);
        setError(null); // Clear any previous error

        if (!user) {
            navigate('/');
            setLoading(false);
            return;
        }

        // Prepare the order details from cart
        const cartItems = getCartItems();
        const orderDetails = {
            customerId: user.id,
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
                            productId: null,
                            price: item.price,
                            productType: "RingPairing",
                            createRingPairingDTO: {
                                ringId: item.ring.rProductId,
                                diamondId: item.diamond.dProductId
                            },
                            createPendantPairingDTO: null,
                            createEarringPairingDTO: null
                        };
                    } else if (item.productType === 'pendant') {
                        return {
                            productId: null,
                            price: item.price,
                            productType: "PendantPairing",
                            createRingPairingDTO: null,
                            createPendantPairingDTO: {
                                pendantId: item.pendant.pProductId,
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
            console.log('Order created successfully:', orderResponse);

            // Navigate to the order confirmation page
            setTimeout(() => {
                navigate('/order-confirmation');
                setLoading(false);
            }, 1000);
        } catch (error) {
            console.error('Error creating order:', error);
            setError('Failed to place order. Please try again later.');
            setLoading(false);
        }
    };

    return (
        <div>
            <button onClick={handleOrderClick} className={styles.orderButton} disabled={loading}>
                {loading ? <CircularIndeterminate size={24} /> : 'Place Order'}
            </button>
            {error && <p className={styles.errorMessage}>{error}</p>} {/* Display error message */}
        </div>
    );
};

export default OrderButton;

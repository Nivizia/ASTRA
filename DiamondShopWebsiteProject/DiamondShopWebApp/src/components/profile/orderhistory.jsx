// src/components/profile/orderhistory.jsx

import React, { useState, useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { fetchOrderHistory } from '../../../javascript/apiService';
import { AuthContext } from '../../contexts/AuthContext';

import CircularIndeterminate from '../misc/loading';

import styles from '../css/profile.module.css';

const OrderHistory = () => {
  const { user } = useContext(AuthContext);
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  useEffect(() => {
    setError(null);
    setLoading(true);

    const loadOrderHistory = async () => {
      if (user) {
        try {
          const data = await fetchOrderHistory(user.sub);
          setOrders(data);
          console.log(data);
        } catch (error) {
          setError(error.message);
        } finally {
          setLoading(false);
        }
      }
    };
    loadOrderHistory();
  }, [user]);

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!loading && orders.length === 0) {
    return <div className={styles.orderHistoryContainer}><h2>Order History</h2>
      <p>You have not ordered. Please order to see order history</p></div>;
  }

  return (
    <div className={styles.orderHistoryContainer}>
      <h2>Order History</h2>
      {loading ? <CircularIndeterminate size={56} /> : (
        orders.map((order) => (
          <div key={order.orderId} className={styles.orderCard}>
            <h3 className={styles.orderId}>Order ID: {order.orderId}</h3>
            <p className={styles.orderDetails}>Order Date: {order.orderDate.split('T')[0]}</p>
            <p className={styles.orderDetails}>Total Amount: <strong>${order.totalAmount}</strong></p>
            <p className={styles.orderDetails}>Order Status: {order.orderStatus}</p>
            <h4>Items:</h4>
            {order.orderitems.map((item) => (
              <div key={item.orderItemId} className={styles.item}>
                <p className={styles.productType}>Product Type: {item.productType === "RingPairing" ? "Ring" : item.productType === "PendantPairing" ? "Pendant" : "Diamond"}</p>
                <p className={styles.price}>Price: ${item.price}</p>
              </div>
            ))}
          </div>
        ))
      )}
    </div>
  );
};

export default OrderHistory;
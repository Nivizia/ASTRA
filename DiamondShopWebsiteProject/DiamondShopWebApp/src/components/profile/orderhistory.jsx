import React, { useState, useEffect, useContext } from 'react';
import { fetchOrderHistory } from '../../../javascript/apiService';
import { AuthContext } from '../../contexts/AuthContext';
import styles from '../css/accountdetails.module.css';

const OrderHistory = () => {
  const { user } = useContext(AuthContext);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    const loadOrderHistory = async () => {
      if (user) {
        const data = await fetchOrderHistory(user.sub);
        setOrders(data);
      }
    };
    loadOrderHistory();
  }, [user]);

  return (
    <div className={styles.orderHistoryContainer}>
      <h2>Order History</h2>
      {orders.map((order) => (
        <div key={order.orderId} className={styles.orderCard}>
          <h3 className={styles.orderId}>Order ID: {order.orderId}</h3>
          <p className={styles.orderDetails}>Order Date: {order.orderDate}</p>
          <p className={styles.orderDetails}>Total Amount: {order.totalAmount}</p>
          <p className={styles.orderDetails}>Order Status: {order.orderStatus}</p>
          <h4>Items:</h4>
            {order.orderitems.map((item) => (
              <div key={item.orderItemId} className={styles.item}>
                <p className={styles.productType}>Product Type: {item.productType}</p>
                <p className={styles.price}>Price: ${item.price}</p>
              </div>
            ))}
        </div>
      ))}
    </div>
  );
};

export default OrderHistory;

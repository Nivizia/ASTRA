import React, { useState, useEffect, useContext } from 'react';
import { fetchOrderHistory } from '../../../javascript/apiService';

// Import the AuthContext
import { AuthContext } from '../../contexts/AuthContext';

const OrderHistory = () => {
  const { user } = useContext(AuthContext);
  const [existingUser, setExistingUser] = useState(user);
  const [orders, setOrders] = useState([]);

  useEffect(() => {
    setExistingUser(user);

    const loadOrderHistory = async () => {
      const data = await fetchOrderHistory(existingUser.sub);
      setOrders(data);
    };
    loadOrderHistory();
  }, [user]);

  return (
    <div>
      <h2>Order History</h2>
      {orders.map((order) => (
        <div key={order.orderId}>
          <h3>Order ID: {order.orderId}</h3>
          <p>Order Date: {order.orderDate}</p>
          <p>Total Amount: {order.totalAmount}</p>
          <p>Order Status: {order.orderStatus}</p>
          <h4>Items:</h4>
          {order.orderitems.map((item) => (
            <div key={item.orderItemId}>
              <p>Product Type: {item.productType}</p>
              <p>Price: {item.price}</p>
            </div>
          ))}
        </div>
      ))}
    </div>
  );
};

export default OrderHistory;
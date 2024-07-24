import React, { useState, useEffect } from "react";
import axios from "axios";
import styles from "../css/StaffMenu.module.css";

const StaffMenu = () => {
  const [orders, setOrders] = useState([]);
  const [showConfirmedOnly, setShowConfirmedOnly] = useState(false);

  const fetchOrders = async (OrderFirstName, OrderLastName, OrderEmail, OrderPhone) => {
    try {
      const response = await axios.get(`http://astradiamonds.com:5212/DiamondAPI/Models/Orders/SearchOrders/${OrderFirstName}/${OrderLastName}/${OrderEmail}/${OrderPhone}`);
      setOrders(response.data);
    } catch (error) {
      console.error("Error fetching orders:", error);
    }
  };

  const handleSearch = () => {
    const OrderFirstName = document.getElementById("OrderFirstName").value;
    const OrderLastName = document.getElementById("OrderLastName").value;
    const OrderEmail = document.getElementById("OrderEmail").value;
    const OrderPhone = document.getElementById("OrderPhone").value;

    fetchOrders(OrderFirstName, OrderLastName, OrderEmail, OrderPhone);
  };

  const handleSendVerificationEmail = async (orderId) => {
    try {
      await axios.put(`http://astradiamonds.com:5212/DiamondAPI/Models/Orders/VerifyCustomer/${orderId}`);
      alert("Verification email sent!");
    } catch (error) {
      console.error("Error sending verification email:", error);
    }
  };

  useEffect(() => {
    fetchOrders("", "", "", "");
  }, []);

  return (
    <div className={styles.staffMenuContainer}>
      <h1>Staff Menu</h1>
      <div className={styles.searchInputs}>
        <input type="text" id="OrderFirstName" placeholder="First Name" className={styles.input} />
        <input type="text" id="OrderLastName" placeholder="Last Name" className={styles.input} />
        <input type="email" id="OrderEmail" placeholder="Email" className={styles.input} />
        <input type="tel" id="OrderPhone" placeholder="Phone" className={styles.input} />
        <button onClick={handleSearch} className={styles.button}>Search</button>
        <label>
          <input 
            type="checkbox" 
            checked={showConfirmedOnly} 
            onChange={() => setShowConfirmedOnly(!showConfirmedOnly)} 
          />
          Show Only Confirmed Orders
        </label>
      </div>

      {Array.isArray(orders) && orders.length > 0 ? (
        <table className={styles.orderTable}>
          <thead>
            <tr>
              <th>Order ID</th>
              <th>Customer ID</th>
              <th>Order Date</th>
              <th>Total Amount</th>
              <th>Status</th>
              <th>Order Items</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {orders
              .filter(order => !showConfirmedOnly || order.orderStatus === "Confirmed")
              .map((order, index) => (
                <tr key={index}>
                  <td>{order.orderId}</td>
                  <td>{order.customerId}</td>
                  <td>{new Date(order.orderDate).toLocaleString()}</td>
                  <td>${order.totalAmount.toFixed(2)}</td>
                  <td>{order.orderStatus}</td>
                  <td>
                    <ul className={styles.orderItemsList}>
                      {order.orderitems.map((item, itemIndex) => (
                        <li key={itemIndex}>
                          {item.productType}: ${item.price}
                        </li>
                      ))}
                    </ul>
                  </td>
                  <td>
                    {order.orderStatus === "Confirmed" && (
                      <button
                        className={styles.sendEmailButton}
                        onClick={() => handleSendVerificationEmail(order.orderId)}
                      >
                        Send Verification Email
                      </button>
                    )}
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      ) : (
        <p>No orders found.</p>
      )}
    </div>
  );
};

export default StaffMenu;

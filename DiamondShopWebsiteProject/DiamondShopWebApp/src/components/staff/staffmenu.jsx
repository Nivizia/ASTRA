import React, { useState, useEffect } from "react";
import axios from "axios";

const StaffMenu = () => {
  const [orders, setOrders] = useState([]);

  const fetchOrders = async (OrderFirstName, OrderLastName, OrderEmail, OrderPhone) => {
    try {
      const response = await axios.get(
        `/DiamondAPI/Models/Orders/SearchOrders/${OrderFirstName}/${OrderLastName}/${OrderEmail}/${OrderPhone}`
      );
      setOrders(response.data);
    } catch (error) {
      console.error("Error fetching orders:", error);
    }
  };

  const handleSearch = () => {
    const OrderFirstName = document.getElementById("OrderFirstName").value || "null";
    const OrderLastName = document.getElementById("OrderLastName").value || "null";
    const OrderEmail = document.getElementById("OrderEmail").value || "null";
    const OrderPhone = document.getElementById("OrderPhone").value || "null";

    fetchOrders(OrderFirstName, OrderLastName, OrderEmail, OrderPhone);
  };

  useEffect(() => {
    fetchOrders("null", "null", "null", "null");
  }, []);

  return (
    <div>
      <h1>Staff Menu</h1>
      <input type="text" id="OrderFirstName" placeholder="First Name" />
      <input type="text" id="OrderLastName" placeholder="Last Name" />
      <input type="email" id="OrderEmail" placeholder="Email" />
      <input type="tel" id="OrderPhone" placeholder="Phone" />
      <button onClick={handleSearch}>Search</button>

      {Array.isArray(orders) && orders.length > 0 ? (
        <ul>
          {orders.map((order, index) => (
            <li key={index}>
              Order ID: {order.orderId}, Customer: {order.orderFirstName} {order.orderLastName}
            </li>
          ))}
        </ul>
      ) : (
        <p>No orders found.</p>
      )}
    </div>
  );
};

export default StaffMenu;

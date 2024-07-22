import React, { useState } from 'react';
import axios from 'axios';

const PaymentDemo = () => {
  const [amount, setAmount] = useState(0);

  const handlePayment = async () => {
    try {
      const response = await axios.post('/api/vnpay/create-payment-url', { amount });
      if (response.data && response.data.paymentUrl) {
        window.location.href = response.data.paymentUrl;
      }
    } catch (error) {
      console.error('Error creating payment URL:', error);
    }
  };

  return (
    <div>
      <h1>Payment Demo</h1>
      <input
        type="number"
        value={amount}
        onChange={(e) => setAmount(e.target.value)}
        placeholder="Enter amount"
      />
      <button onClick={handlePayment}>Pay Now</button>
    </div>
  );
};

export default PaymentDemo;
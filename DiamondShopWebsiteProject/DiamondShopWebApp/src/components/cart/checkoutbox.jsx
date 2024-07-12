// src/components/cart/checkoutbox.jsx

import React from 'react';

import OrderButton from './orderButton';

import styles from '../css/shoppingcart.module.css';

const CheckoutBox = ({ totalPrice }) => {
  return (
    <div className={styles.checkoutBoxContainer}>
      <h2>Total Amount</h2>
      <p className={styles.cartItemInfo}>${totalPrice.toFixed(2)}</p>
      <OrderButton />
    </div>
  );
};

export default CheckoutBox;
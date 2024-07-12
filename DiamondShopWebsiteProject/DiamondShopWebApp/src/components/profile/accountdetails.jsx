// src/components/profile/accountdetails.jsx

import React, { useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import styles from '../css/accountdetails.module.css';

const AccountDetails = () => {
  const { user } = useContext(AuthContext);

  return (
    <div className={styles.container}>
      <h2>Account Details</h2>
      <p>View and manage your personal details and contact information</p>
      <div className={styles.detailsSection}>
        <div className={styles.detailsColumn}>
          <h4 className={styles.detailsTitle}>Login Details:</h4>
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>First Name:</div>
            <div className={styles.detailsValue}>{user.FirstName || '-'}</div>
          </div>
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>Last Name:</div>
            <div className={styles.detailsValue}>{user.LastName || '-'}</div>
          </div>
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>Phone:</div>
            <div className={styles.detailsValue}>{user.phone || '-'}</div>
          </div>
        </div>
        <div className={styles.detailsColumn}>
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>Email:</div>
            <div className={styles.detailsValue}>{user.Email || '-'}</div>
          </div>
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>Current Password:</div>
            <div className={styles.detailsValue}>**********</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AccountDetails;
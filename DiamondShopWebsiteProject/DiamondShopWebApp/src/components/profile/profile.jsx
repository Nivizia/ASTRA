// src/components/profile/profile.jsx

import React, { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';

import { BottomNavigation, BottomNavigationAction } from '@mui/material';

import { Person } from '@mui/icons-material';
import RestoreIcon from '@mui/icons-material/Restore';

import OrderHistory from './orderhistory.jsx';
import AccountDetails from './accountdetails.jsx';
import styles from '../css/profile.module.css';

const Profile = () => {
  const [value, setValue] = React.useState(0);
  const location = useLocation();
  const navigate = useNavigate();

  const [fromCheckout, setFromCheckout] = useState(location.state?.fromCheckout);

  const renderScreen = () => {
    switch (value) {
      case 0:
        return <AccountDetails />;
      case 1:
        return <OrderHistory />;
      default:
        return null;
    }
  };

  const handleBack = () => {
    navigate('/checkout', { state: { allowed: true } });
  }

  return (
    <div className={styles.mainContainer}>
      {fromCheckout ? (
        <div onClick={handleBack} className={styles.backButton}>Back to cart</div>
      ) : null}
      <div className={styles.profileContainer}>
        <BottomNavigation
          showLabels
          value={value}
          onChange={(event, newValue) => {
            setValue(newValue);
          }}
          className={styles.bottomNavigation}
        >
          <BottomNavigationAction label="Account Details" icon={<Person />} />
          <BottomNavigationAction label="Order History" icon={<RestoreIcon />} />
        </BottomNavigation>
        {renderScreen()}
      </div>
    </div>
  );
};

export default Profile;
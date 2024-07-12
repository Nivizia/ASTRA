// src/components/profile/profile.jsx

import React from 'react';
import BottomNavigation from '@mui/material/BottomNavigation';
import BottomNavigationAction from '@mui/material/BottomNavigationAction';
import RestoreIcon from '@mui/icons-material/Restore';
import FavoriteIcon from '@mui/icons-material/Favorite';
import OrderHistory from './orderhistory.jsx';
import AccountDetails from './accountdetails.jsx';
import styles from '../css/accountdetails.module.css';

const Profile = () => {
  const [value, setValue] = React.useState(0);

  const renderScreen = () => {
    switch (value) {
      case 0:
        return <OrderHistory />;
      case 1:
        return <AccountDetails />;
      default:
        return null;
    }
  };

  return (
    <div className={styles.profileContainer}>
      <BottomNavigation
        showLabels
        value={value}
        onChange={(event, newValue) => {
          setValue(newValue);
        }}
        className={styles.bottomNavigation}
      >
        <BottomNavigationAction label="Order History" icon={<RestoreIcon />} />
        <BottomNavigationAction label="Account Details" icon={<FavoriteIcon />} />
      </BottomNavigation>
      {renderScreen()}
    </div>
  );
};

export default Profile;
// src/components/headerAndFooter/header/header.jsx

import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';

import logo from '../../../images/logo-no-background.png';
import MenuNav from './menunavigation';
import AccountButton from './accountButton/accountButton';

import {
  Badge,
  Tooltip,
} from '@mui/material';

import '../../css/header.css';
import { getCartLength } from '../../../../javascript/cartService';

import { PiShoppingBagOpenDuotone } from "react-icons/pi";

const Header = () => {
  const [cartItemCount, setCartItemCount] = useState(0);
  const location = useLocation();

  useEffect(() => {
    // Update cart count when the location changes
    setCartItemCount(getCartLength());
  }, [location]);

  useEffect(() => {
    // Update cart count on component mount
    setCartItemCount(getCartLength());

    // Event listener for changes to local storage
    const handleStorageChange = () => {
      setCartItemCount(getCartLength());
    };

    // Add event listener for storage changes
    window.addEventListener('storage', handleStorageChange);

    // Clean up event listener on component unmount
    return () => {
      window.removeEventListener('storage', handleStorageChange);
    };
  }, []);

  return (
    <header className="header">
      <div className="header-content">
        <a href="/"><img src={logo} alt="Astra Logo" className="logo" /></a>
        <div className="nav-and-header-right-container">
          <nav className="nav">
            <MenuNav />
          </nav>
          <div className="header-right">
            <input type="text" placeholder="Search" className="search-bar" />
            <AccountButton />
            <span className="icon">
              <a href="/cart">
                <Tooltip title="Cart">
                  <Badge badgeContent={cartItemCount} color="primary">
                    <PiShoppingBagOpenDuotone />
                  </Badge>
                </Tooltip>
              </a>
            </span>
          </div>
        </div>

      </div>
    </header>
  );
};

export default Header;
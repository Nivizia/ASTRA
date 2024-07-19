// src/components/headerAndFooter/header/header.jsx

import React, { useState, useEffect, useRef } from 'react';
import { useLocation } from 'react-router-dom';

import logo from '../../../images/logo-no-background.png';
import MenuNav from './menunavigation';
import AccountButton from './accountButton/accountButton';

import { Badge, Tooltip } from '@mui/material';
import { PiShoppingBagOpenDuotone } from "react-icons/pi";

import '../../css/header.css';
import { getCartLength } from '../../../../javascript/cartService';

const Header = () => {
  const [cartItemCount, setCartItemCount] = useState(0);
  const [isMinimized, setIsMinimized] = useState(false);
  const [closeMenu, setCloseMenu] = useState(false);
  const location = useLocation();

  const handleScroll = () => {
    if (window.scrollY > 96) {
      setIsMinimized(true);
      setCloseMenu(true);
    } else if (window.scrollY < 96) {
      setIsMinimized(false);
      setCloseMenu(false);
    }
  };

  const handleGoToTop = () => {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

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

    // Add scroll event listener
    window.addEventListener('scroll', handleScroll);

    // Clean up event listener on component unmount
    return () => {
      window.removeEventListener('storage', handleStorageChange);
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  return (
    <>
      <header className={`header ${isMinimized ? 'minimizing-header' : ''}`}>
        <div className="header-content">
          <a href="/"><img src={logo} alt="Astra Logo" className="logo" /></a>
          <div className="nav-and-header-right-container">
            <nav className="nav">
              <MenuNav closeMenu={closeMenu}/>
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
      {isMinimized ?
        (<div className='header-minimized'>
          <div className="header-content">
            <div onClick={handleGoToTop} className="go-to-top-button">Go to top</div>
            <div className="nav-and-header-right-container">
              <nav className="nav nav-mini">
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
        </div>
        ) : null
      }

    </>
  );
};

export default Header;
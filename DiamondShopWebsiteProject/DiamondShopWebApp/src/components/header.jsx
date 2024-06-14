import React from 'react';
import './header.css';
import logo from '../images/logo-no-background.png';
import MenuNav from './menunavigation';

const Header = () => {
  return (
    <header className="header">
      <div className="header-content">
        <img src={logo} alt="Astra Logo" className="logo" />
        <nav className="nav">
          <MenuNav />
        </nav>
        <div className="header-right">
          <input type="text" placeholder="Search" className="search-bar" />
          <span className="icon">👤</span>
          <span className="icon">🛒</span>
        </div>
      </div>
    </header>
  );
};

export default Header;

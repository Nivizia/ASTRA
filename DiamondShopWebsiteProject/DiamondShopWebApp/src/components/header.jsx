import React from 'react';
import './header.css';
import logo from '../images/logo-no-background.png';
import MenuNav from './menunavigation';
import Account from './account';

const Header = () => {
  return (
    <header className="header">
      <div className="header-content">
        <a href="/"><img src={logo} alt="Astra Logo" className="logo" /></a>
        <nav className="nav">
          <MenuNav />
        </nav>
        <div className="header-right">
          <input type="text" placeholder="Search" className="search-bar" />
          <Account />
          <span className="icon">🛒</span>
        </div>
      </div>
    </header>
  );
};

export default Header;

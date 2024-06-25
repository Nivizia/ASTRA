import React from 'react';
import '../css/header.css';
import logo from '../../images/logo-no-background.png';
import MenuNav from '../menunavigation';
import AccountButton from '../accountButton/accountButton';

const Header = () => {
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
            <span className="icon"><a href="/cart">🛒</a></span>
          </div>
        </div>

      </div>
    </header>
  );
};

export default Header;

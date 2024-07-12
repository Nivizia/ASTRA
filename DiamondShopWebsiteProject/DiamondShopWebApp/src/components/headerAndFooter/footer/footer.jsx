// src/components/headerAndFooter/footer/footer.jsx

import React from 'react';
import '../../css/footer.css';
import logo from '../../../images/logo-no-background.png';

const Footer = () => {

  const href = "/diamond?shape="

  return (
    <footer className="footer">

      <div className="footer-content">
        <div className="footer-column-wrapper">
          <div className="footer-column">
            <h3>Diamond Shapes</h3>
            <ul className="footer-list">
              <li><a href={`${href}round`}>Round</a></li>
              <li><a href={`${href}princess`}>Princess</a></li>
              <li><a href={`${href}cushion`}>Cushion</a></li>
              <li><a href={`${href}oval`}>Oval</a></li>
              <li><a href={`${href}emerald`}>Emerald</a></li>
              <li><a href={`${href}pear`}>Pear</a></li>
              <li><a href={`${href}asscher`}>Asscher</a></li>
              <li><a href={`${href}heart`}>Heart</a></li>
              <li><a href={`${href}oval`}>Oval</a></li>
              <li><a href={`${href}emerald`}>Emerald</a></li>
            </ul>
          </div>
          <div className="footer-column">
            <h3>Customer Support</h3>
            <ul>
              <li>Contact Us</li>
              <li>FAQs</li>
            </ul>
            <h3>About ASTRA</h3>
            <ul>
              <li>About Us</li>
              <li>Services</li>
              <li>Education</li>
            </ul>
          </div>
        </div>
        <div className="footer-logo">
          <img src={logo} alt="Astra Logo" />
          <p>© 2024 ASTRA, Inc. All Rights Reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
import React from 'react';
import './footer.css';
import logo from '../images/logo-no-background.png';

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-content">
        <div className="footer-column">
          <h3>Diamond Shapes</h3>
          <ul>
            <li>Round</li>
            <li>Princess</li>
            <li>Cushion</li>
            <li>Oval</li>
            <li>Emerald</li>
            <li>Pear</li>
            <li>Asscher</li>
            <li>Heart</li>
            <li>Oval</li>
            <li>Emerald</li>
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
        <div className="footer-logo">
          <img src={logo} alt="Astra Logo" />
          <p>© 2024 ASTRA, Inc. All Rights Reserved.</p>
        </div>
      </div>
    </footer>
  );
};

export default Footer;

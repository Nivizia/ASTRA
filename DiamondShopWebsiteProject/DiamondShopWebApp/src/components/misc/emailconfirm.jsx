import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';

const EmailConfirm = () => {
  const [message, setMessage] = useState('Confirming...');
  const location = useLocation();

  useEffect(() => {
    const searchParams = new URLSearchParams(location.search);
    const token = searchParams.get('t');
    console.log(token);

    if (token) {
      fetch(`http://astradiamonds.com:5212/DiamondAPI/Models/Orders/Confirm/${token}`, {
        method: 'PUT',
      })
      .then(response => {
        if (response.ok) {
          return response.json();
        }
        throw new Error('Failed to confirm email.');
      })
      .then(data => setMessage('Your email has been successfully confirmed.'))
      .catch(error => setMessage(error.message));
    } else {
      setMessage('No token provided.');
    }
  }, [location.search]);

  return (
    <div>{message}</div>
  );
};

export default EmailConfirm;
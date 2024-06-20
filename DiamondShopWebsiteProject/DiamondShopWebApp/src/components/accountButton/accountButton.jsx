import React, { useEffect, useState } from 'react';
import AccountButtonNotLoggedIn from './accountButtonNotLoggedIn';
import AccountButtonLoggedIn from './accountButtonLoggedIn';

const AccountButton = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        setIsLoggedIn(!!token);
    }, []);

    const handleLoginSuccess = () => {
        setIsLoggedIn(true);
    };

    const handleLogout = () => {
        localStorage.removeItem('authToken');
        setIsLoggedIn(false);
    };

    return (
        <>
            {isLoggedIn ? (
                <AccountButtonLoggedIn onLogout={handleLogout} />
            ) : (
                <AccountButtonNotLoggedIn onLoginSuccess={handleLoginSuccess} />
            )}
        </>
    );
};

export default AccountButton;

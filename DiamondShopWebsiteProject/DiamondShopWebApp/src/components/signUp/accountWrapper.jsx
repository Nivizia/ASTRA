import React, { useState } from 'react';
import AccountButton from '../accountButton/accountButton';
import SignUp from './signUp';

const AccountWrapper = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const handleLoginSuccess = () => {
        setIsLoggedIn(true);
    };

    const handleLogout = () => {
        setIsLoggedIn(false);
    };

    return (
        <>
            <AccountButton isLoggedIn={isLoggedIn} onLogout={handleLogout} />
            <SignUp onLoginSuccess={handleLoginSuccess} />
        </>
    );
};

export default AccountWrapper;

// src/components/headerAndFooter/header/accountButton/accountButton.jsx

import React, { useState, useEffect, useContext } from 'react';
import { useLocation } from 'react-router-dom';
import AccountButtonNotLoggedIn from './accountButtonNotLoggedIn';
import AccountButtonLoggedIn from './accountButtonLoggedIn';
import SnackbarNotification from './SnackbarNotification';
import { AuthContext } from '../../../../contexts/AuthContext';

const AccountButton = () => {
    const { user, login, logout } = useContext(AuthContext);
    const [hasUserLoggedIn, setHasUserLoggedIn] = useState(!!user);

    //const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [snackbarOpen, setSnackbarOpen] = useState(false);
    const [snackbarMessage, setSnackbarMessage] = useState('');
    const [snackbarSeverity, setSnackbarSeverity] = useState('success');
    const location = useLocation(); // Hook to access the current location

    // Function to check if user is logged in
    const checkLoginStatus = () => {
        const token = localStorage.getItem('authToken');
        setHasUserLoggedIn(!!token);
    };

    // The below useEffect hook will process the change in state of account button 
    // (logged in/not logged in)

    useEffect(() => {
        checkLoginStatus();
    }, []); // Run the effect only once when the component mounts

    useEffect(() => {
        checkLoginStatus();
    }, [location]); // Re-run the effect when the location changes

    const handleLoginSuccess = () => {
        setHasUserLoggedIn(true);
        showSnackbar('Login successful!', 'success'); // Change snackbar message to login success
    };

    const handleLogout = () => {
        logout();
        showSnackbar('You are logged out', 'info'); // Change snackbar message to logged out
    };

    const handleSnackbarClose = () => {
        setSnackbarOpen(false);
    };

    const showSnackbar = (message, severity) => {
        setSnackbarMessage(message);
        setSnackbarSeverity(severity);
        setSnackbarOpen(true);
    };

    return (
        <>
            {hasUserLoggedIn && user ? (
                <AccountButtonLoggedIn user={user} onLogout={handleLogout} />
            ) : (
                <AccountButtonNotLoggedIn loginMethod={login} onLoginSuccess={handleLoginSuccess} />
            )}
            <SnackbarNotification
                open={snackbarOpen}
                handleClose={handleSnackbarClose}
                message={snackbarMessage}
                severity={snackbarSeverity}
            />
        </>
    );
};

export default AccountButton;
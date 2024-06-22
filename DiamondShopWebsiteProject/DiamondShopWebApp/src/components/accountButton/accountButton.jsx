// accountButton.jsx
import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import AccountButtonNotLoggedIn from './accountButtonNotLoggedIn';
import AccountButtonLoggedIn from './accountButtonLoggedIn';
import SnackbarNotification from './SnackbarNotification';

const AccountButton = () => { // Extract isLoggedInSignUp as a prop
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [snackbarOpen, setSnackbarOpen] = useState(false);
    const [snackbarMessage, setSnackbarMessage] = useState('');
    const [snackbarSeverity, setSnackbarSeverity] = useState('success');
    const location = useLocation(); // Hook to access the current location

    // Function to check if user is logged in
    const checkLoginStatus = () => {
        const token = localStorage.getItem('authToken');
        setIsLoggedIn(!!token);
    };

    useEffect(() => {
        checkLoginStatus();
    }, []);

    useEffect(() => {
        checkLoginStatus();
    }, [location]); // Re-run the effect when the location changes

    const handleLoginSuccess = () => {
        setIsLoggedIn(true);
        showSnackbar('Login successful!', 'success');
    };

    const handleLogout = () => {
        localStorage.removeItem('authToken');
        setIsLoggedIn(false);
        showSnackbar('You are logged out', 'info');
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
            {isLoggedIn ? (
                <AccountButtonLoggedIn onLogout={handleLogout} />
            ) : (
                <AccountButtonNotLoggedIn onLoginSuccess={handleLoginSuccess} />
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

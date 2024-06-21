// accountButton.jsx
import React, { useEffect, useState } from 'react';
import AccountButtonNotLoggedIn from './accountButtonNotLoggedIn';
import AccountButtonLoggedIn from './accountButtonLoggedIn';
import SnackbarNotification from './SnackbarNotification';

const AccountButton = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [snackbarOpen, setSnackbarOpen] = useState(false);
    const [snackbarMessage, setSnackbarMessage] = useState('');
    const [snackbarSeverity, setSnackbarSeverity] = useState('success');

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        setIsLoggedIn(!!token);
    }, []);

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

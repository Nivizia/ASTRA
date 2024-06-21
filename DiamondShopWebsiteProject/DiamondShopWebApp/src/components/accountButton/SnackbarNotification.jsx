// SnackbarNotification.jsx
import React from 'react';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';

const SnackbarNotification = ({ open, handleClose, message, severity = 'success' }) => {
    return (
        <Snackbar
            open={open}
            autoHideDuration={6000}
            onClose={handleClose}
            anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            sx={{ width: `${20}vw`}}
        >
            <Alert 
            onClose={handleClose} 
            severity={severity} 
            sx={{ width: '100%', fontSize: '1.1rem' }}>
                {message}
            </Alert>
        </Snackbar>
    );
};

export default SnackbarNotification;

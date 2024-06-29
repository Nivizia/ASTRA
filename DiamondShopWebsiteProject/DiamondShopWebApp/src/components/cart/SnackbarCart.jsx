// SnackbarCart.jsx
import React from 'react';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import Slide from '@mui/material/Slide';

function SlideTransition(props) {
    return <Slide {...props} direction="up" />;
  }

const SnackbarCart = ({ open, handleClose, message, severity = 'success', key }) => {
    return (
        <Snackbar
            open={open}
            autoHideDuration={6000}
            onClose={handleClose}
            TransitionComponent={SlideTransition}
            anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
            key={key}
        >
            <Alert
                onClose={handleClose}
                severity={severity}
                sx={{ minWidth: '30vw', fontSize: '100%' }}>
                {message}
            </Alert>
        </Snackbar>
    );
};

export default SnackbarCart;

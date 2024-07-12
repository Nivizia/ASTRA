// src/components/misc/errorpage.jsx

import React from 'react';
import { useNavigate } from 'react-router-dom';
import styles from '../css/errorPage.module.css'; // Adjust the path according to your project structure

const ErrorPage = () => {
    const navigate = useNavigate();

    const onRetry = () => {
        window.location.reload();
    };

    return (
        <div className={styles.errorPage}>
            <h1 className={styles.errorTitle}>Oops! Something went wrong.</h1>
            <div className={styles.actions}>
                <button className={styles.homeButton} onClick={() => navigate('/')}>
                    Go to Homepage
                </button>
                <button className={styles.retryButton} onClick={onRetry}>
                    Retry
                </button>
            </div>
        </div>
    );
};

export default ErrorPage;
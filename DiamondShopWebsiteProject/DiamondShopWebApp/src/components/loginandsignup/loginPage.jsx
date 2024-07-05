import React, { useState, useEffect, useContext } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

import CircularIndeterminate from '../misc/loading';

import { AuthContext } from '../../contexts/AuthContext';

import styles from '../css/account.module.css';

const LoginPage = () => {
    const { user, login } = useContext(AuthContext);
    const [hasUserLoggedIn, setHasUserLoggedIn] = useState(!!user);
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    const location = useLocation();
    const params = new URLSearchParams(location.search);

    const cart = params.get('cart');

    useEffect(() => {
        if (user) {
            setTimeout(() => {
                navigate('/'); // Change '/home' to your home route as needed
            }, 3000); // Waits for 3 seconds before redirecting
        }
    }, [user, navigate]);

    const handleLoginSuccess = () => {
        setHasUserLoggedIn(true);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError(null);
        setLoading(true); // Set loading to true when login starts

        try {
            const result = await login(username, password);
            if (result.success) {
                handleLoginSuccess();
                if (cart) {
                    navigate('/cart');
                } else {
                    navigate('/'); // Change '/home' to your home route as needed
                }
            } else {
                setError(result.message);
            }
        } catch (error) {
            console.error('Error during login:', error);
            setError('Server error. Please try again later.');
        } finally {
            setLoading(false); // Set loading to false when login completes
        }
    };
    return (
        <>
            {!hasUserLoggedIn && !user ? (
                <>
                    <div className={styles.backgroundContainer}></div>
                    <div className={`${styles.loginPageContainer} ${styles.container} `}>
                        <h2 className={styles.textInLoginForm}>Login</h2>
                        <form onSubmit={handleSubmit} className={styles.submitForm}>
                            <div className={styles.divInLoginForm}>
                                <label className={styles.textInLoginForm}>Username:</label>
                                <input
                                    type="text"
                                    id="username"
                                    placeholder='Enter your username'
                                    onChange={(e) => setUsername(e.target.value)}
                                    required
                                />
                            </div>
                            <div className={styles.divInLoginForm}>
                                <label className={styles.textInLoginForm}>Password:</label>
                                <input
                                    type="password"
                                    id="password"
                                    placeholder='Enter your password'
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                />
                            </div>
                            {error && <div className={styles.error}>{error}</div>}
                            <button className={styles.buttonLogin} type="submit" disabled={loading}>
                                {loading ? <CircularIndeterminate size={24} /> : 'Login'}
                            </button>
                        </form>
                        <div className={styles.footer}>
                            <p className={styles.textInLoginForm}>Don't have an account? <a className={styles.signUp} href="/signup">Sign Up</a></p>
                        </div>
                    </div>
                </>

            ) : (
                <>
                    <div className={`${styles.backgroundContainerLoggedInPage} ${styles.backgroundContainer}`}></div>
                    <div className={styles.loggedInContainer}>
                        <CircularIndeterminate size={54} />
                        <h2>You are already logged in! Redirecting you to home.</h2>
                    </div>
                </>
            )}
        </>
    );
};

export default LoginPage;

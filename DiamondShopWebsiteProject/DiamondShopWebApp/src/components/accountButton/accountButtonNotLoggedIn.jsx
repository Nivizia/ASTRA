import React, { useState } from 'react';
import { Dialog } from '@mui/material';
import { MdAccountCircle } from "react-icons/md";
import { loginUser } from '../../../javascript/apiService';
import CircularIndeterminate from '../loading';
import styles from '../css/account.module.css';

const AccountButtonNotLoggedIn = ({ onLoginSuccess }) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const [open, setOpen] = useState(false);
    const [loading, setLoading] = useState(false); // Add loading state

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError(null);
        setLoading(true); // Set loading to true when login starts

        try {
            const result = await loginUser(username, password);
            if (result.success) {
                onLoginSuccess();
                setOpen(false);
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

    const handleAccountClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <>
            <div onClick={handleAccountClickOpen}>
                <MdAccountCircle />
            </div>
            <Dialog open={open} onClose={handleClose}>
                <div className={styles.container}>
                    <h2 className={styles.textInLoginForm}>Login</h2>
                    <form onSubmit={handleSubmit}>
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
                            {loading ? <CircularIndeterminate /> : 'Login'}
                        </button>
                    </form>
                    <div className={styles.footer}>
                        <p className={styles.textInLoginForm}>Don't have an account? <a className={styles.signUp} href="/signup">Sign Up</a></p>
                    </div>
                </div>
            </Dialog>
        </>
    );
};

export default AccountButtonNotLoggedIn;

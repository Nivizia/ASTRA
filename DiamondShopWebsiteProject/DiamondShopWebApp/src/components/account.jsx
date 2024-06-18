import React from 'react'
import { useState } from 'react';

import { Dialog } from '@mui/material'

import { useNavigate } from 'react-router-dom';

import { MdAccountCircle } from "react-icons/md";

import { loginUser } from '../../apiService';
import styles from './account.module.css'

const Account = () => {

    // Login form
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate(); // Initialize navigate

    const handleSubmit = async (event) => {
        event.preventDefault(); // Prevent page reload

        setError(null); // Reset error state

        try {
            // Use the loginUser function from apiService
            const token = await loginUser(username, password);
            if (token) {
                // Redirect to /diamond after successful login
                setOpen(false);
                console.log('Login successful. Token:', token);
                navigate('/diamond');
            }
        } catch (error) {
            console.error('Error during login:', error);
            setError('Invalid username or password');
        }
    };



    // Account button
    const [open, setOpen] = useState(false);


    const handleAccountClickOpen = () => {
        setOpen(true);
    }

    const handleClose = () => {
        setOpen(false);
    }

    return (
        <>
            <div onClick={handleAccountClickOpen}><MdAccountCircle /></div>
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
                                value={username}
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
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>
                        {error && <div className={styles.error}>{error}</div>}
                        <button className={styles.buttonLogin} type="submit">Login</button>
                    </form>
                    <div className={styles.footer}>
                        <p className={styles.textInLoginForm}>Don't have an account? <a className={styles.signUp} href="/signup">Sign Up</a></p>
                    </div>
                </div>
            </Dialog>
        </>
    )
}

export default Account
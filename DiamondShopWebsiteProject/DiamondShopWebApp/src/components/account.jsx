import { Dialog } from '@mui/material'
import React from 'react'
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
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
        <div>
            <button onClick={handleAccountClickOpen}>👤</button>
            <Dialog open={open} onClose={handleClose}>
                <div className={styles.container}>
                    <h2>Login</h2>
                    <form onSubmit={handleSubmit}>
                        <div>
                            <label htmlFor="username">Username:</label>
                            <input
                                type="text"
                                id="username"
                                value={username}
                                onChange={(e) => setUsername(e.target.value)}
                                required
                            />
                        </div>
                        <div>
                            <label htmlFor="password">Password:</label>
                            <input
                                type="password"
                                id="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>
                        {error && <div className={styles.error}>{error}</div>}
                        <button type="submit">Login</button>
                    </form>
                    <div className={styles.footer}>
                        <p>Don't have an account? <a href="/signup">Sign Up</a></p>
                    </div>
                </div>
            </Dialog>
        </div>
    )
}

export default Account
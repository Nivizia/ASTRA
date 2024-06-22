import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../../javascript/apiService';
import styles from './css/account.module.css';

const LoginPage = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        setIsLoggedIn(!!token); // Convert token presence to boolean
    }, []);

    const handleSubmit = async (event) => {
        event.preventDefault();
        setError(null);

        try {
            const token = await loginUser(username, password);
            if (token) {
                console.log('Login successful. Token:', token);
                navigate('/diamond');
            }
        } catch (error) {
            console.error('Error during login:', error);
            setError('Invalid username or password');
        }
    };

    return (
        <div className={styles.container}>
            <h2 className={styles.textInLoginForm}>Login</h2>
            <form onSubmit={handleSubmit}>
                <div className={styles.divInLoginForm}>
                    <label className={styles.textInLoginForm} htmlFor="username">Username:</label>
                    <input
                        type="text"
                        id="username"
                        placeholder="Enter your username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div className={styles.divInLoginForm}>
                    <label className={styles.textInLoginForm} htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        placeholder="Enter your password"
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
    );
};

export default LoginPage;

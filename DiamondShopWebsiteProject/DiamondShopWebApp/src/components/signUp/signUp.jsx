// signUp.jsx
import React, { useEffect, useState } from 'react';
import { Navigate } from 'react-router-dom';
import { signUpUser, loginUser } from '../../../javascript/apiService'; // Import signUpUser and loginUser functions
import CircularIndeterminate from '../loading';
import styles from '../css/signUp.module.css'; // Import the CSS module

const SignUp = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false); // Add loading state
    const [redirectToIndex, setRedirectToIndex] = useState(false); // State for redirection

    const handleSignUpSuccess = async () => {
        setLoading(true); // Set loading to true when sign-up starts

        try {
            // Sign up the user
            const user = { firstName, lastName, username, password };
            const signUpResponse = await signUpUser(user);

            if (!signUpResponse.success) {
                setError(signUpResponse.message || 'Sign-up failed. Please try again.');
                setLoading(false); // Set loading to false on failure
                return;
            }

            // If sign-up was successful, proceed with login
            const loginResponse = await loginUser(username, password);

            if (loginResponse.success) {
                // Store the token and handle successful login
                localStorage.setItem('authToken', loginResponse.token);
                setRedirectToIndex(true); // Trigger redirection
            } else {
                setError(loginResponse.message || 'Login failed after sign-up. Please try logging in manually.');
            }
        } catch (err) {
            console.error('Sign-up and/or login error:', err);
            setError('Sign-up and/or login failed. Please try again.');
        } finally {
            setLoading(false); // Set loading to false after sign-up and login attempt
        }
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        setError(null);
        handleSignUpSuccess();
    };

    return (
        <div className={styles.signupContainer}>
            {redirectToIndex && <Navigate to="/" />}
            <h2 className={styles.textInLoginForm}>Sign Up</h2>
            <form className={styles.signupForm} onSubmit={handleSubmit}>
                <div className={styles.divInLoginForm}>
                    <label className={styles.signupLabel} htmlFor="firstName">First Name:</label>
                    <input
                        type="text"
                        id="firstName"
                        placeholder="Enter your first name"
                        className={styles.signupInput}
                        value={firstName}
                        onChange={(e) => setFirstName(e.target.value)}
                        required
                    />
                </div>
                <div className={styles.divInLoginForm}>
                    <label className={styles.signupLabel} htmlFor="lastName">Last Name:</label>
                    <input
                        type="text"
                        id="lastName"
                        placeholder="Enter your last name"
                        className={styles.signupInput}
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)}
                        required
                    />
                </div>
                <div className={styles.divInLoginForm}>
                    <label className={styles.signupLabel} htmlFor="username">Username:</label>
                    <input
                        type="text"
                        id="username"
                        placeholder="Enter your username"
                        className={styles.signupInput}
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div className={styles.divInLoginForm}>
                    <label className={styles.signupLabel} htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        placeholder="Enter your password"
                        className={styles.signupInput}
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                {error && <div className={styles.signupError}>{error}</div>}
                <button className={styles.signupButton} type="submit" disabled={loading}>
                    {loading ? <CircularIndeterminate size={24} /> : 'Sign Up'}
                </button>
            </form>
            <div className={styles.signupFooter}>
                <p className={styles.textInLoginForm}>
                    Already have an account? <a className={styles.signupLink} href="/login">Login</a>
                </p>
            </div>
        </div>
    );
};

export default SignUp;

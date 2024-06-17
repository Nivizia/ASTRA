import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../../apiService';

const LoginForm = () => {
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
                console.log('Login successful. Token:', token);
                navigate('/diamond');
            }
        } catch (error) {
            console.error('Error during login:', error);
            setError('Invalid username or password');
        }
    };

    return (
        <div style={{ maxWidth: '400px', margin: '0 auto', padding: '1em', border: '1px solid #ccc', borderRadius: '4px' }}>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <div style={{ marginBottom: '1em' }}>
                    <label htmlFor="username" style={{ display: 'block', marginBottom: '0.5em' }}>Username:</label>
                    <input
                        type="text"
                        id="username"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        style={{ width: '100%', padding: '0.5em', boxSizing: 'border-box' }}
                        required
                    />
                </div>
                <div style={{ marginBottom: '1em' }}>
                    <label htmlFor="password" style={{ display: 'block', marginBottom: '0.5em' }}>Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        style={{ width: '100%', padding: '0.5em', boxSizing: 'border-box' }}
                        required
                    />
                </div>
                {error && <div style={{ color: 'red', marginBottom: '1em' }}>{error}</div>}
                <button type="submit" style={{ width: '100%', padding: '0.5em', background: 'blue', color: 'white', border: 'none', borderRadius: '4px' }}>Login</button>
            </form>
        </div>
    );
};

export default LoginForm;

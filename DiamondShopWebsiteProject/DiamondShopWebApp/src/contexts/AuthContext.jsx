import React, { createContext, useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';

// Create the AuthContext
export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    // Function to login user
    const login = async (username, password) => {
        try {
            const response = await fetch(`http://localhost:5212/DiamondAPI/Models/Customer/login`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ username, password }),
            });

            if (!response.ok) {
                if (response.status === 401) {
                    return { success: false, message: 'Invalid username or password' };
                } else {
                    return { success: false, message: 'Server error. Please try again later.' };
                }
            }

            const responseJson = await response.json(); // Get the response as JSON
            const token = responseJson.token; // Extract the token string from the response object
            console.log('Login token:', token);

            // Store the token in localStorage
            localStorage.setItem('authToken', token);

            // Before decoding, check if the token exists and looks like a JWT
            if (token && token.split('.').length === 3) {
                try {
                    const userInfo = jwtDecode(token);
                    console.log(userInfo); // This will show you the structure of userInfo
                    setUser(userInfo);
                } catch (error) {
                    console.error('Error decoding token:', error);
                    // Handle the error (e.g., by logging out the user or showing an error message)
                }
            } else {
                console.error('Invalid or missing token');
                // Handle the case of an invalid or missing token appropriately
            }

            // Return success along with the token
            return { success: true, token };
        } catch (error) {
            console.error('Login error:', error);
            return { success: false, message: 'Network error. Please try again later.' };
        }
    };

    // Function to logout user
    const logout = () => {
        localStorage.removeItem('authToken');
        setUser(null);
    };

    // Function to check login status
    useEffect(() => {
        const token = localStorage.getItem('authToken');
        if (token) {
            try {
                const userInfo = jwtDecode(token);
                setUser(userInfo);
            } catch (error) {
                console.error('Error decoding token:', error);
                // Handle the error appropriately, e.g., by logging out the user
                logout(); // Assuming logout() clears the user state and any auth tokens
            }
        }
    }, []);

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

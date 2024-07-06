import React, { createContext, useState, useEffect } from 'react';
import axios from 'axios'; // Import axios
import { jwtDecode } from 'jwt-decode'; // Import jwt-decode

// Create the AuthContext
export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);

    // Function to login user
    const login = async (username, password) => {
        try {
            const response = await axios.post('http://localhost:5212/DiamondAPI/Models/Customer/login', {
                username,
                password
            });

            // Check for a successful response
            if (response.status === 200) {
                const { token } = response.data; // Assuming the token is part of the response data
                console.log('Login token:', token);

                // Store the token in localStorage
                localStorage.setItem('authToken', token);

                // Decode the token and set user state
                if (token && token.split('.').length === 3) {
                    try {
                        const userInfo = jwtDecode(token);
                        console.log(userInfo); // This will show you the structure of userInfo
                        setUser(userInfo);
                    } catch (error) {
                        console.error('Error decoding token:', error);
                        setUser(null);
                        return { success: false, message: 'Invalid token. Please log in again.' };
                    }
                } else {
                    console.error('Invalid or missing token');
                    return { success: false, message: 'Invalid token. Please log in again.' };
                }

                // Return success along with the token
                return { success: true, token };
            } else {
                // Handle different error statuses if needed
                if (response.status === 401) {
                    return { success: false, message: 'Invalid username or password' };
                } else {
                    return { success: false, message: 'Server error. Please try again later.' };
                }
            }
        } catch (error) {
            console.error('Login error:', error);
            // Handle the error properly
            if (error.response && error.response.status === 401) {
                return { success: false, message: 'Invalid username or password' };
            } else {
                return { success: false, message: 'Network error. Please try again later.' };
            }
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
                logout();
            }
        }
    }, []);

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

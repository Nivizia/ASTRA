// apiService.js
const BASE_URL = "http://localhost:5212/DiamondAPI/Models";

// Function to get the stored JWT token
const getToken = () => {
    return localStorage.getItem('authToken');
};

// Function to handle user login
// apiService.js
// apiService.js
export const loginUser = async (username, password) => {
    try {
        const response = await fetch(`${BASE_URL}/Customer/login`, {
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

        const token = await response.json(); // Assuming the token is returned as plain text
        console.log('Login token:', token);

        // Store the token in localStorage
        localStorage.setItem('authToken', token);

        // Return success along with the token
        return { success: true, token };
    } catch (error) {
        console.error('Login error:', error);
        return { success: false, message: 'Network error. Please try again later.' };
    }
};

// Function to handle user sign up
export const signUpUser = async (user) => {
    try {
        const response = await fetch(`${BASE_URL}/Customer/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(user),
        });

        if (!response.ok) {
            if (response.status === 409) {
                // Handle specific case where username already exists
                const errorData = await response.json();
                return { success: false, message: errorData.message || 'Username already exists' };
            }
            // Handle other errors
            const message = await response.json();
            throw new Error(`Sign up failed: ${message}`);
        }

        return { success: true };
    } catch (error) {
        console.error('Sign up error:', error);
        return { success: false, message: 'Network error. Please try again later.' };
    }
};

// Function to fetch all diamonds
export const fetchDiamonds = async () => {
    try {
        const response = await fetch(`${BASE_URL}/Diamond`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Function to fetch a single diamond by ID
export const fetchDiamondById = async (id) => {
    if (!id) {
        throw new Error('Diamond ID must be provided');
    }

    try {
        const response = await fetch(`${BASE_URL}/Diamond/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error(`Error fetching diamond with ID ${id}: ${response.statusText}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error in fetchDiamondById:', error);
        throw error;
    }
};


// Function to create a new diamond
export const createDiamond = async (diamond) => {
    try {
        const token = getToken(); // Retrieve the token from localStorage
        const response = await fetch(`${BASE_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}` // Include the token in the request headers
            },
            body: JSON.stringify(diamond)
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Function to update an existing diamond
export const updateDiamond = async (id, diamond) => {
    try {
        const token = getToken(); // Retrieve the token from localStorage
        const response = await fetch(`${BASE_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}` // Include the token in the request headers
            },
            body: JSON.stringify(diamond)
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Function to delete a diamond
export const deleteDiamond = async (id) => {
    try {
        const token = getToken(); // Retrieve the token from localStorage
        const response = await fetch(`${BASE_URL}/${id}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}` // Include the token in the request headers
            }
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return 'Deleted successfully';
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Ring functions:
// Function to fetch all rings
export const fetchRings = async () => {
    try {
        const response = await fetch(`${BASE_URL}/Ring`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Function to fetch a single ring by ID
export const fetchRingById = async (id) => {
    if (!id) {
        throw new Error('Ring ID must be provided');
    }

    try {
        const response = await fetch(`${BASE_URL}/Ring/${id}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error(`Error fetching ring with ID ${id}: ${response.statusText}`);
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Error in fetchRingById:', error);
        throw error;
    }
};



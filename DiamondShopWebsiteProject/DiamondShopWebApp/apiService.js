// apiService.js
const BASE_URL = "http://localhost:5212/DiamondAPI/Models";

// Function to get the stored JWT token
const getToken = () => {
    return localStorage.getItem('authToken');
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
    try {
        const response = await fetch(`${BASE_URL}/Diamond/${id}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Function to create a new diamond
export const createDiamond = async (diamond) => {
    try {
        const response = await fetch(`${BASE_URL}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
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
        const response = await fetch(`${BASE_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
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
        const response = await fetch(`${BASE_URL}/${id}`, {
            method: 'DELETE'
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return 'Deleted successfully';
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
};

// Function to handle user login
export const loginUser = async (username, password) => {
    try {
        const response = await fetch(`${BASE_URL}/Customer/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username, password })
        });

        if (!response.ok) {
            throw new Error('Login failed');
        }

        const data = await response.json();
        // Store the token in localStorage
        localStorage.setItem('authToken', data.token);
        return data.token;
    } catch (error) {
        console.error('Login error:', error);
    }
};

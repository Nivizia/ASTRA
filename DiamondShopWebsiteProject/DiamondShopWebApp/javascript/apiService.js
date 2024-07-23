// javascript/apiService.js

import axios from 'axios';

const BASE_URL = "http://astradiamonds.com:5212/DiamondAPI/Models";

// Function to get the stored JWT token
const getToken = () => {
    return localStorage.getItem('authToken');
};

// Function to handle user sign up
export const signUpUser = async (user) => {
    try {
        await axios.post(`${BASE_URL}/Customer/register`, user);
        return { success: true };
    } catch (error) {
        console.error('Sign up error:', error);
        if (error.response && error.response.status === 409) {
            // Handle specific case where username already exists
            return { success: false, message: error.response.data.message || 'Username already exists' };
        } else {
            return { success: false, message: 'Network error. Please try again later.' };
        }
    }
};

// Diamond:
// Function to fetch all diamonds
export const fetchDiamonds = async () => {
    try {
        const response = await axios.get(`${BASE_URL}/Diamond`);
        return response.data;
    } catch (error) {
        console.error('Error in fetchDiamonds:', error);
        throw error;
    }
};

// Function to fetch available diamonds
export const fetchDiamondsAvailable = async () => {
    try {
        const diamonds = await fetchDiamonds();
        return diamonds.filter(diamond => diamond.available === true);
    } catch (error) {
        console.error('Error in fetchDiamondsAvailable:', error);
        throw error;
    }
};

// Function to fetch available diamonds by suitable shapes
export const fetchAvailableDiamondsByShape = async (suitableShapes) => {
    // If suitableShapes is not an array, wrap it in an array
    if (!Array.isArray(suitableShapes)) {
        suitableShapes = [suitableShapes];
    }

    // Convert all shapes in suitableShapes to lowercase
    suitableShapes = suitableShapes.map(shape => shape.toLowerCase());

    // Check if the array is empty
    if (suitableShapes.length === 0) {
        throw new Error('A non-empty array or a single shape must be provided');
    }

    try {
        const availableDiamonds = await fetchDiamondsAvailable();
        // Convert diamond.shape to lowercase before comparison
        return availableDiamonds.filter(diamond => suitableShapes.includes(diamond.shape.toLowerCase()));
    } catch (error) {
        console.error('Error in fetchAvailableDiamondsByShape:', error);
        throw error;
    }
};

// Function to fetch available diamonds by shape, carat, color, clarity, and cut
export const fetchAvailableDiamondsByCriteria = async (shape, carat, color, clarity, cut) => {
    try {
        const diamonds = await fetchDiamondsAvailable();
        // Define tolerance levels and acceptable variations
        const caratTolerance = 0.5; // Example: Allow a 0.1 carat difference
        const acceptableShapes = [shape]; // Extend this array with similar shapes if applicable
        const acceptableColors = [color]; // Extend this array with similar colors
        const acceptableClarities = [clarity]; // Extend this array with similar clarities
        const acceptableCuts = [cut]; // Extend this array with similar cuts

        // Filter the diamonds based on the criteria with tolerances
        return diamonds.filter(diamond => {
            return acceptableShapes.includes(diamond.shape.toLowerCase())
                && Math.abs(diamond.caratWeight - carat) <= caratTolerance
                && acceptableColors.includes(diamond.color)
                && acceptableClarities.includes(diamond.clarity)
                && acceptableCuts.includes(diamond.cut);
        });
    }
    catch (error) {
        console.error('Error in fetchAvailableDiamondsByCriteria:', error);
        throw error;
    }
};

// Function to fetch a single diamond by ID
export const fetchDiamondById = async (id) => {
    if (!id) {
        throw new Error('Diamond ID must be provided');
    }

    try {
        const response = await axios.get(`${BASE_URL}/Diamond/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error in fetchDiamondById:', error);
        throw error;
    }
};

// Function to create a new diamond
export const createDiamond = async (diamond) => {
    try {
        const token = getToken();
        const response = await axios.post(`${BASE_URL}/Diamond`, diamond, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        throw error;
    }
};

// Function to update an existing diamond
export const updateDiamond = async (id, diamond) => {
    try {
        const token = getToken();
        const response = await axios.put(`${BASE_URL}/Diamond/${id}`, diamond, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return response.data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        throw error;
    }
};

// Function to delete a diamond
export const deleteDiamond = async (id) => {
    try {
        const token = getToken();
        await axios.delete(`${BASE_URL}/Diamond/${id}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        return 'Deleted successfully';
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        throw error;
    }
};

// Diamond price calculator
export const calculateDiamondPrice = async (carat, color, clarity, cut) => {
    try {
        if (!carat || !color || !clarity || !cut) {
            throw new Error('Carat, color, clarity, and cut must be provided');
        }

        // Construct the query parameters
        const params = {
            carat,
            color,
            clarity,
            cut
        };

        // Make the GET request with query parameters
        const response = await axios.get(`${BASE_URL}/Diamond/GetPrice`, { params });

        // Return the price from the response data
        return response.data;

    } catch (error) {
        console.error('Error in calculateDiamondPrice:', error);
        throw error;
    }
};

// Diamond price per carat calculator
export const calculateDiamondPricePerCarat = async (carat, color, clarity, cut) => {
    try {
        if (!carat || !color || !clarity || !cut) {
            throw new Error('Carat, color, clarity, and cut must be provided');
        }

        // Construct the query parameters
        const params = {
            carat,
            color,
            clarity,
            cut
        };

        // Make the GET request with query parameters
        const response = await axios.get(`${BASE_URL}/Diamond/GetPricePerCarat`, { params });

        // Return the price per carat from the response data
        return response.data;

    } catch (error) {
        console.error('Error in calculateDiamondPricePerCarat:', error);
        throw error;
    }
};

// Ring:
// Function to fetch all rings
export const fetchRings = async () => {
    try {
        const response = await axios.get(`${BASE_URL}/Ring`);
        return response.data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        throw error;
    }
};

// Function to fetch a single ring by ID
export const fetchRingById = async (id) => {
    if (!id) {
        throw new Error('Ring ID must be provided');
    }

    try {
        const response = await axios.get(`${BASE_URL}/Ring/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error in fetchRingById:', error);
        throw error;
    }
};

// Function to fetch rings by shape
export const fetchRingsByShape = async (shape) => {
    if (!shape) {
        throw new Error('Shape must be provided');
    }

    try {
        const rings = await fetchRings();
        return rings.filter(ring => ring.shapes.includes(shape));
    } catch (error) {
        console.error('Error in fetchRingsByShape:', error);
        throw error;
    }
};

// Pendant:
// Function to fetch all pendants
export const fetchPendants = async () => {
    try {
        const response = await axios.get(`${BASE_URL}/Pendant`);
        return response.data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        throw error;
    }
};

// Function to fetch a single pendant by ID
export const fetchPendantById = async (id) => {
    if (!id) {
        throw new Error('Pendant ID must be provided');
    }

    try {
        const response = await axios.get(`${BASE_URL}/Pendant/${id}`);
        return response.data;
    } catch (error) {
        console.error('Error in fetchPendantById:', error);
        throw error;
    }
};

// Order:
// Function to create a new order
export const createOrder = async (orderDetails) => {
    try {
        const token = getToken();
        const response = await axios.post(`${BASE_URL}/Orders`, orderDetails, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });
        return { success: true, data: response.data };
    } catch (error) {
        console.error('Error creating order:', error);

        if (error.response) {
            // Handle 404 Not Found specifically
            if (error.response.status === 404) {
                return { success: false, message: 'User not found', noUser: true };
            }
            // Handle other HTTP errors
            return {
                success: false,
                message: error.response.data.message || 'An error occurred. Please try again later.'
            };
        } else {
            // Handle errors without a response (network errors, etc.)
            return {
                success: false,
                message: 'Failed to place order. Please check your connection and try again.'
            };
        }
    }
};

//Function to return a list of order history
export const fetchOrderHistory = async (CustomerID) => {
    try {
        const response = await axios.get(`${BASE_URL}/Orders/OrderHistory/${CustomerID}`);
        return response.data;
    } catch (error) {
        console.error('Error in fetchOrderHistory:', error);
        throw error;
    }
};

// Customer
// Function to update customer
export const updateCustomer = async (customerId, customerDetails) => {
    try {
        console.log(customerId);
        console.log('Updating customer with details:', customerDetails); // Add this line
        const response = await axios.put(`${BASE_URL}/Customer/${customerId}`, customerDetails);
        return response.data;
    } catch (error) {
        console.error('Error in updateCustomer:', error);
        throw error;
    }
};

export const createVNpayPaymentUrl = async (orderId, isDeposit) => {
    try {
        const response = await axios.post(`${BASE_URL}/VNPay/create-payment-url`, { orderId, isDeposit }, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        
        return response.data;
    } catch (error) {
        throw error;
    }
};

export const sendPaymentResponse = async (orderDetails) => {
    try {
        const response = await axios.post(`${BASE_URL}/VNPay/return`, orderDetails);
        console.log('Payment response sent successfully:', response.data);
    } catch (error) {
        console.error('Error sending payment response:', error);
    }
};
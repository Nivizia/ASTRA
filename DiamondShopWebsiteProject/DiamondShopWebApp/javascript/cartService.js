// Local storage functions for cart management:

// Adding an item to the cart
export const addToCart = (item) => {
    // Retrieve the current cart items from local storage
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Add the new item to the cart
    cart.push(item);

    // Save the updated cart back to local storage
    localStorage.setItem('cart', JSON.stringify(cart));
};

// Getting the cart items
export const getCartItems = () => {
    // Get the cart items from local storage
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    return cart;
};

// Removing an item from the cart
export const removeFromCart = (itemId) => {
    // Retrieve the current cart items from local storage
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    console.log('Current cart items before removal:', cart);

    // Filter out the item with the specified id
    cart = cart.filter(item => item.details.dProductId !== itemId);
    console.log('Updated cart items after removal:', cart);

    // Save the updated cart back to local storage
    localStorage.setItem('cart', JSON.stringify(cart));
};


// Clearing the cart
export const clearCart = () => {
    localStorage.removeItem('cart');
};

// Getting the length of the cart
export const getCartLength = () => {
    // Retrieve the current cart items from local storage
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    // Return the length of the cart array
    return cart.length;
};

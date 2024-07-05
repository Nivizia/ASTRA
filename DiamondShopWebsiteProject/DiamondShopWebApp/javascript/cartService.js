// Adding an item to the cart
export const addToCart = (item) => {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    if (item.type === 'pairing') {
        // Remove any loose diamond that matches the diamond in the pairing
        cart = cart.filter(cartItem => {
            if (cartItem.type === 'diamond') {
                return cartItem.details.dProductId !== item.diamond.dProductId;
            }
            return true;
        });

        // Remove any existing pairing that includes the same diamond
        cart = cart.filter(cartItem => {
            if (cartItem.type === 'pairing') {
                return cartItem.diamond.dProductId !== item.diamond.dProductId;
            }
            return true;
        });

        // Check if the pairing is already in the cart
        const pairingAlreadyInCart = cart.some(cartItem => cartItem.pId === item.pId);

        // If the pairing is not already in the cart, add it
        if (!pairingAlreadyInCart) {
            cart.push(item);
        }
    } else if (item.type === 'diamond') {
        // Check if the diamond is already in the cart
        const diamondAlreadyInCart = cart.some(cartItem =>
            cartItem.type === 'diamond' &&
            cartItem.details &&
            item.details &&
            cartItem.details.dProductId === item.details.dProductId
        );

        // Check if the diamond is part of any pairing already in the cart
        const diamondInPairing = cart.some(cartItem =>
            cartItem.type === 'pairing' &&
            cartItem.diamond &&
            cartItem.diamond.dProductId === item.details.dProductId
        );

        // If the diamond is not already in the cart or part of a pairing, add it
        if (!diamondAlreadyInCart && !diamondInPairing) {
            cart.push(item);
        }
    }
    // Save the updated cart back to local storage
    localStorage.setItem('cart', JSON.stringify(cart));
};

// Removing an item from the cart
export const removeFromCart = (itemId, itemType) => {
    // Retrieve the current cart items from local storage
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    console.log('Current cart items before removal:', cart);

    // Filter out the item with the specified id and type
    cart = cart.filter(item => {
        if (itemType === 'pairing') {
            return item.pId !== itemId;
        } else if (itemType === 'diamond') {
            return item.details?.dProductId !== itemId;
        }
        return true; // Default case if item type is not recognized
    });

    console.log('Updated cart items after removal:', cart);

    // Save the updated cart back to local storage
    localStorage.setItem('cart', JSON.stringify(cart));
    // Trigger the storage event manually
    window.dispatchEvent(new Event('storage'));
};

// Clearing the cart
export const clearCart = () => {
    localStorage.removeItem('cart');
    // Trigger the storage event manually
    window.dispatchEvent(new Event('storage'));
};

// Getting the cart items
export const getCartItems = () => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    return cart;
};

// Getting the length of the cart
export const getCartLength = () => {
    // Retrieve the current cart items from local storage
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    // Return the length of the cart array
    return cart.length;
};

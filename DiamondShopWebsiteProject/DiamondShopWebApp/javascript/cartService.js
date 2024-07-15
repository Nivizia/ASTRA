// Adding an item to the cart
export const addToCart = (item, chooseAnother, oldDiamondId, oldRingId, oldPendantId) => {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    if (item.type === 'pairing') {
        // Check if a pairing with the same diamond already exists
        const existingPairingIndex = cart.findIndex(cartItem =>
            cartItem.type === 'pairing' &&
            cartItem.diamond.dProductId === item.diamond.dProductId
        );

        if (!chooseAnother && (!oldDiamondId || !oldRingId || !oldPendantId)) {
            // If a pairing with the same diamond exists, update it; otherwise, add new pairing
            if (existingPairingIndex !== -1) {
                cart[existingPairingIndex] = item;
                console.log("Updated the pairing in the cart");
                console.log("Existing pairing index: " + existingPairingIndex);
            } else {
                cart.push(item);
                console.log("Added a new pairing to the cart");
                console.log("Existing pairing index: " + existingPairingIndex);
            }
        } else {
            // If the user chooses another diamond, replace the existing pairing
            const oldPairingIndex = getExistingPairingIndex(oldDiamondId, oldRingId, oldPendantId);
            cart[oldPairingIndex] = item;
            console.log("Replaced the existing pairing from the cart");
            console.log("Existing pairing index: " + oldPairingIndex);
        }


        // Remove any loose diamond that matches the diamond in the pairing
        cart = cart.filter(cartItem => {
            if (cartItem.type === 'diamond') {
                return cartItem.details.dProductId !== item.diamond.dProductId;
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

// Getting the index of an existing pairing in the cart with diamondId, ringId, or pendantId
export const getExistingPairingIndex = (diamondId, ringId, pendantId) => {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    return cart.findIndex(item =>
        item.type === 'pairing' && (
            (item.diamond?.dProductId === diamondId) ||
            (item.ring?.rProductId === ringId) ||
            (item.pendant?.pProductId === pendantId)
        )
    );
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

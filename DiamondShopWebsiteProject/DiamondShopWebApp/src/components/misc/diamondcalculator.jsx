import React, { useState } from 'react';
import { calculateDiamondPrice, calculateDiamondPricePerCarat } from '../../../javascript/apiService';

const DiamondPriceCalculator = () => {
    const [carat, setCarat] = useState('');
    const [color, setColor] = useState('');
    const [clarity, setClarity] = useState('');
    const [cut, setCut] = useState('');
    const [price, setPrice] = useState(null);
    const [pricePerCarat, setPricePerCarat] = useState(null);
    const [error, setError] = useState(null);

    const handleCalculatePrice = async () => {
        try {
            const result = await calculateDiamondPrice(carat, color, clarity, cut);
            setPrice(result);
        } catch (err) {
            setError(err.message);
        }
    };

    const handleCalculatePricePerCarat = async () => {
        try {
            const result = await calculateDiamondPricePerCarat(carat, color, clarity, cut);
            setPricePerCarat(result);
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div>
            <h1>Diamond Price Calculator</h1>
            <input type="number" placeholder="Carat" value={carat} onChange={e => setCarat(e.target.value)} />
            <input type="text" placeholder="Color" value={color} onChange={e => setColor(e.target.value)} />
            <input type="text" placeholder="Clarity" value={clarity} onChange={e => setClarity(e.target.value)} />
            <input type="text" placeholder="Cut" value={cut} onChange={e => setCut(e.target.value)} />
            <button onClick={handleCalculatePrice}>Calculate Price</button>
            <button onClick={handleCalculatePricePerCarat}>Calculate Price Per Carat</button>
            {price !== null && <p>Total Price: ${price.toFixed(2)}</p>}
            {pricePerCarat !== null && <p>Price Per Carat: ${pricePerCarat.toFixed(2)}</p>}
            {error && <p>Error: {error}</p>}
        </div>
    );
};

export default DiamondPriceCalculator;
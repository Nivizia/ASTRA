import React, { useState } from 'react';
import { calculateDiamondPrice, calculateDiamondPricePerCarat } from '../../../javascript/apiService';
import styles from "../css/diamondcalculator.module.css";

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
            setError(null);
        } catch (err) {
            setError(err.message);
        }
    };

    const handleCalculatePricePerCarat = async () => {
        try {
            const result = await calculateDiamondPricePerCarat(carat, color, clarity, cut);
            setPricePerCarat(result);
            setError(null);
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className={styles.container}>
            <h1 className={styles.title}>Diamond Price Calculator</h1>
            <input
                type="number"
                placeholder="Carat"
                value={carat}
                onChange={e => setCarat(e.target.value)}
                className={styles.inputField}
            />
            <input
                type="text"
                placeholder="Color"
                value={color}
                onChange={e => setColor(e.target.value)}
                className={styles.inputField}
            />
            <input
                type="text"
                placeholder="Clarity"
                value={clarity}
                onChange={e => setClarity(e.target.value)}
                className={styles.inputField}
            />
            <input
                type="text"
                placeholder="Cut"
                value={cut}
                onChange={e => setCut(e.target.value)}
                className={styles.inputField}
            />
            <button
                onClick={handleCalculatePrice}
                className={`${styles.button} ${styles.calculateButton}`}
            >
                Calculate Price
            </button>
            <button
                onClick={handleCalculatePricePerCarat}
                className={`${styles.button} ${styles.calculatePerCaratButton}`}
            >
                Calculate Price Per Carat
            </button>
            {price !== null && <p className={styles.result}>Total Price: ${price.toFixed(2)}</p>}
            {pricePerCarat !== null && <p className={styles.result}>Price Per Carat: ${pricePerCarat.toFixed(2)}</p>}
            {error && <p className={styles.error}>Error: {error}</p>}
        </div>
    );
};

export default DiamondPriceCalculator;
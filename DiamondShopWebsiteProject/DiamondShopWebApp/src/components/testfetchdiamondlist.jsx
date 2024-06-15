import React, { useEffect, useState } from 'react';
import { fetchDiamonds } from '../../apiService';

const DiamondList = () => {
    const [diamonds, setDiamonds] = useState([]); // Initialize as an empty array
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        async function getDiamonds() {
            try {
                const data = await fetchDiamonds();
                if (Array.isArray(data)) {
                    setDiamonds(data);
                } else {
                    setError("Unexpected data format");
                }
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        }
        getDiamonds();
    }, []);

    if (loading) {
        return <p>Loading...</p>;
    }

    if (error) {
        return <p>Error: {error}</p>;
    }

    // Check if diamonds is an array before using map
    if (!Array.isArray(diamonds)) {
        return <p>Data is not available</p>;
    }

    return (
        <div>
            <h1>Diamonds</h1>
            <ul>
                {diamonds.map(diamond => (
                    <li key={diamond.dProductId}>{diamond.name}</li>
                ))}
            </ul>
        </div>
    );
};

export default DiamondList;

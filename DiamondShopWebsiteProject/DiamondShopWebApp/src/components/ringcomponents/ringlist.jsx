import React, { useEffect, useState } from 'react';
import { fetchRings } from '../../../javascript/apiService';

import CircularIndeterminate from '../loading';
import RingBox from './ringbox';

import '../css/diamond.css';

const RingList = () => {
  const [rings, setRings] = useState([]); // Initialize as an empty array
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function getRings() {
      try {
        const data = await fetchRings();
        if (Array.isArray(data)) {
            setRings(data);
        } else {
          setError("Unable to fetch diamond");
        }
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getRings();
  }, []);

  if (loading) {
    return <CircularIndeterminate size={56} />;
  }

  if (error) {
    return <p>Network error: {error}</p>;
  }

  // Check if diamonds is an array before using map
  if (!Array.isArray(rings)) {
    return <p>Data is not available</p>;
  }

  return (
    <div>
      <div className="ring-list">
        {rings.map((ring) => (
          <RingBox
            key={ring.ringId}
            ringId={ring.ringId}
            name={ring.name}
            price={ring.price}
            stockQuantity={ring.stockQuantity}
            imageUrl={ring.imageUrl}
            metalType={ring.metalType}
            ringSize={ring.ringSize}
          />
        ))}
      </div>
    </div>
  );
};

export default RingList;

import React, { useEffect, useState } from 'react';
import { fetchPendants } from '../../../javascript/apiService';

import CircularIndeterminate from '../loading';
import RingBox from './ringbox';

import '../css/diamond.css';

const RingList = () => {
  const [pendants, setPendants] = useState([]); // Initialize as an empty array
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function getPendants() {
      try {
        const data = await fetchPendants();
        if (Array.isArray(data)) {
            setPendants(data);
        } else {
          setError("Unable to fetch pendants");
        }
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getPendants();
  }, []);

  if (loading) {
    return <CircularIndeterminate size={56} />;
  }

  if (error) {
    return <p>Network error: {error}</p>;
  }

  // Check if diamonds is an array before using map
  if (!Array.isArray(pendants)) {
    return <p>Data is not available</p>;
  }

  return (
    <div>
      <div className="diamond-list">
        {pendants.map((pendant) => (
          <RingBox
            key={pendant.pendantId}
            ringId={pendant.pendantId}
            name={pendant.name}
            price={pendant.price}
            stockQuantity={pendant.stockQuantity}
            imageUrl={pendant.imageUrl}
            metalType={pendant.metalType}
          />
        ))}
      </div>
    </div>
  );
};

export default RingList;

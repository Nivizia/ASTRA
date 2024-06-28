import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchRings } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../loading';
import RingBox from './ringbox';

import '../../css/product.css';

const RingList = () => {
  const [rings, setRings] = useState([]); // Initialize as an empty array
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const { diamondId, ringId } = useParams();

  useEffect(() => {
    async function getRings() {
      try {
        const data = await fetchRings();
        if (Array.isArray(data)) {
          setRings(data);
        } else {
          setError("Unable to fetch rings");
        }
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getRings();
    console.log(diamondId, ringId);
  }, [diamondId, ringId]);

  if (loading) {
    return <CircularIndeterminate size={56} />;
  }

  if (error) {
    return <p>Network error: {error}</p>;
  }

  // Check if rings is an array before using map
  if (!Array.isArray(rings)) {
    return <p>Data is not available</p>;
  }

  // Check if there are no ring in the array
  if (rings.length === 0) {
    return <p>There's no pendant</p>;
  }

  return (
    <div>
      <div className="diamond-list">
        {rings.map((ring) => (
          diamondId && !ringId ? (
            // Condition for diamondId is defined and ringId is undefined
            <RingBox
              key={ring.ringId}
              ringId={ring.ringId}
              diamondId={diamondId} // Passed from the parent component or context
              // ringId is intentionally not passed here based on your condition
              name={ring.name}
              price={ring.price}
              stockQuantity={ring.stockQuantity}
              imageUrl={ring.imageUrl}
              metalType={ring.metalType}
              ringSize={ring.ringSize}
            />
          ) : (
            // Default case, assuming ring-first route or any other case
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
          )
        ))}
      </div>
    </div>
  );
};

export default RingList;

import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchPendants } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../misc/loading';
import PendantBox from './pendantbox';

import '../../css/product.css';

const PendantList = () => {
  const [pendants, setPendants] = useState([]); // Initialize as an empty array
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const { diamondId, pendantId } = useParams();

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
    console.log(`diamondId: ${diamondId}, ringId: ${pendantId}`);
  }, [diamondId, pendantId]);

  if (loading) {
    return <CircularIndeterminate size={56} />;
  }

  if (error) {
    return <p>Network error: {error}</p>;
  }

  // Check if pendants is an array before using map
  if (!Array.isArray(pendants)) {
    return <p>Data is not available</p>;
  }

  // Check if there are no pendant in the array
  if (pendants.length === 0) {
    return <p>There's no pendant</p>;
  }

  return (
    <div>
      <div className="diamond-list">
        {pendants.map((pendant) => (
          
          diamondId ? (
            // Condition for diamondId is defined and pendantId is undefined
            <PendantBox
              key={pendant.pendantId}
              pendantId={pendant.pendantId}
              diamondId={diamondId} // Passed from the parent component or context
              // pendantId is intentionally not passed here based on your condition
              name={pendant.name}
              price={pendant.price}
              stockQuantity={pendant.stockQuantity}
              imageUrl={pendant.imageUrl}
              metalType={pendant.metalType}
            />
          ) : (
            // Default case, assuming pendant-first route or any other case
            <PendantBox
              key={pendant.pendantId}
              pendantId={pendant.pendantId}
              name={pendant.name}
              price={pendant.price}
              stockQuantity={pendant.stockQuantity}
              imageUrl={pendant.imageUrl}
              metalType={pendant.metalType}
            />
          )
        ))}
      </div>
    </div>
  );
};

export default PendantList;

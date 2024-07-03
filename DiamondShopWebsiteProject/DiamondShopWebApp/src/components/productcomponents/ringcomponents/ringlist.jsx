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
          console.log(data);
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

  const getRingName = (ring) => {
    let RingName = '';
  
    if (ring.ringType === 'Solitaire') {
      RingName = `${ring.ringSubtype} ${ring.frameType} ${ring.ringType} Engagement Ring in ${ring.metalType}`;
    } else if (ring.ringType === 'Halo') {
      RingName = `${ring.ringSubtype} ${ring.ringType} Diamond Engagement Ring in ${ring.metalType}`;
    } else if (ring.ringType === 'Sapphire Sidestone') {
      RingName = `${ring.ringSubtype} Sapphire and Diamond Engagement Ring in ${ring.metalType}`;
    } else if (ring.ringType === 'Three-Stone') {
      RingName = `${ring.ringSubtype} ${ring.ringType} Diamond Engagement Ring in ${ring.metalType}`;
    }
  
    // Add optional attributes like stoneCut or specialFeatures
    if (ring.stoneCut) {
      RingName = `${ring.stoneCut} ${RingName}`;
    }
    if (ring.specialFeatures) {
      RingName = `${RingName} featuring ${ring.specialFeatures}`;
    }
  
    return RingName;
  };

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
              name={getRingName(ring)}
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
              name={getRingName(ring)}
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

import React, { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import { fetchRings, fetchRingsByShape } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../misc/loading';
import RingBox from './ringbox';

import '../../css/product.css';

const RingList = () => {
  const [rings, setRings] = useState([]); // Initialize as an empty array
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const { diamondId, ringId } = useParams();

  const location = useLocation();
  const params = new URLSearchParams(location.search);

  const shape = params.get('shape');

  useEffect(() => {
    async function getRings() {
      try {
        let data;
        if (diamondId) {
          data = await fetchRingsByShape(shape);
        } else {
          data = await fetchRings();
        }
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
    console.log(`diamondId: ${diamondId}, ringId: ${ringId}`);
  }, [diamondId, shape]);

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

    // Helper function to return non-null values or an empty string
    const safeValue = (value) => value ? value : '';

    // Build the ring name based on the type
    if (ring.ringType === 'Solitaire') {
      RingName = `${safeValue(ring.ringSubtype)} ${safeValue(ring.frameType)} ${safeValue(ring.ringType)} Engagement Ring in ${safeValue(ring.metalType)}`.trim();
    } else if (ring.ringType === 'Halo') {
      RingName = `${safeValue(ring.ringSubtype)} ${safeValue(ring.ringType)} Diamond Engagement Ring in ${safeValue(ring.metalType)}`.trim();
    } else if (ring.ringType === 'Sapphire sidestone') {
      RingName = `${safeValue(ring.ringSubtype)} Sapphire and Diamond Engagement Ring in ${safeValue(ring.metalType)}`.trim();
    } else if (ring.ringType === 'Three-stone') {
      RingName = `${safeValue(ring.ringSubtype)} ${safeValue(ring.ringType)} Diamond Engagement Ring in ${safeValue(ring.metalType)}`.trim();
    }

    // Add optional attributes like stoneCut or specialFeatures
    if (ring.stoneCut) {
      RingName = `${ring.stoneCut} ${RingName}`.trim();
    }
    if (ring.specialFeatures) {
      RingName = `${RingName} featuring ${ring.specialFeatures}`.trim();
    }

    // Remove any extra spaces
    RingName = RingName.replace(/\s+/g, ' ').trim();

    return RingName;
  };

  return (
    <div className="product-list">
      {rings.map((ring) => (

        diamondId ? (
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
  );
};

export default RingList;

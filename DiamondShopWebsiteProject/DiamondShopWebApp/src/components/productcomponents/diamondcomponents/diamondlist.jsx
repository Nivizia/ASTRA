import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchAvailableDiamondsByShape, fetchDiamondsAvailable, fetchRingById } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../misc/loading';
import DiamondBox from './diamondbox';

import '../../css/product.css';


const DiamondList = () => {
  const { diamondId, ringId, pendantId } = useParams();
  const [diamonds, setDiamonds] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function getDiamonds() {
      try {
        let data;
        if (ringId) {
          const ring = await fetchRingById(ringId);
          console.log("Ring route");
          data = await fetchAvailableDiamondsByShape(ring.shapes);
        } else if (pendantId) {
          console.log("Pendant route");
          data = await fetchDiamondsAvailable();
        } else {
          console.log("Diamond route");
          data = await fetchDiamondsAvailable();
        }
        if (Array.isArray(data)) {
          setDiamonds(data);
        } else {
          setError("Unable to fetch diamond");
        }
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getDiamonds();
    console.log(`diamondId: ${diamondId}, ringId: ${ringId}`);
  }, []);

  if (loading) {
    return <CircularIndeterminate size={56} />;
  }

  if (error) {
    return <p>Network error: {error}</p>;
  }

  // Check if diamonds is an array before using map
  if (!Array.isArray(diamonds)) {
    return <p>Data is not available</p>;
  }

  // Check if there are no diamond in the array
  if (diamonds.length === 0) {
    return <p>There's no diamond</p>;
  }

  return (
    <div>
      <div className="diamond-list">
        {diamonds.map((diamond) => (
          ringId ? (
            // Condition for ringId is defined
            <DiamondBox
              key={diamond.dProductId}
              diamondId={diamond.dProductId}
              ringId={ringId} // Passed from the parent component or context
              price={diamond.price}
              imageUrl={diamond.imageUrl}
              caratWeight={diamond.caratWeight}
              color={diamond.color}
              clarity={diamond.clarity}
              cut={diamond.cut}
              shape={diamond.shape}
              // diamondId is intentionally passed here based on your condition
            />
          ) : pendantId ? (
            // Condition for pendantId is defined
            <DiamondBox
              key={diamond.dProductId}
              diamondId={diamond.dProductId}
              pendantId={pendantId} // Passed from the parent component or context
              price={diamond.price}
              imageUrl={diamond.imageUrl}
              caratWeight={diamond.caratWeight}
              color={diamond.color}
              clarity={diamond.clarity}
              cut={diamond.cut}
              shape={diamond.shape}
              // diamondId is intentionally passed here based on your condition
            />
          ) : (
            // Default case, assuming no ringId or pendantId
            <DiamondBox
              key={diamond.dProductId}
              diamondId={diamond.dProductId}
              price={diamond.price}
              imageUrl={diamond.imageUrl}
              caratWeight={diamond.caratWeight}
              color={diamond.color}
              clarity={diamond.clarity}
              cut={diamond.cut}
              shape={diamond.shape}
              // Neither ringId nor pendantId is passed here
            />
          )
        ))}
      </div>
    </div>
  );
};

export default DiamondList;

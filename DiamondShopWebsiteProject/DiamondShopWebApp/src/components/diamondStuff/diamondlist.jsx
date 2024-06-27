import React, { useEffect, useState } from 'react';
import { fetchDiamonds } from '../../../javascript/apiService';

import CircularIndeterminate from '../loading';
import DiamondBox from './diamondbox';

import '../css/productbox.css';

const DiamondList = () => {
  const [diamonds, setDiamonds] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function getDiamonds() {
      try {
        const data = await fetchDiamonds();
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
          <DiamondBox
            key={diamond.dProductId}
            id={diamond.dProductId}
            price={diamond.price}
            imageUrl={diamond.imageUrl}

            caratWeight={diamond.caratWeight}
            color={diamond.color}
            clarity={diamond.clarity}
            cut={diamond.cut}
            shape={diamond.dType}
          />
        ))}
      </div>
    </div>
  );
};

export default DiamondList;

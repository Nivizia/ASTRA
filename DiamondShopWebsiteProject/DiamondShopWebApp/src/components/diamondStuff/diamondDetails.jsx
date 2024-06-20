import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchDiamondById } from '../../../javascript/apiService';
import CircularIndeterminate from '../loading';

import '../css/diamond.css';

const DiamondDetails = () => {
  const { diamondId } = useParams();
  const [diamond, setDiamond] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function getDiamond() {
      try {
        const data = await fetchDiamondById(diamondId);
        setDiamond(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getDiamond();
  }, [diamondId]);

  if (loading) {
    return <CircularIndeterminate />;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!diamond) {
    return <p>Diamond not found.</p>;
  }

  return (
    <div className="diamond-details-page">
      <img src='/src/images/diamond.png' alt="Diamond" className="diamond-details-image" />
      <div className="diamond-details-info">
      <h2>{`${diamond.caratWeight} Carat ${diamond.color}-${diamond.clarity} ${diamond.cut} cut ${diamond.dType} Diamond`}</h2>
        <p>Carat: {diamond.caratWeight}</p>
        <p>Color: {diamond.color}</p>
        <p>Clarity: {diamond.clarity}</p>
        <p>Cut: {diamond.cut}</p>
        <p>Shape: {diamond.dType}</p>
        <p className="price">${diamond.price.toFixed(2)}</p>
      </div>
    </div>
  );
};

export default DiamondDetails;

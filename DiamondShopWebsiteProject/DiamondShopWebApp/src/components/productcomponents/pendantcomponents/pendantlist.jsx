// src/components/productcomponents/pendantcomponents/pendantlist.jsx

import React, { useEffect, useState } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import { fetchPendants } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../misc/loading';
import PendantBox from './pendantbox';

import '../../css/product.css';

const PendantList = () => {
  const { diamondId, pendantId } = useParams();
  const [pendants, setPendants] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const location = useLocation();
  const [chooseAnother, setChooseAnother] = useState(location.state?.chooseAnother);
  const [oldPendantId, setOldPendantId] = useState(location.state?.oldPendantId);

  useEffect(() => {
    setError(null);
    setLoading(true);

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
    console.log(`diamondId: ${diamondId}, pendantId: ${pendantId}`);
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
    <div className="product-list">
      {pendants.map((pendant) => (

        diamondId ? (
          // Condition for diamondId is defined and pendantId is undefined (diamond-first route)
          <PendantBox
            key={pendant.pendantId}
            pendantId={pendant.pendantId}
            diamondId={diamondId}
            name={pendant.name}
            price={pendant.price}
            stockQuantity={pendant.stockQuantity}
            imageUrl={pendant.imageUrl}
            metalType={pendant.metalType}
            chooseAnother={chooseAnother}
            oldPendantId={oldPendantId}
          />
        ) : (
          // Default case, assuming pendant-first route or any other case (pendant route)
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
  );
};

export default PendantList;
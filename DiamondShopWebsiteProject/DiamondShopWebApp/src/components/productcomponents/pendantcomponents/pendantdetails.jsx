// src/components/productcomponents/pendantcomponents/pendantdetails.jsx

import React, { useEffect, useState } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { fetchPendantById } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../misc/loading';
import Button from '@mui/material/Button';

import '../../css/product.css';
import styles from "../../css/temporarydrawer.module.css";

const PendantDetails = () => {
  const { diamondId, pendantId } = useParams();

  const [pendant, setPendant] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const location = useLocation();
  const navigate = useNavigate();
  const [fromCart, setFromCart] = useState(location.state?.fromCart);
  const [chooseAnother, setChooseAnother] = useState(location.state?.chooseAnother);
  const [oldPendantId, setOldPendantId] = useState(location.state?.oldPendantId);

  const handleSelectPendant = () => {
    let path;
    if (!chooseAnother) {
      // Not choose another path (when you click on "Select Pendant" button)
      if (!fromCart) {
        // Add to cart normally path
        path = diamondId ? `/cart?d=${diamondId}&p=${pendantId}` : `/pendant/${pendantId}/choose-diamond/`;
        navigate(path);
      } else {
        // Rechoose pendant path (when you click on "Select Pendant" button when navigated from cart)
        path = diamondId ? `/cart?d=${diamondId}&p=${pendantId}` : `/pendant/${pendantId}/choose-diamond/`;
        navigate(path, { state: { rechoose: true, rechooseType: 'pendant' } });
      }
    } else {
      // Choose another pendant path (when you click on "Select Another Pendant" button)
      path = diamondId ? `/cart?d=${diamondId}&p=${pendantId}` : `/pendant/${pendantId}/choose-diamond/`;
      // If the choose another path pendant is different from the old pendant, navigate to cart with the old pendant id
      if (oldPendantId != pendantId) navigate(path, { state: { chooseAnother: chooseAnother, oldPendantId: oldPendantId } });
      // Else navigate to cart like the rechoose pendant path
      else navigate(path, { state: { rechoose: true, rechooseType: 'pendant' } });
    }
  };

  const handleSelectAnotherPendant = () => {
    const path = diamondId ? `/diamond/${diamondId}/choose-pendant` : `/pendant/${pendantId}/choose-diamond/`;
    navigate(path, { state: { chooseAnother: true, oldPendantId: pendantId } });
  }

  useEffect(() => {
    setError(null);
    setLoading(true);

    async function getPendant() {
      try {
        const data = await fetchPendantById(pendantId);
        setPendant(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getPendant();
    console.log(`diamondId: ${diamondId}, pendantId: ${pendantId}`);
  }, [diamondId, pendantId]);

  if (loading) {
    return <CircularIndeterminate size={56} />;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!pendant) {
    return <p>Pendant not found.</p>;
  }

  return (
    <div className="product-details-container">
      <div className="image-section">
        <img src='/src/images/pendant.png' alt="Diamond" className="product-details-image" />
        {/* <div className="thumbnail-gallery">
          <img src='/src/images/diamond-thumbnail1.png' alt="Thumbnail 1" />
          <img src='/src/images/diamond-thumbnail2.png' alt="Thumbnail 2" />
          <img src='/src/images/diamond-thumbnail3.png' alt="Thumbnail 3" />
          <img src='/src/images/gia-report.png' alt="GIA Report" />
        </div> */}
      </div>
      <div className="details-section">
        <h2>{pendant.name}</h2>
        <div className="badge-group">
          <span className="badge">{pendant.chainLength || 'Unknown'}</span>
          <span className="badge">{pendant.chainType || 'Unknown'}</span>
          <span className="badge">{pendant.claspType || 'Unknown'}</span>
        </div>
        <p className="price">${pendant.price.toFixed(2)}</p>
        <div className={styles.selectButtonContainer}>
          {diamondId ? (
            <Button className={styles.selectDiamondButton} onClick={handleSelectPendant}>SELECT PENDANT</Button>
          ) : (
            <Button className={styles.selectDiamondButton} onClick={handleSelectPendant}>SELECT A DIAMOND</Button>
          )}
          {fromCart ? (
            <Button className={styles.selectAnotherDiamondButton} onClick={handleSelectAnotherPendant}>SELECT ANOTHER PENDANT</Button>
          ) : (
            null
          )}
        </div>
        {/* <div className="order-info">
          <h3>Your Order Includes:</h3>
          <div className="order-detail">
            <span className="order-icon">🚚</span>
            <p>Free Shipping</p>
            <small>We're committed to making your entire experience a pleasant one, from shopping to shipping</small>
          </div>
          <div className="order-detail">
            <h4>GIA Grading Report</h4>
          </div>
        </div> */}
      </div>
      <div className="specifications-section">
        <div className="product-detail">
          <h4>Product Details</h4>
        </div>
        <table>
          <tbody>
            <tr>
              <td>Name</td>
              <td>{pendant.name}</td>
            </tr>
            <tr>
              <td>Price</td>
              <td>${pendant.price.toFixed(2)}</td>
            </tr>
            <tr>
              <td>Chain Length</td>
              <td>{pendant.chainLength || 'Unknown'}</td>
            </tr>
            <tr>
              <td>Chain Type</td>
              <td>{pendant.chainType || 'Unknown'}</td>
            </tr>
            <tr>
              <td>Clasp Type</td>
              <td>{pendant.claspType || 'Unknown'}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PendantDetails;
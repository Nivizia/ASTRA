import React, { useEffect, useState } from 'react';
import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { fetchRingById, fetchDiamondById } from '../../../../javascript/apiService';

import CircularIndeterminate from '../../misc/loading';
import Button from '@mui/material/Button';

import '../../css/product.css';
import styles from "../../css/temporarydrawer.module.css";

const RingDetails = () => {
  const { diamondId, ringId } = useParams();

  const [shape, setShape] = useState(null);

  const [ring, setRing] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const location = useLocation();
  const params = new URLSearchParams(location.search);

  const cart = params.get('cart');

  const navigate = useNavigate();

  const handleSelectRing = () => {
    const path = diamondId ? `/cart?d=${diamondId}&r=${ringId}` : `/ring/${ringId}/choose-diamond/`;
    navigate(path);
  };

  const handleSelectAnotherRing = () => {
    const path = diamondId ? `/diamond/${diamondId}/choose-ring?shape=${shape}` : `/ring/${ringId}/choose-diamond/`;
    navigate(path);
  }

  useEffect(() => {
    async function getRing() {
      try {
        const data = await fetchRingById(ringId);
        setRing(data);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getRing();

    async function getDiamond() {
      try {
        const data = await fetchDiamondById(diamondId);
        setShape(data.shape);
      } catch (error) {
        setError(error.message);
      } finally {
        setLoading(false);
      }
    }
    getDiamond();
    console.log(`diamondId: ${diamondId}, ringId: ${ringId}`);
  }, [diamondId, ringId]);

  if (loading) {
    return <CircularIndeterminate />;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!ring) {
    return <p>Ring not found.</p>;
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
    <div className="product-details-container">
      <div className="image-section">
        <img src='/src/images/ring.png' alt="Diamond" className="product-details-image" />
        <div className="thumbnail-gallery">
          <img src='/src/images/diamond-thumbnail1.png' alt="Thumbnail 1" />
          <img src='/src/images/diamond-thumbnail2.png' alt="Thumbnail 2" />
          <img src='/src/images/diamond-thumbnail3.png' alt="Thumbnail 3" />
          <img src='/src/images/gia-report.png' alt="GIA Report" />
        </div>
      </div>
      <div className="details-section">
        <h2>{getRingName(ring)}</h2>
        <div className="badge-group">
          <span className="badge">{ring.ringType || 'Unknown'}</span>
          <span className="badge">{ring.metalType || 'Unknown'}</span>
        </div>
        <p className="price">${ring.price.toFixed(2)}</p>
        <div className={styles.selectButtonContainer}>
          {diamondId ? (
            <Button className={styles.selectDiamondButton} onClick={handleSelectRing}>SELECT RING</Button>
          ) : (
            <Button className={styles.selectDiamondButton} onClick={handleSelectRing}>SELECT A DIAMOND</Button>
          )}
          {cart ? (
            <Button className={styles.selectAnotherDiamondButton} onClick={handleSelectAnotherRing}>SELECT ANOTHER RING</Button>
          ) : (
            null
          )}
        </div>
        <div className="order-info">
          <h3>Your Order Includes:</h3>
          <div className="order-detail">
            <span className="order-icon">🚚</span>
            <p>Free Shipping</p>
            <small>We're committed to making your entire experience a pleasant one, from shopping to shipping</small>
          </div>
          <div className="order-detail">
            <h4>GIA Grading Report</h4>
          </div>
        </div>
      </div>
      <div className="specifications-section">
        <div className="product-detail">
          <h4>Product Details</h4>
        </div>
        <table>
          <tbody>
            <tr>
              <td>Price</td>
              <td>{ring.price}</td>
            </tr>
            <tr>
              <td>Metal Type</td>
              <td>{ring.metalType}</td>
            </tr>
            <tr>
              <td>Ring Size</td>
              <td>{ring.ringSize}</td>
            </tr>
            <tr>
              <td>Suitable Shapes</td>
              <td>{ring.shapes.map((shape, index) => (
                <div key={index}>{shape}</div>
              ))}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default RingDetails;
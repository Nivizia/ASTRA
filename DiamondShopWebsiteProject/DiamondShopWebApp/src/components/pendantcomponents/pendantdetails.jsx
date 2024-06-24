import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchPendantById } from '../../../javascript/apiService';

import CircularIndeterminate from '../loading';
import Button from '@mui/material/Button';

import '../css/productbox.css';
import styles from "../css/temporarydrawer.module.css";

const PendantDetails = () => {
  const { pendantId } = useParams();
  const [pendant, setPendant] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
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
  }, [pendantId]);

  if (loading) {
    return <CircularIndeterminate />;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }

  if (!pendant) {
    return <p>Pendant not found.</p>;
  }

  return (
    <div className="diamond-details-container">
      <div className="image-section">
        <img src='/src/images/diamond.png' alt="Diamond" className="diamond-image" />
        <div className="thumbnail-gallery">
          <img src='/src/images/diamond-thumbnail1.png' alt="Thumbnail 1" />
          <img src='/src/images/diamond-thumbnail2.png' alt="Thumbnail 2" />
          <img src='/src/images/diamond-thumbnail3.png' alt="Thumbnail 3" />
          <img src='/src/images/gia-report.png' alt="GIA Report" />
        </div>
      </div>
      <div className="details-section">
        <h2>{`${pendant.name} Price ${pendant.price}`}</h2>
        <div className="badge-group">
          <span className="badge">{`${pendant.name}`}</span>
          <span className="badge">{`${pendant.price}`}</span>
          <span className="badge">{`Beeg`}</span>
        </div>
        <p className="price">${pendant.price.toFixed(2)}</p>
        <Button className={styles.selectDiamondButton} >SELECT THIS PENDANT</Button>
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
          <div className="order-detail">
            <h4>Product Details</h4>
          </div>
        </div>
      </div>
      <div className="specifications-section">
        <table>
          <tbody>
            <tr>
              <td>Name</td>
              <td>{pendant.name}</td>
            </tr>
            <tr>
              <td>Price</td>
              <td>{pendant.price}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PendantDetails;

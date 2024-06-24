import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchDiamondById } from '../../../javascript/apiService';

import CircularIndeterminate from '../loading';
import TemporaryDrawer from '../temporaryDrawer';

import '../css/productbox.css';

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
        <h2>{`${diamond.caratWeight} Carat ${diamond.color}-${diamond.clarity} ${diamond.cut} cut ${diamond.dType} Diamond`}</h2>
        <div className="badge-group">
          <span className="badge">{`${diamond.caratWeight}ct`}</span>
          <span className="badge">{`${diamond.color} Color`}</span>
          <span className="badge">{`${diamond.clarity} Clarity`}</span>
          <span className="badge">{`Excellent`}</span>
        </div>
        <p className="price">${diamond.price.toFixed(2)}</p>
        <TemporaryDrawer />
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
              <td>Shape</td>
              <td>{diamond.dType}</td>
            </tr>
            <tr>
              <td>Cut</td>
              <td>{diamond.cut}</td>
            </tr>
            <tr>
              <td>Color</td>
              <td>{diamond.color}</td>
            </tr>
            <tr>
              <td>Clarity</td>
              <td>{diamond.clarity}</td>
            </tr>
            <tr>
              <td>Carat Weight</td>
              <td>{diamond.caratWeight}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default DiamondDetails;

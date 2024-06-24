import React from 'react';
import '../css/productbox.css';

const PendantBox = ({ pendantId, name, price, stockQuantity, imageUrl, metalType }) => {
    return (
        <a href={`/Pendant/${pendantId}`} className="diamond-link">
            <div className="diamond-box">
                <div className='diamond-image-container'>
                    <img src='/src/images/diamond.png' alt="Diamond" className="diamond-image" />
                </div>
                <div className="diamond-details">
                    <h2>{`${metalType} Pendant`}</h2>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a >
    );
};

export default PendantBox;
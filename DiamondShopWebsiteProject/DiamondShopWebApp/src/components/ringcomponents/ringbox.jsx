import React from 'react';
import '../css/diamond.css';

const RingBox = ({ ringId, name, price, stockQuantity, imageUrl, metalType, ringSize }) => {
    return (
        <a href={`/Ring/${ringId}`} className="diamond-link">
            <div className="diamond-box">
                <div className='diamond-image-container'>
                    <img src='/src/images/diamond.png' alt="Diamond" className="diamond-image" />
                </div>
                <div className="diamond-details">
                    <h2>{`${metalType} Ring`}</h2>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a >
    );
};

export default RingBox;
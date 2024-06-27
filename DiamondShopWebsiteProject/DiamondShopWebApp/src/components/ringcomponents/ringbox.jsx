import React from 'react';
import '../css/productbox.css';

const RingBox = ({ ringId, diamondId, name, price, stockQuantity, imageUrl }) => {

    const href = diamondId ? `/diamond/${diamondId}/choose-ring/${ringId}` : `/ring/${ringId}`;
    return (

        <a href={href} className="diamond-link">
            <div className="diamond-box">
                <div className='diamond-image-container'>
                    <img src='/src/images/ring.png' alt={name} className="diamond-image" />
                </div>
                <div className="diamond-details">
                    <h2>{`${name}`}</h2>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a>
    );
};

export default RingBox;
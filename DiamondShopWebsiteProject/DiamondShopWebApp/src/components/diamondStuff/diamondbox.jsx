import React from 'react';
import '../css/diamond.css';

const DiamondBox = ({ id, price, imageUrl, caratWeight, color, clarity, cut, shape }) => {
    return (
        <a href={`/diamond/${id}`} className="diamond-link">
            <div className="diamond-box">
                <div className='diamond-image-container'>
                    <img src='/src/images/diamond.png' alt="Diamond" className="diamond-image" />
                </div>
                <div className="diamond-details">
                    <h2>{`${caratWeight} Carat ${color}-${clarity} ${cut} cut ${shape} Diamond`}</h2>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a >
    );
};

export default DiamondBox;
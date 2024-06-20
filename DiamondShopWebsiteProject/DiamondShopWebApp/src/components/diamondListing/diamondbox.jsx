// diamondbox.jsx
import React from 'react';
import '../css/diamondbox.css';

const DiamondBox = ({ price, imageUrl, caratWeight, color, clarity, cut, shape }) => {
    return (
        <div className="diamond-box">
            <div className='diamond-image-container'>
                <img src='/src/images/diamond.png' alt="Diamond" className="diamond-image" />
            </div>
            <div className="diamond-details">
                <div className="diamond-details">
                    <h2>{`${caratWeight} Carat ${color}-${clarity} ${cut} cut ${shape} Diamond`}</h2>
                    <p>Carat: {caratWeight}</p>
                    <p>Color: {color}</p>
                    <p>Clarity: {clarity}</p>
                    <p>Cut: {cut}</p>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </div>
    );
};

export default DiamondBox;
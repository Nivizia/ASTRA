import React from 'react';
import '../../css/product.css';

const DiamondBox = ({ diamondId, ringId, pendantId, price, imageUrl, caratWeight, color, clarity, cut, shape }) => {

    const href = ringId ? `/ring/${ringId}/choose-diamond/${diamondId}` : pendantId ? `/pendant/${pendantId}/choose-diamond/${diamondId}` : `/diamond/${diamondId}`;
    return (
        <a href={href} className="diamond-link">
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
import React from 'react';
import '../../css/product.css';

const DiamondBox = ({ diamondId, ringId, pendantId, price, imageUrl, caratWeight, color, clarity, cut, shape, chooseAnother, oldDiamondId }) => {

    let href;
    if (!chooseAnother && !oldDiamondId) {
        if (ringId) {
            href = `/ring/${ringId}/choose-diamond/${diamondId}`;
        } else if (pendantId) {
            href = `/pendant/${pendantId}/choose-diamond/${diamondId}`;
        } else {
            href = `/diamond/${diamondId}`;
        }
    } else {
        if (ringId) {
            href = `/ring/${ringId}/choose-diamond/${diamondId}?choose-another=true&od=${oldDiamondId}`;
        } else if (pendantId) {
            href = `/pendant/${pendantId}/choose-diamond/${diamondId}?choose-another=true&od=${oldDiamondId}`;
        }
    }

    return (
        <a href={href} className="product-link">
            <div className="product-box">
                <div className='product-image-container'>
                    <img src='/src/images/diamond.png' alt="Diamond" className="product-image" />
                </div>
                <div className="product-details">
                    <h2>{`${caratWeight} Carat ${color}-${clarity} ${cut} cut ${shape} Diamond`}</h2>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a >
    );
};

export default DiamondBox;
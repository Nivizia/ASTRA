// src/components/productcomponents/diamondcomponents/diamondbox.jsx

import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../../css/product.css';

const DiamondBox = ({ diamondId, ringId, pendantId, price, imageUrl, caratWeight, color, clarity, cut, shape, chooseAnother, oldDiamondId }) => {

    const navigate = useNavigate();

    function handleClickLink() {
        let href;
        if (!chooseAnother && !oldDiamondId) {
            if (ringId) {
                href = `/ring/${ringId}/choose-diamond/${diamondId}`;
            } else if (pendantId) {
                href = `/pendant/${pendantId}/choose-diamond/${diamondId}`;
            } else {
                href = `/diamond/${diamondId}`;
            }
            navigate(href);
        } else {
            if (ringId) {
                href = `/ring/${ringId}/choose-diamond/${diamondId}`;
            } else if (pendantId) {
                href = `/pendant/${pendantId}/choose-diamond/${diamondId}`;
            }
            navigate(href, { state: { chooseAnother: true, oldDiamondId: oldDiamondId } });
        }
    }


    return (
        <a onClick={() => { handleClickLink() }} className="product-link">
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
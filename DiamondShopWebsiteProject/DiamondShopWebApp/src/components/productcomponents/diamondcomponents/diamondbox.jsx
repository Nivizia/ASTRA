// src/components/productcomponents/diamondcomponents/diamondbox.jsx

import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../../css/product.css';

const DiamondBox = ({ diamondId, ringId, pendantId, price, imageUrl, caratWeight, color, clarity, cut, shape, chooseAnother, oldDiamondId }) => {

    const navigate = useNavigate();

    function handleClickLink() {
        let path;
        if (!chooseAnother && !oldDiamondId) {
            // If chooseAnother and oldDiamondId are not defined, original route
            if (ringId) {
                // Ring-first route
                path = `/ring/${ringId}/choose-diamond/${diamondId}`;
            } else if (pendantId) {
                // Pendant-first route
                path = `/pendant/${pendantId}/choose-diamond/${diamondId}`;
            } else {
                // Default diamond route
                path = `/diamond/${diamondId}`;
            }
            navigate(path);
        } else {
            // If chooseAnother and oldDiamondId are defined, choosing another diamond route
            if (ringId) {
                // Ring route
                path = `/ring/${ringId}/choose-diamond/${diamondId}`;
            } else if (pendantId) {
                // Pendant route
                path = `/pendant/${pendantId}/choose-diamond/${diamondId}`;
            }
            // Navigate to the href with the chooseAnother and oldDiamondId state
            navigate(path, { state: { chooseAnother: chooseAnother, oldDiamondId: oldDiamondId } });
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
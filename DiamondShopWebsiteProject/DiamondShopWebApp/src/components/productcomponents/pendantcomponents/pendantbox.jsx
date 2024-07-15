// src/components/productcomponents/pendantcomponents/pendantbox.jsx

import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../../css/product.css';

const PendantBox = ({ pendantId, diamondId, name, price, stockQuantity, imageUrl, chooseAnother, oldPendantId }) => {

    const navigate = useNavigate();

    function handleClickLink() {
        let path;
        if (!chooseAnother && !oldPendantId) {
            // If chooseAnother and oldPendantId are not defined, original route
            if (diamondId) {
                // Diamond-first route
                path = `/diamond/${diamondId}/choose-pendant/${pendantId}`;
            } else {
                // Default pendant route
                path = `/pendant/${pendantId}`;
            }
            navigate(path);
        } else {
            // If chooseAnother and oldPendantId are defined, choosing another pendant route
            if (diamondId) {
                // Diamond route
                path = `/diamond/${diamondId}/choose-pendant/${pendantId}`;
            }
            // Navigate to the path with the chooseAnother and oldPendantId state
            navigate(path, { state: { chooseAnother: chooseAnother, oldPendantId: oldPendantId } });
        }
    }

    return (
        <a onClick={() => { handleClickLink() }} className="product-link">
            <div className="product-box">
                <div className='product-image-container'>
                    <img src={imageUrl || '/src/images/pendant.png'} alt={name} className="product-image" />
                </div>
                <div className="product-details">
                    <h3>{`${name}`}</h3>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a>
    );
};

export default PendantBox;
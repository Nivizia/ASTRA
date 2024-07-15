// src/components/productcomponents/ringcomponents/ringbox.jsx

import React from 'react';
import { useNavigate } from 'react-router-dom';
import '../../css/product.css';

const RingBox = ({ ringId, diamondId, name, price, stockQuantity, imageUrl, chooseAnother, oldRingId }) => {

    const navigate = useNavigate();

    function handleClickLink() {
        let path;
        if (!chooseAnother && !oldRingId) {
            // If chooseAnother and oldRingId are not defined, original route
            if (diamondId) {
                // Diamond-first route
                path = `/diamond/${diamondId}/choose-ring/${ringId}`;
            } else {
                // Default ring route
                path = `/ring/${ringId}`;
            }
            navigate(path);
        } else {
            // If chooseAnother and oldRingId are defined, choosing another ring route
            if (diamondId) {
                // Diamond route
                path = `/diamond/${diamondId}/choose-ring/${ringId}`;
            }
            // Navigate to the href with the chooseAnother and oldRingId state
            navigate(path, { state: { chooseAnother: chooseAnother, oldRingId: oldRingId } });
        }
    }

    return (
        <a onClick={() => { handleClickLink() }} className="product-link">
            <div className="product-box">
                <div className='product-image-container'>
                    <img src='/src/images/ring.png' alt={name} className="product-image" />
                </div>
                <div className="product-details">
                    <h3>{`${name}`}</h3>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a>
    );
};

export default RingBox;
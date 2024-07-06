import React from 'react';
import '../../css/product.css';

const PendantBox = ({ pendantId, diamondId, name, price, stockQuantity, imageUrl }) => {

    const href = diamondId ? `/diamond/${diamondId}/choose-pendant/${pendantId}` : `/pendant/${pendantId}`;
    return (

        <a href={href} className="product-link">
            <div className="product-box">
                <div className='product-image-container'>
                    <img src='/src/images/pendant.png' alt={name} className="product-image" />
                </div>
                <div className="product-details">
                    <h2>{`${name}`}</h2>
                    <p className="price">${price.toFixed(2)}</p>
                </div>
            </div>
        </a>
    );
};

export default PendantBox;
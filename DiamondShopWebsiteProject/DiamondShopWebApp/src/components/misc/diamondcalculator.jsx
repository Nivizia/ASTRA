// src/components/misc/diamondcalculator.jsx

import React, { useState } from 'react';
import { calculateDiamondPrice, calculateDiamondPricePerCarat } from '../../../javascript/apiService';

import Box from '@mui/material/Box';
import Slider from '@mui/material/Slider';
import { Grid, TextField } from '@mui/material';
import grid from '@mui/material/Grid';
import Button from '@mui/material/Button';

import styles from "../css/diamondcalculator.module.css";

const DiamondPriceCalculator = () => {
    const [carat, setCarat] = useState('');
    const [color, setColor] = useState('');
    const [clarity, setClarity] = useState('');
    const [cut, setCut] = useState('');
    const [price, setPrice] = useState(null);
    const [pricePerCarat, setPricePerCarat] = useState(null);
    const [error, setError] = useState(null);
    const [shape, setShape] = useState('');

    const handleCalculatePrice = async () => {
        try {
            const result = await calculateDiamondPrice(carat, color, clarity, cut);
            setPrice(result);
            setError(null);
        } catch (err) {
            setError(err.message);
        }
    };

    const handleCalculatePricePerCarat = async () => {
        try {
            const result = await calculateDiamondPricePerCarat(carat, color, clarity, cut);
            setPricePerCarat(result);
            setError(null);
        } catch (err) {
            setError(err.message);
        }
    };

    // Carat slider
    const handleSliderChange = (event, newValue) => {
        setCarat(newValue);
    };

    const handleInputChange = (event) => {
        setCarat(event.target.value === '' ? '' : Number(event.target.value));
    };

    const handleBlur = () => {
        if (carat < 0.1) {
            setCarat(0.1);
        } else if (carat > 5) {
            setCarat(5);
        }
    };

    // Shape buttons
    const handleShapeChange = (event) => {
        setShape(event.target.value);
    };

    function valuetext(value) {
        return `${value}°C`;
    }

    return (
        <>

            <div className={styles.container}>
                <h1 className={styles.title}>Diamond Price Calculator</h1>
                <Grid container spacing={2}>
                    <Grid item xs={8}>
                        <Box className={styles.inputField}>
                            <Slider
                                aria-label="Carat"
                                defaultValue={1}
                                getAriaValueText={valuetext}
                                valueLabelDisplay='auto'
                                shiftStep={30}
                                step={0.01}
                                min={0.1}
                                max={5}
                                value={carat}
                                onChange={handleSliderChange}
                            />
                        </Box>
                    </Grid>
                    <Grid item xs={4}>
                        <TextField
                            id="outlined-basic"
                            label="Carat"
                            variant="outlined"
                            value={carat}
                            onChange={handleInputChange}
                            onBlur={handleBlur}
                            inputProps={{ step: 0.01, min: 0.1, max: 5, type: 'number' }}
                            className={styles.inputField} />
                    </Grid>
                </Grid>
                <Grid container spacing={2}>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Round" onClick={handleShapeChange} className={styles.shapeButton}>Round</Button>
                    </Grid>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Princess" onClick={handleShapeChange} className={styles.shapeButton}>Princess</Button>
                    </Grid>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Emerald" onClick={handleShapeChange} className={styles.shapeButton}>Emerald</Button>
                    </Grid>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Asscher" onClick={handleShapeChange} className={styles.shapeButton}>Asscher</Button>
                    </Grid>
                </Grid>
                <Grid container spacing={2}>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Radiant" onClick={handleShapeChange} className={styles.shapeButton}>Radiant</Button>
                    </Grid>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Oval" onClick={handleShapeChange} className={styles.shapeButton}>Oval</Button>
                    </Grid>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Pear" onClick={handleShapeChange} className={styles.shapeButton}>Pear</Button>
                    </Grid>
                    <Grid item xs={3}>
                        <Button variant="contained" color="primary" value="Marquise" onClick={handleShapeChange} className={styles.shapeButton}>Marquise</Button>
                    </Grid>
                </Grid>
                <input
                    type="text"
                    placeholder="Clarity"
                    value={clarity}
                    onChange={e => setClarity(e.target.value)}
                    className={styles.inputField}
                />
                <input
                    type="text"
                    placeholder="Cut"
                    value={cut}
                    onChange={e => setCut(e.target.value)}
                    className={styles.inputField}
                />
                <button
                    onClick={handleCalculatePrice}
                    className={`${styles.button} ${styles.calculateButton}`}
                >
                    Calculate Price
                </button>
                <button
                    onClick={handleCalculatePricePerCarat}
                    className={`${styles.button} ${styles.calculatePerCaratButton}`}
                >
                    Calculate Price Per Carat
                </button>
                {price !== null && <p className={styles.result}>Total Price: ${price.toFixed(2)}</p>}
                {pricePerCarat !== null && <p className={styles.result}>Price Per Carat: ${pricePerCarat.toFixed(2)}</p>}
                {error && <p className={styles.error}>Error: {error}</p>}
            </div>

        </>
    );
};

export default DiamondPriceCalculator;
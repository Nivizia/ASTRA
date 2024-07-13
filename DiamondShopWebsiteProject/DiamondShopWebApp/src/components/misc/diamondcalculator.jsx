// src/components/misc/diamondcalculator.jsx

import React, { useState } from 'react';
import { calculateDiamondPrice, calculateDiamondPricePerCarat } from '../../../javascript/apiService';

import Box from '@mui/material/Box';
import Slider from '@mui/material/Slider';
import { Grid, TextField, Typography } from '@mui/material';
import { ToggleButtonGroup, ToggleButton } from '@mui/material';

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

    // Color buttons
    const handleColorChange = (event) => {
        setColor(event.target.value);
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
                <Typography id="discrete-slider" gutterBottom>Shape</Typography>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <ToggleButtonGroup value={shape} exclusive onChange={handleShapeChange} aria-label="shape">
                            <Grid container spacing={2}>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="round" aria-label="round">Round</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="cushion" aria-label="cushion">Cushion</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="emerald" aria-label="emerald">Emerald</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="oval" aria-label="oval">Oval</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="princess" aria-label="princess">Princess</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="pear" aria-label="pear">Pear</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="radiant" aria-label="radiant">Radiant</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="marquise" aria-label="marquise">Marquise</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="asscher" aria-label="asscher">Asscher</ToggleButton></Grid>
                                <Grid item xs={2.4}><ToggleButton className={styles.togglebutton} value="heart" aria-label="heart">Heart</ToggleButton></Grid>
                            </Grid>
                        </ToggleButtonGroup>
                    </Grid>
                </Grid>
                <Typography id="discrete-slider" gutterBottom>Color</Typography>
                <Grid container spacing={2} className={styles.colorbuttons}>
                    <Grid item xs={12}>
                        <ToggleButtonGroup value={color} exclusive onChange={handleShapeChange} aria-label="color">
                            <Grid container spacing={2} className={styles.colorbuttons}>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="K" aria-label="K">K</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="J" aria-label="J">J</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="I" aria-label="I">I</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="H" aria-label="H">H</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="G" aria-label="G">G</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="F" aria-label="F">F</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="E" aria-label="E">E</ToggleButton></Grid>
                                <Grid item xs={3}><ToggleButton className={styles.colorbutton} value="D" aria-label="D">D</ToggleButton></Grid>
                            </Grid>
                        </ToggleButtonGroup>
                    </Grid>
                </Grid>
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
// src/components/misc/diamondcalculator.jsx

import React, { useState, useEffect } from 'react';
import { calculateDiamondPrice, calculateDiamondPricePerCarat, fetchAvailableDiamondsByCriteria } from '../../../javascript/apiService';

import { Box, Slider, Grid, TextField, Typography, ToggleButtonGroup, ToggleButton } from '@mui/material';

import CircularIndeterminate from './loading';
import DiamondBox from '../productcomponents/diamondcomponents/diamondbox';

import styles from "../css/diamondcalculator.module.css";

const DiamondPriceCalculator = () => {
    const [carat, setCarat] = useState(1);
    const [shape, setShape] = useState('round');
    const [color, setColor] = useState('K');
    const [clarity, setClarity] = useState('I1');
    const [cut, setCut] = useState('Fair');

    const [price, setPrice] = useState(null);
    const [pricePerCarat, setPricePerCarat] = useState(null);

    const [loading, setLoading] = useState(true);
    const [loadingDiamonds, setLoadingDiamonds] = useState(true);
    const [error, setError] = useState(null);

    const [diamonds, setDiamonds] = useState([]);

    const handleCalculatePrice = async () => {
        setError(null);
        setLoading(true);
        try {
            const price = await calculateDiamondPrice(carat, color, clarity, cut);
            const ppc = await calculateDiamondPricePerCarat(carat, color, clarity, cut);
            setPrice(price);
            setPricePerCarat(ppc);
            setError(null);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
        getDiamonds();
    };

    const getDiamonds = async () => {
        setError(null);
        setLoadingDiamonds(true);
        try {
            const data = await fetchAvailableDiamondsByCriteria(shape, carat, color, clarity, cut);
            console.log(shape, carat, color, clarity, cut);
            console.log(data);
            if (Array.isArray(data)) {
                setDiamonds(data);
            } else {
                setError("Unable to fetch diamond");
            }
        } catch (error) {
            setError(error.message);
        } finally {
            setLoadingDiamonds(false);
        }
    }

    useEffect(() => {
        handleCalculatePrice();
    }, []);

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

    // Clarity buttons
    const handleClarityChange = (event) => {
        setClarity(event.target.value);
    };

    // Cut buttons
    const handleCutChange = (event) => {
        setCut(event.target.value);
    };

    return (
        <>
            <Grid container spacing={5} className={styles.mainContainer}>
                <Grid item sm={12} md={4.5} className={styles.calculatorGrid}>
                    <div className={styles.calculatorContainer}>
                        <Typography variant="h4" gutterBottom>Diamond Price Calculator</Typography>
                        <Typography variant="h5" gutterBottom>Carat</Typography>
                        <Grid container spacing={2} className={styles.gridContainer}>
                            <Grid item xs={8}>
                                <Box className={styles.caratSliderContainer}>
                                    <Slider
                                        aria-label="Carat"
                                        defaultValue={1}
                                        valueLabelDisplay='auto'
                                        step={0.01}
                                        min={0.1}
                                        max={5}
                                        value={carat}
                                        onChange={handleSliderChange}
                                        className={styles.slider}
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

                        <Typography variant="h5" gutterBottom>Shape</Typography>
                        <Grid container spacing={2} className={styles.gridContainer}>
                            <Grid item xs={12}>
                                <ToggleButtonGroup value={shape} exclusive onChange={handleShapeChange} aria-label="shape" className={styles.toggleButtonGroup}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="round" aria-label="round">Round</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="cushion" aria-label="cushion">Cushion</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="emerald" aria-label="emerald">Emerald</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="oval" aria-label="oval">Oval</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="princess" aria-label="princess">Princess</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="pear" aria-label="pear">Pear</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="radiant" aria-label="radiant">Radiant</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="marquise" aria-label="marquise">Marquise</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="asscher" aria-label="asscher">Asscher</ToggleButton></Grid>
                                        <Grid item xs={6} sm={2.4} md={6} lg={2.4}><ToggleButton className={styles.togglebutton} value="heart" aria-label="heart">Heart</ToggleButton></Grid>
                                    </Grid>
                                </ToggleButtonGroup>
                            </Grid>
                        </Grid>

                        <Typography variant="h5" gutterBottom>Color</Typography>
                        <Grid container spacing={2} className={styles.gridContainer}>
                            <Grid item xs={12}>
                                <ToggleButtonGroup value={color} exclusive onChange={handleColorChange} aria-label="color" className={styles.toggleButtonGroup}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="K" aria-label="K">K</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="J" aria-label="J">J</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="I" aria-label="I">I</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="H" aria-label="H">H</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="G" aria-label="G">G</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="F" aria-label="F">F</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="E" aria-label="E">E</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="D" aria-label="D">D</ToggleButton></Grid>
                                    </Grid>
                                </ToggleButtonGroup>
                            </Grid>
                        </Grid>

                        <Typography variant="h5" gutterBottom>Clarity</Typography>
                        <Grid container spacing={2} className={styles.gridContainer}>
                            <Grid item xs={12}>
                                <ToggleButtonGroup value={clarity} exclusive onChange={handleClarityChange} aria-label="clarity" className={styles.toggleButtonGroup}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="I1" aria-label="I1">I1</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="SI2" aria-label="SI2">SI2</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="SI1" aria-label="SI1">SI1</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="VS2" aria-label="VS2">VS2</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="VS1" aria-label="VS1">VS1</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="VVS2" aria-label="VVS2">VVS2</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="VVS1" aria-label="VVS1">VVS1</ToggleButton></Grid>
                                        <Grid item xs={3}><ToggleButton className={styles.CButton} value="IF" aria-label="IF">IF</ToggleButton></Grid>
                                    </Grid>
                                </ToggleButtonGroup>
                            </Grid>
                        </Grid>

                        <Typography variant="h5" gutterBottom>Cut</Typography>
                        <Grid container spacing={2} className={styles.gridContainer}>
                            <Grid item xs={12}>
                                <ToggleButtonGroup value={cut} exclusive onChange={handleCutChange} aria-label="cut" className={styles.toggleButtonGroup}>
                                    <Grid container spacing={2}>
                                        <Grid item xs={6} sm={3} md={6} lg={3}><ToggleButton className={styles.CButton} value="Fair" aria-label="Fair">Fair</ToggleButton></Grid>
                                        <Grid item xs={6} sm={3} md={6} lg={3}><ToggleButton className={styles.CButton} value="Good" aria-label="Good">Good</ToggleButton></Grid>
                                        <Grid item xs={6} sm={3} md={6} lg={3}><ToggleButton className={styles.CButton} value="Very Good" aria-label="Very Good">Very Good</ToggleButton></Grid>
                                        <Grid item xs={6} sm={3} md={6} lg={3}><ToggleButton className={styles.CButton} value="Excellent" aria-label="Excellent">Excellent</ToggleButton></Grid>
                                    </Grid>
                                </ToggleButtonGroup>
                            </Grid>
                        </Grid>
                        <button
                            onClick={handleCalculatePrice}
                            className={`${styles.button} ${styles.calculateButton}`}
                        >
                            Calculate Price
                        </button>
                    </div>
                </Grid>

                <Grid item sm={12} md={7.5} className={styles.outputGrid}>
                    <div className={styles.outputContainer}>
                        <Typography variant="h4" gutterBottom>Price Output</Typography>
                        <Grid container className={styles.gridContainer}>
                            <Grid item xs={6} className={styles.priceGrid}>
                                <Typography variant="h6">Calculated Price:</Typography>
                                {!loading ? (
                                    <Typography variant="body1" className={styles.result}>${price.toFixed(2)}</Typography>
                                ) : (
                                    <CircularIndeterminate />
                                )}
                            </Grid>
                            <Grid item xs={6} className={styles.priceGrid}>
                                <Typography variant="h6">Price Per Carat:</Typography>
                                {!loading ? (
                                    <Typography variant="body1" className={styles.result}>${pricePerCarat.toFixed(2)}/ct.</Typography>
                                ) : (
                                    <CircularIndeterminate />
                                )}
                            </Grid>
                        </Grid>
                        {error && <Typography variant="body1" color="error">{error}</Typography>}
                    </div>
                </Grid>
                <Grid item sm={12} className={styles.diamondGrid}>
                    {diamonds && diamonds.length > 0 ? (
                        <>
                            <Typography variant="h5" gutterBottom className={styles.interestedDiamonds}>Diamonds You Might Be Interested In:</Typography>
                            <div className={styles.productList}>
                                {diamonds.map((diamond) => (
                                    <DiamondBox
                                        key={diamond.dProductId}
                                        diamondId={diamond.dProductId}
                                        price={diamond.price}
                                        imageUrl={diamond.imageUrl}
                                        caratWeight={diamond.caratWeight}
                                        color={diamond.color}
                                        clarity={diamond.clarity}
                                        cut={diamond.cut}
                                        shape={diamond.shape}
                                    />
                                ))}
                            </div>
                        </>
                    ) : loadingDiamonds ? (
                        <CircularIndeterminate size={56} />
                    ) : null
                    }
                </Grid>
            </Grid>
        </>
    );
};

export default DiamondPriceCalculator;
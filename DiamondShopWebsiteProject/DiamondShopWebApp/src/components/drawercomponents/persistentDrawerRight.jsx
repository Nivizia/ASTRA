// src/components/drawercomponents/persistentDrawerRight.jsx

import * as React from 'react';
import { useNavigate } from 'react-router-dom'; // Import useNavigate hook from react-router-dom
import { styled } from '@mui/material/styles';
import {
    Button,
    Box,
    Drawer,
    CssBaseline,
    ToggleButtonGroup,
} from '@mui/material';
import MuiToggleButton from '@mui/material/ToggleButton';
import styles from '../css/temporarydrawer.module.css';


const drawerWidth = 240;

const Main = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        flexGrow: 1,
        padding: 0,
        transition: theme.transitions.create('margin', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
        marginRight: -drawerWidth,
        ...(open && {
            transition: theme.transitions.create('margin', {
                easing: theme.transitions.easing.easeOut,
                duration: theme.transitions.duration.enteringScreen,
            }),
            marginRight: 0,
        }),
        position: 'relative',
    })
);

const ToggleButton = styled(MuiToggleButton)(({ selectedcolor }) => ({
    '&.Mui-selected, &.Mui-selected:hover': {
        color: 'white',
        backgroundColor: selectedcolor,
    },
}));

export default function PersistentDrawerRight({ diamondId, diamondShape }) {
    const [open, setOpen] = React.useState(false);
    const [selectedMode, setSelectedMode] = React.useState('');
    const navigate = useNavigate(); // Initialize useNavigate hook

    const handleToggleButtonChange = (event, newMode) => {
        if (newMode) {
            setSelectedMode(newMode);
            setOpen(true); // Open drawer when a button is selected
        } else {
            setSelectedMode('');
            setOpen(false); // Close drawer when no button is selected
        }
    };

    const handleContinue = () => {
        let path;
        switch (selectedMode) {
            case 'ring':
                path = `/diamond/${diamondId}/choose-ring?shape=${diamondShape}`;
                break;
            case 'pendant':
                path = `/diamond/${diamondId}/choose-pendant/`;
                break;
            case 'cart':
                path = `/cart?diamondId=${diamondId}`;
                break;
            default:
                // Handle case where no option is selected or an invalid option is somehow selected
                return;
        }
        navigate(path);
    };

    return (
        <Box sx={{ display: 'flex', position: 'relative', width: '100%' }}>
            <CssBaseline />
            <Main open={open}>
                <ToggleButtonGroup
                    className={styles.toggleButtonGroup}
                    value={selectedMode}
                    exclusive
                    onChange={handleToggleButtonChange}
                >
                    <ToggleButton className={styles.drawerBox} value="ring" selectedcolor="rgb(0, 0, 200)">
                        Choose Ring
                    </ToggleButton>
                    <ToggleButton className={styles.drawerBox} value="pendant" selectedcolor="rgb(0, 0, 200)">
                        Choose Pendant
                    </ToggleButton>
                    <ToggleButton className={styles.drawerBox} value="cart" selectedcolor="rgb(0, 0, 200)">
                        Add To Cart
                    </ToggleButton>
                </ToggleButtonGroup>
            </Main>
            <Drawer
                sx={{
                    width: drawerWidth,
                    flexShrink: 0,
                    '& .MuiDrawer-paper': {
                        width: drawerWidth,
                        height: '100%', // Ensure the drawer extends the full height of the parent drawer
                        position: 'absolute', // Position absolutely within the parent drawer
                        top: 0, // Align with the top of the parent drawer
                        right: 0, // Align to the right edge of the parent drawer
                    },
                }}
                variant="persistent"
                anchor="right"
                open={open}
                PaperProps={{
                    elevation: 0, // Set the elevation to 0
                }}
            >
                <Button
                    variant="contained"
                    className={styles.confirmButton} // Apply confirmButton class
                    onClick={handleContinue} // Handle click event for Continue button to navigate to the selected mode
                    disabled={!selectedMode} // Disable button if no mode is selected or an invalid mode is selected
                >
                    Continue
                </Button>
            </Drawer>
        </Box>
    );
}
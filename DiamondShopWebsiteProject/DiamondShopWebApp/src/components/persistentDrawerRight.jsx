import * as React from 'react';
import { styled } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import CssBaseline from '@mui/material/CssBaseline';
import { Button } from '@mui/material';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import MuiToggleButton from '@mui/material/ToggleButton';
import styles from "./css/temporarydrawer.module.css";

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

const ToggleButton = styled(MuiToggleButton)(({ selectedColor }) => ({
    "&.Mui-selected, &.Mui-selected:hover": {
        color: "white",
        backgroundColor: selectedColor
    }
}));

export default function PersistentDrawerRight() {
    const [open, setOpen] = React.useState(false);
    const [selectedMode, setSelectedMode] = React.useState('');
    const [continueButtonLink, setContinueButtonLink] = React.useState('');

    const handleToggleButtonChange = (event, newMode) => {
        if (newMode) {
            setSelectedMode(newMode);
            setOpen(true); // Open drawer when a button is selected
            setContinueButtonLink(`/${newMode}`); // Set link for Continue button based on selectedMode
        } else {
            setSelectedMode('');
            setOpen(false); // Close drawer when no button is selected
            setContinueButtonLink(''); // Clear link when no mode is selected
        }
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
                    <ToggleButton className={styles.drawerBox} value="ring" selectedColor="#0000c0">
                        Choose Ring
                    </ToggleButton>
                    <ToggleButton className={styles.drawerBox} value="pendant" selectedColor="#0000c0">
                        Choose Pendant
                    </ToggleButton>
                    <ToggleButton className={styles.drawerBox} value="cart" selectedColor="#0000c0">
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
                    href={continueButtonLink} // Link dynamically set based on selectedMode
                    disabled={!continueButtonLink} // Disable button if no mode is selected
                >
                    Continue
                </Button>
            </Drawer>
        </Box>
    );
}

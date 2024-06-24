import React, { useState } from 'react';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';
import styles from "./css/temporarydrawer.module.css";

import PersistentDrawerRight from './persistentDrawerRight';

export default function TemporaryDrawer({ diamondId }) {
    const [open, setOpen] = useState(false);

    const toggleDrawer = (newOpen) => () => {
        setOpen(newOpen);
    };

    return (
        <>
            <Button className={styles.selectDiamondButton} onClick={toggleDrawer(true)}>SELECT THIS DIAMOND</Button>
            <Drawer open={open} onClose={toggleDrawer(false)} anchor='bottom'>
                <Box 
                    className={styles.drawerContent} 
                    role="presentation" 
                    sx={{ position: 'relative', overflow: 'hidden', height: '100%', display: 'flex', flexDirection: 'column' }}
                >
                    <PersistentDrawerRight diamondId={diamondId}/>
                </Box>
            </Drawer>
        </>
    );
}

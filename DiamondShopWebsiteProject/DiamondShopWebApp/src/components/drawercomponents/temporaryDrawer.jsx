import React, { useState } from 'react';
import {
    Box,
    Drawer,
    Button,
} from '@mui/material';
import styles from "../css/temporarydrawer.module.css";

import PersistentDrawerRight from './persistentDrawerRight';

export default function TemporaryDrawer({ diamondId, diamondShape }) {
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
                    <PersistentDrawerRight diamondId={diamondId} diamondShape={diamondShape} />
                </Box>
            </Drawer>
        </>
    );
}
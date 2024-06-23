import React, { useState } from 'react';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';
import styles from "./css/temporarydrawer.module.css";

import ToggleButtons from './toggleButton';

export default function TemporaryDrawer() {
  const [open, setOpen] = useState(false);

  const toggleDrawer = (newOpen) => () => {
    setOpen(newOpen);
  };

  const DrawerContent = (
    <Box className={styles.drawerContent} role="presentation">
      <ToggleButtons />
    </Box>
  );

  return (
    <>
      <Button className={styles.selectDiamondButton} onClick={toggleDrawer(true)}>SELECT THIS DIAMOND</Button>
      <Drawer open={open} onClose={toggleDrawer(false)} anchor='bottom'>
        {DrawerContent}
      </Drawer>
    </>
  );
}

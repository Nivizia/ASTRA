import { useState } from 'react';
import ToggleButtonGroup from '@mui/material/ToggleButtonGroup';
import MuiToggleButton from '@mui/material/ToggleButton';
import styles from "./css/temporarydrawer.module.css";
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';

import { styled } from "@mui/material/styles";

export default function ToggleButtons() {
  const [buyMode, setBuyMode] = useState('');
  const [open, setOpen] = useState(false);

  const handleChangeBuyMode = (event, newBuyMode) => {
    setBuyMode(newBuyMode);
  };

  const ToggleButton = styled(MuiToggleButton)(({ selectedColor }) => ({
    "&.Mui-selected, &.Mui-selected:hover": {
      color: "white",
      backgroundColor: selectedColor
    }
  }));

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
      <ToggleButtonGroup
        className={styles.toggleButtonGroup}
        value={buyMode}
        exclusive
        onChange={handleChangeBuyMode}
      >
        <ToggleButton className={styles.drawerBox} value="ring" selectedColor="#000080">
          Choose Ring
        </ToggleButton>
        <ToggleButton className={styles.drawerBox} value="pendant" selectedColor="#000080">
          Choose Pendant
        </ToggleButton>
        <ToggleButton className={styles.drawerBox} value="cart" selectedColor="#000080">
          Add To Cart
        </ToggleButton>
      </ToggleButtonGroup>
      <Button className={styles.selectDiamondButton} onClick={toggleDrawer(true)}>SELECT THIS DIAMOND</Button>
      <Drawer open={open} onClose={toggleDrawer(false)} anchor='bottom'>
        {DrawerContent}
      </Drawer>
    </>
  );
}
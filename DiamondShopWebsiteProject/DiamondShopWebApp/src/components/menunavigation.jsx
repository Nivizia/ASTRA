import * as React from 'react';
import Button from '@mui/material/Button';
import ClickAwayListener from '@mui/material/ClickAwayListener';
import Grow from '@mui/material/Grow';
import Paper from '@mui/material/Paper';
import Popper from '@mui/material/Popper';
import MenuItem from '@mui/material/MenuItem';
import MenuList from '@mui/material/MenuList';

import { Link } from 'react-router-dom';

export default function MenuNav() {
  const [diamondOpen, setDiamondOpen] = React.useState(false);
  const [educationOpen, setEducationOpen] = React.useState(false);

  const diamondAnchorRef = React.useRef(null);
  const educationAnchorRef = React.useRef(null);

  const handleDiamondToggle = () => {
    setDiamondOpen((prevOpen) => !prevOpen);
    if (educationOpen) setEducationOpen(false);
  };

  const handleEducationToggle = () => {
    setEducationOpen((prevOpen) => !prevOpen);
    if (diamondOpen) setDiamondOpen(false);
  };

  const handleClose = (event, setState, anchorRef) => {
    if (anchorRef.current && anchorRef.current.contains(event.target)) {
      return;
    }
    setState(false);
  };

  const handleDiamondClose = (event) => {
    handleClose(event, setDiamondOpen, diamondAnchorRef);
  };

  const handleEducationClose = (event) => {
    handleClose(event, setEducationOpen, educationAnchorRef);
  };

  function handleListKeyDown(event, setState) {
    if (event.key === 'Tab') {
      event.preventDefault();
      setState(false);
    } else if (event.key === 'Escape') {
      setState(false);
    }
  }

  // Ensure focus returns to the button when the menu closes
  React.useEffect(() => {
    if (diamondOpen) {
      diamondAnchorRef.current.focus();
    }
    if (educationOpen) {
      educationAnchorRef.current.focus();
    }
  }, [diamondOpen, educationOpen]);

  return (
    <ul id='navButtons'>
      <li>
        <Button
          ref={diamondAnchorRef}
          id="diamond-button"
          aria-controls={diamondOpen ? 'diamond-menu' : undefined}
          aria-expanded={diamondOpen ? 'true' : undefined}
          aria-haspopup="true"
          onClick={handleDiamondToggle}
        >
          Diamond
        </Button>
        <Popper
          open={diamondOpen}
          anchorEl={diamondAnchorRef.current}
          role={undefined}
          placement="bottom-start"
          transition
          disablePortal
        >
          {({ TransitionProps, placement }) => (
            <Grow
              {...TransitionProps}
              style={{
                transformOrigin:
                  placement === 'bottom-start' ? 'left top' : 'left bottom',
              }}
            >
              <Paper>
                <ClickAwayListener onClickAway={handleDiamondClose}>
                  <MenuList
                    autoFocusItem={diamondOpen}
                    id="diamond-menu"
                    aria-labelledby="diamond-button"
                    onKeyDown={(event) => handleListKeyDown(event, setDiamondOpen)}
                  >
                    <MenuItem onClick={handleDiamondClose} component={Link} to="/diamond/">Diamond</MenuItem>
                    <MenuItem onClick={handleDiamondClose} component={Link} to="/ring/">Ring</MenuItem>
                    <MenuItem onClick={handleDiamondClose} component={Link} to="/pendant/">Pendant</MenuItem>
                  </MenuList>
                </ClickAwayListener>
              </Paper>
            </Grow>
          )}
        </Popper>
      </li>
      <li>
        <Button
          ref={educationAnchorRef}
          id="education-button"
          aria-controls={educationOpen ? 'education-menu' : undefined}
          aria-expanded={educationOpen ? 'true' : undefined}
          aria-haspopup="true"
          onClick={handleEducationToggle}
        >
          Education
        </Button>
        <Popper
          open={educationOpen}
          anchorEl={educationAnchorRef.current}
          role={undefined}
          placement="bottom-start"
          transition
          disablePortal
        >
          {({ TransitionProps, placement }) => (
            <Grow
              {...TransitionProps}
              style={{
                transformOrigin:
                  placement === 'bottom-start' ? 'left top' : 'left bottom',
              }}
            >
              <Paper>
                <ClickAwayListener onClickAway={handleEducationClose}>
                  <MenuList
                    autoFocusItem={educationOpen}
                    id="education-menu"
                    aria-labelledby="education-button"
                    onKeyDown={(event) => handleListKeyDown(event, setEducationOpen)}
                  >
                    <MenuItem onClick={handleEducationClose}>Profile</MenuItem>
                    <MenuItem onClick={handleEducationClose}>My account</MenuItem>
                    <MenuItem onClick={handleEducationClose}>Logout</MenuItem>
                  </MenuList>
                </ClickAwayListener>
              </Paper>
            </Grow>
          )}
        </Popper>
      </li>
    </ul>
  );
}

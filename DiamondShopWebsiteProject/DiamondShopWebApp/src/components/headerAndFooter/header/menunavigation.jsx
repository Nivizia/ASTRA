import * as React from 'react';
import {
  Button,
  MenuItem,
  MenuList,
  Paper,
  Popper,
  Grow,
  ClickAwayListener
} from '@mui/material';

import { Link } from 'react-router-dom';

export default function MenuNav() {
  const [diamondOpen, setDiamondOpen] = React.useState(false);
  const [educationOpen, setEducationOpen] = React.useState(false);

  const diamondAnchorRef = React.useRef(null);
  const educationAnchorRef = React.useRef(null);

  const handleDiamondHover = () => {
    setDiamondOpen(true);
    if (educationOpen) setEducationOpen(false);
  };

  const handleEducationHover = () => {
    setEducationOpen(true);
    if (diamondOpen) setDiamondOpen(false);
  };

  const handleCalculatorHover = () => {
    if (diamondOpen) setDiamondOpen(false);
    if (educationOpen) setEducationOpen(false);
  }

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

  return (
    <ul class='nav-button-container'>
      <li>
        <Button
          ref={diamondAnchorRef}
          id="diamond-button"
          aria-controls={diamondOpen ? 'diamond-menu' : undefined}
          aria-expanded={diamondOpen ? 'true' : undefined}
          aria-haspopup="true"
          className="nav-button"
          onMouseEnter={handleDiamondHover}
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
                    className="menu-list"
                    onKeyDown={(event) => handleListKeyDown(event, setDiamondOpen)}
                  >
                    <MenuItem onClick={handleDiamondClose} component={Link} to="/diamond/" className="menu-option">Diamond</MenuItem>
                    <MenuItem onClick={handleDiamondClose} component={Link} to="/ring/" className="menu-option">Ring</MenuItem>
                    <MenuItem onClick={handleDiamondClose} component={Link} to="/pendant/" className="menu-option">Pendant</MenuItem>
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
          className="nav-button"
          onMouseEnter={handleEducationHover}
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
                    className="menu-list"
                    onKeyDown={(event) => handleListKeyDown(event, setEducationOpen)}
                  >
                    <MenuItem onClick={handleEducationClose} className="menu-option">Placeholder</MenuItem>
                    <MenuItem onClick={handleEducationClose} className="menu-option">Placeholder</MenuItem>
                    <MenuItem onClick={handleEducationClose} className="menu-option">Placeholder</MenuItem>
                  </MenuList>
                </ClickAwayListener>
              </Paper>
            </Grow>
          )}
        </Popper>
      </li>
      <li>
        <Button component={Link} to="/calculator/"
          className="nav-button"
          onMouseEnter={handleCalculatorHover}
        >
          Calulator
        </Button>
      </li>
    </ul>
  );
}

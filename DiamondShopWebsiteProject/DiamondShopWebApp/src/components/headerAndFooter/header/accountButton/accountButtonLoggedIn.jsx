// src/components/headerAndFooter/header/accountButton/accountButtonLoggedIn.jsx

import React, { useState, useEffect } from 'react';
import {
    Box,
    Avatar,
    Menu,
    MenuItem,
    ListItemIcon,
    Divider,
    IconButton,
    Tooltip,
} from '@mui/material';
import {
    PersonAdd,
    Settings,
    Logout,
} from '@mui/icons-material';
import { BsPerson } from "react-icons/bs";
import { useNavigate } from 'react-router-dom';

const AccountButtonLoggedIn = ({ user, onLogout }) => {
    const [anchorEl, setAnchorEl] = useState(null);
    const navigate = useNavigate();

    const [hasUserLoggedIn, setHasUserLoggedIn] = useState(!!user);
    const open = Boolean(anchorEl);

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        setHasUserLoggedIn(!!token);
    }, []);

    const handleClick = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleLogout = () => {
        onLogout();
        handleClose();
        navigate('/login');
    };

    const handleProfileClick = () => {
        navigate('/profile');
        handleClose();
    }

    return (
        <React.Fragment>
            <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
                <Tooltip title="Account settings">
                    <IconButton
                        onClick={handleClick}
                        size="small"
                        aria-controls={open ? 'account-menu' : undefined}
                        aria-haspopup="true"
                        aria-expanded={open ? 'true' : undefined}
                    >
                        {
                            user.Username ? (
                                <Avatar sx={{ width: 32, height: 32 }}>{user.Username.slice(0, 2)}</Avatar>
                            ) : (
                                <Avatar sx={{ width: 32, height: 32 }}><BsPerson /></Avatar>
                            )
                        }
                    </IconButton>
                </Tooltip>
            </Box>
            <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleClose}
                PaperProps={{
                    elevation: 0,
                    sx: {
                        overflow: 'visible',
                        filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                        mt: 1.5,
                        '& .MuiAvatar-root': {
                            width: 32,
                            height: 32,
                            ml: -0.5,
                            mr: 1,
                        },
                        '&::before': {
                            content: '""',
                            display: 'block',
                            position: 'absolute',
                            top: 0,
                            right: 14,
                            width: 10,
                            height: 10,
                            bgcolor: 'background.paper',
                            transform: 'translateY(-50%) rotate(45deg)',
                            zIndex: 0,
                        },
                    },
                }}
                transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
            >
                {hasUserLoggedIn && user ? (
                    <MenuItem>
                        Welcome, {user.Username}
                    </MenuItem>
                ) : (
                    <MenuItem>
                        Welcome, Guest
                    </MenuItem>
                )}
                <MenuItem onClick={handleProfileClick}>
                    <Avatar /> Profile
                </MenuItem>
                <Divider />

                <MenuItem onClick={handleLogout}>
                    <ListItemIcon>
                        <Logout fontSize="small" />
                    </ListItemIcon>
                    Logout
                </MenuItem>
            </Menu>
        </React.Fragment>
    );
};

export default AccountButtonLoggedIn;
import React from 'react';
import BottomNavigation from '@mui/material/BottomNavigation';
import BottomNavigationAction from '@mui/material/BottomNavigationAction';
import RestoreIcon from '@mui/icons-material/Restore'; // Assuming this represents "Order History"
import FavoriteIcon from '@mui/icons-material/Favorite'; // Assuming this represents "Account Details"
// Import your screen components
import OrderHistory from './orderhistory.jsx';
import AccountDetails from './accountdetails.jsx';

const Profile = () => {
    const [value, setValue] = React.useState(0);

    // Function to render the current screen based on the navigation state
    const renderScreen = () => {
        switch (value) {
            case 0:
                return <OrderHistory />;
            case 1:
                return <AccountDetails />;
            default:
                return <OrderHistory />;
        }
    };

    return (
        <div>
            <BottomNavigation
                showLabels
                value={value}
                onChange={(event, newValue) => {
                    setValue(newValue);
                }}
            >
                <BottomNavigationAction label="Order History" icon={<RestoreIcon />} />
                <BottomNavigationAction label="Account Details" icon={<FavoriteIcon />} />
            </BottomNavigation>
            {/* Render the current screen */}
            {renderScreen()}
        </div>
    );
};

export default Profile;
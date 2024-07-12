import React, { useState, useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import styles from '../css/accountdetails.module.css';

const AccountDetails = () => {
    const { user } = useContext(AuthContext);
    const [editMode, setEditMode] = useState(false);

    const toggleEditMode = () => {
        setEditMode(!editMode);
    };

    return (
        <div className={styles.container}>
            <h2>Account Details</h2>
            <p>View and manage your personal details and contact information</p>
            <Button
                variant="outlined"
                onClick={toggleEditMode}
                className={styles.editButton}
            >
                {editMode ? 'Cancel Edit' : 'Edit Login Details'}
            </Button>
            <div className={styles.detailsSection}>
                {editMode ? (
                    <form className={styles.editForm}>
                        <TextField
                            label="First Name"
                            defaultValue={user.FirstName}
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Last Name"
                            defaultValue={user.LastName}
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Phone Number"
                            defaultValue={user.phone}
                            fullWidth
                            margin="normal"
                        />
                        <TextField
                            label="Email"
                            defaultValue={user.Email}
                            fullWidth
                            margin="normal"
                            InputProps={{
                                readOnly: true,
                            }}
                        />
                        <Button
                            variant="contained"
                            color="primary"
                            className={styles.saveButton}
                            onClick={toggleEditMode} // Add your save logic here
                        >
                            Save Login Details
                        </Button>
                    </form>
                ) : (
                    <div className={styles.detailsSection}>
                        <div className={styles.detailsColumn}>
                            <h4 className={styles.detailsTitle}>Login Details:</h4>
                            <div className={styles.detailsRow}>
                                <div className={styles.detailsLabel}>First Name:</div>
                                <div className={styles.detailsValue}>{user.FirstName || '-'}</div>
                            </div>
                            <div className={styles.detailsRow}>
                                <div className={styles.detailsLabel}>Last Name:</div>
                                <div className={styles.detailsValue}>{user.LastName || '-'}</div>
                            </div>
                            <div className={styles.detailsRow}>
                                <div className={styles.detailsLabel}>Phone:</div>
                                <div className={styles.detailsValue}>{user.phone || '-'}</div>
                            </div>
                        </div>
                        <div className={styles.detailsColumn}>
                            <div className={styles.detailsRow}>
                                <div className={styles.detailsLabel}>Email:</div>
                                <div className={styles.detailsValue}>{user.Email || '-'}</div>
                            </div>
                            <div className={styles.detailsRow}>
                                <div className={styles.detailsLabel}>Current Password:</div>
                                <div className={styles.detailsValue}>**********</div>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}

export default AccountDetails;

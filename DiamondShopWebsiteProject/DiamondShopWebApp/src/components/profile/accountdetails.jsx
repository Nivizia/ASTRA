import React, { useState, useContext, useEffect } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import { updateCustomer } from '../../../javascript/apiService';
import { jwtDecode } from 'jwt-decode'; // Import jwt-decode

import { Tooltip } from '@mui/material';
import { ImCheckmark } from "react-icons/im";
import { ImCross } from "react-icons/im";

import styles from '../css/profile.module.css';

const AccountDetails = () => {
  const { user } = useContext(AuthContext);
  const [accountUser, setAccountUser] = useState(user);
  const [editMode, setEditMode] = useState({});
  const [editedValues, setEditedValues] = useState({
    FirstName: accountUser?.FirstName,
    LastName: accountUser?.LastName,
    Username: accountUser?.Username,
    Email: accountUser?.Email,
    PhoneNumber: accountUser?.PhoneNumber,
  });
  const [errors, setErrors] = useState({});

  useEffect(() => {
    setAccountUser(user);
    setEditedValues({
      FirstName: user?.FirstName,
      LastName: user?.LastName,
      Username: user?.Username,
      Email: user?.Email,
      PhoneNumber: user?.PhoneNumber
    });
  }, [user]);

  const handleDoubleClick = (field) => {
    setEditMode({ ...editMode, [field]: true });
  };

  const handleChange = (e, field) => {
    setEditedValues({ ...editedValues, [field]: e.target.value });
  };

  const validateFields = () => {
    let valid = true;
    let newErrors = {};

    if (!editedValues.FirstName || editedValues.FirstName.length < 1 || editedValues.FirstName.length > 16) {
      valid = false;
      newErrors.FirstName = 'First name must be between 1 and 16 characters.';
    }

    if (!editedValues.LastName || editedValues.LastName.length < 1 || editedValues.LastName.length > 16) {
      valid = false;
      newErrors.LastName = 'Last name must be between 1 and 16 characters.';
    }

    if (!editedValues.Username || editedValues.Username.length < 1 || editedValues.Username.length > 16) {
      valid = false;
      newErrors.Username = 'Username must be between 1 and 16 characters.';
    }

    if (!editedValues.Email || !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(editedValues.Email) || editedValues.Email.length > 32) {
      valid = false;
      newErrors.Email = 'Email must be a valid format and between 1 and 32 characters.';
    }

    setErrors(newErrors);
    return valid;
  };

  const handleConfirmChange = async (field) => {
    if (!validateFields()) return;

    try {
      const { customer, token } = await updateCustomer(accountUser.sub, editedValues);

      // Update the local storage with the new token
      localStorage.setItem('authToken', token);

      // Decode the new token and update the user state
      const userInfo = jwtDecode(token);
      setAccountUser(userInfo);

      setEditMode({ ...editMode, [field]: false });
    } catch (error) {
      console.error('Error updating customer:', error);
    }
  };

  const handleCancelChange = (field) => {
    setEditedValues({ ...editedValues, [field]: accountUser[field] });
    setEditMode({ ...editMode, [field]: false });
    setErrors({});
  };

  const getTooltipMessage = (field) => {
    switch (field) {
      case 'FirstName':
        return 'Double click to update your first name';
      case 'LastName':
        return 'Double click to update your last name';
      case 'Username':
        return 'Double click to update your username';
      case 'PhoneNumber':
        return 'Double click to update your phone number';
      default:
        return 'Double click to update';
    }
  };

  return (
    <div className={styles.container}>
      <h2>Account Details</h2>
      <p>View and manage your personal details and contact information</p>
      <div className={styles.detailsSection}>
        <div className={styles.detailsColumn}>
          <h4 className={styles.detailsTitle}>Login Details:</h4>
          {['FirstName', 'LastName', 'Username', 'PhoneNumber'].map((field) => (
            <div key={field} className={styles.detailsRow}>
              <div className={styles.detailsLabel}>{field.replace(/([A-Z])/g, ' $1')}:</div>
              {editMode[field] ? (
                <div className={styles.detailsUpdate}>
                  <input
                    className={styles.updateInputField}
                    type="text"
                    value={editedValues[field]}
                    onChange={(e) => handleChange(e, field)}
                    autoFocus
                  />
                  {errors[field] && <div className={styles.error}>{errors[field]}</div>}
                  <div className={styles.buttonUpdateContainer}>
                    <button className={styles.buttonUpdate} onClick={() => handleConfirmChange(field)}><ImCheckmark /></button>
                    <button className={styles.buttonUpdate} onClick={() => handleCancelChange(field)}><ImCross /></button>
                  </div>
                </div>
              ) : (
                <Tooltip title={getTooltipMessage(field)} arrow placement="right">
                  <div
                    className={styles.detailsValue}
                    onDoubleClick={() => handleDoubleClick(field)}
                  >
                    {editedValues[field] || '[Not set yet]'}
                  </div>
                </Tooltip>
              )}
            </div>
          ))}
        </div>
        <div className={styles.detailsColumn}>
          {/* Email Field */}
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>Email:</div>
            {editMode.Email ? (
              <div className={styles.detailsUpdate}>
                <input
                  className={styles.updateInputField}
                  type="email"
                  value={editedValues.Email}
                  onChange={(e) => handleChange(e, 'Email')}
                  autoFocus
                />
                {errors.Email && <div className={styles.error}>{errors.Email}</div>}
                <div className={styles.buttonUpdateContainer}>
                  <button className={styles.buttonUpdate} onClick={() => handleConfirmChange('Email')}><ImCheckmark /></button>
                  <button className={styles.buttonUpdate} onClick={() => handleCancelChange('Email')}><ImCross /></button>
                </div>
              </div>
            ) : (
              <Tooltip title="Double click to update your email" arrow placement="right">
                <div
                  className={styles.detailsValue}
                  onDoubleClick={() => handleDoubleClick('Email')}
                >
                  {editedValues.Email || '[Not set yet]'}
                </div>
              </Tooltip>
            )}
          </div>

          {/* Password Field */}
          <div className={styles.detailsRow}>
            <div className={styles.detailsLabel}>Password:</div>
            <div className={styles.detailsValuePassword}>{'*'.repeat(accountUser?.PasswordLength)}</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AccountDetails;
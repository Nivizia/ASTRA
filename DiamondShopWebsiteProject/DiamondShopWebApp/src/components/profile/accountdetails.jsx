import React, { useState, useContext } from 'react';
import { AuthContext } from '../../contexts/AuthContext';
import { updateCustomer } from '../../../javascript/apiService';
import styles from '../css/profile.module.css';

const AccountDetails = () => {
  const { user } = useContext(AuthContext);
  const [editMode, setEditMode] = useState({});
  const [editedValues, setEditedValues] = useState({
    FirstName: user.FirstName,
    LastName: user.LastName,
    Username: user.Username,
    Email: user.Email,
    Phone: user.Phone,
  });

  const handleDoubleClick = (field) => {
    setEditMode({ ...editMode, [field]: true });
  };

  const handleChange = (e, field) => {
    setEditedValues({ ...editedValues, [field]: e.target.value });
  };

  const handleConfirmChange = async (field) => {
    try {
      await updateCustomer(user.sub, editedValues);
      setEditMode({ ...editMode, [field]: false });
    } catch (error) {
      console.error('Error updating customer:', error);
    }
  };

  const handleCancelChange = (field) => {
    setEditedValues({ ...editedValues, [field]: user[field] });
    setEditMode({ ...editMode, [field]: false });
  };

  return (

    <div className={styles.container}>
      <h2>Account Details</h2>
      <p>View and manage your personal details and contact information</p>
      <div className={styles.detailsSection}>
        <div className={styles.detailsColumn}>
          <h4 className={styles.detailsTitle}>Login Details:</h4>
          {['FirstName', 'LastName', 'Username'].map((field) => (
            <div key={field} className={styles.detailsRow}>
              <div className={styles.detailsLabel}>{field.replace(/([A-Z])/g, ' $1')}:</div>
              {editMode[field] ? (
                <div
                  className={styles.detailsValue}>
                  <input
                    type="text"
                    value={editedValues[field]}
                    onChange={(e) => handleChange(e, field)}
                  />
                  <button onClick={() => handleConfirmChange(field)}>✔️</button>
                  <button onClick={() => handleCancelChange(field)}>❌</button>
                </div>
              ) : (
                <div
                  className={styles.detailsValue}
                  onDoubleClick={() => handleDoubleClick(field)}
                >
                  {editedValues[field] || '[Not set yet]'}
                </div>
              )}
            </div>
          ))}
        </div>
        <div className={styles.detailsColumn}>
          {['Phone', 'Email', 'Password'].map((field) => (
            <div key={field} className={styles.detailsRow}>
              <div className={styles.detailsLabel}>{field.replace(/([A-Z])/g, ' $1')}:</div>
              {editMode[field] ? (
                <div
                  className={styles.detailsValue}>
                  <input
                    type="text"
                    value={editedValues[field]}
                    onChange={(e) => handleChange(e, field)}
                  />
                  <button onClick={() => handleConfirmChange(field)}>✔️</button>
                  <button onClick={() => handleCancelChange(field)}>❌</button>
                </div>
              ) : (
                <div
                  className={styles.detailsValue}
                  onDoubleClick={() => handleDoubleClick(field)}
                >
                  {field === 'Password'
                    ? '*'.repeat(user.PasswordLength)
                    : editedValues[field] || '[Not set yet]'}
                </div>
              )}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default AccountDetails;
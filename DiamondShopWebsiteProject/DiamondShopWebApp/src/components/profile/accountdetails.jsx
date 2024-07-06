import React, { useState, useContext } from 'react'
import { AuthContext } from '../../contexts/AuthContext';

const accountdetails = () => {
    const { user, logout } = useContext(AuthContext);
    const [FirstName, setFirstName] = useState('');

    const handleclick = () => {
        console.log(user);
    }

    return (
        <div>
            <h2>Account Details</h2>
            <p>View and manage your personal details and contact information</p>
            <button onClick={handleclick}>user</button>
        </div>
    )
}

export default accountdetails
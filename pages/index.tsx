import React from 'react';
import Login from '@containers/Login';
import { LoginProvider } from '@contexts/LoginContext';

const index = () => {
    return (
        <LoginProvider>
            <Login />
        </LoginProvider>
    )
}

export default index;

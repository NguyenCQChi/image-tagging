import '@styles/style.css';
import { theme } from '@styles/index';
import { ThemeProvider } from '@emotion/react';
import VerifyAuthentication from '@src/containers/VerifyAuthentication';
import { LoginProvider } from '@contexts/LoginContext';

const MyApp = ({ Component, ...rest } : { Component: any }) => {
    return (
        <LoginProvider>
            <ThemeProvider theme={theme}>
                <VerifyAuthentication />
                <Component {... rest} />
            </ThemeProvider>
        </LoginProvider>
    )
}

export default MyApp;

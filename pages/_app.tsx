import '@styles/style.css';
import { theme } from '@styles/index';
import { ThemeProvider } from '@emotion/react';
import VerifyAuthentication from '@src/containers/VerifyAuthentication';

const MyApp = ({ Component, ...rest } : { Component: any }) => {
    return (
        <ThemeProvider theme={theme}>
            {/* <VerifyAuthentication /> */}
            <Component {... rest} />
        </ThemeProvider>
    )
}

export default MyApp;

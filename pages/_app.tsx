import '@styles/style.css';
import { theme } from '@styles/index';
import { ThemeProvider } from '@emotion/react';

const MyApp = ({ Component, ...rest } : { Component: any }) => {
  return (
    <ThemeProvider theme={theme}>
      <Component {... rest} />
    </ThemeProvider>
  )
}

export default MyApp;
import { createTheme } from '@mui/material/styles';

declare module '@mui/material/styles' {
  interface Palette {
    myBackground: any;
  }

  interface PaletteOptions {
    myBackground: any
  }
}

const { palette } = createTheme();

const theme = createTheme({
  typography: {
    allVariants: {
      fontFamily: 'serif'
    }
  },
  palette: {
    primary: {
      main: '#6D5DD3', //background
      light: '#AC99F2', //my text primary
      dark: '#000000', //my text secondary
    },
    secondary: {
      main: '#fffcf2', //paper background
      light: '#BDE0FE', //other text primary
      dark: '#A2D2FF' //other text secondary
    },
    info: {
      main: '#8758a1',
      dark: '#3e84c7'
    },
    myBackground: palette.augmentColor({
      color: {
        main: '#fffcf2',
        light: '#faf4e1',
        dark: '#e0dac8',
        contrastText: '#c7c1b1'
      }
    })
  }
})

export default theme;
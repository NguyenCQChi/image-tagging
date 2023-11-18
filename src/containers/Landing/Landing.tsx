import { Navigation } from '@components';
import { Box } from '@mui/material';
import { useTheme } from '@mui/material/styles';

const Landing = () => {
  const theme = useTheme()
  const outterContainer = {
    width: '100vw',
    height: '100vh',
    display: 'flex',
    flexDirection: 'column',
    background: 'white'
  }

  const container = {
    flexGrow: 1,
    background: `${theme.palette.myBackground.light}`,
    padding: '35px 35px'
  }
  return (
    <Box sx={outterContainer}>
      <Navigation />
      <Box sx={container}>box</Box>
    </Box>
  )
}

export default Landing;
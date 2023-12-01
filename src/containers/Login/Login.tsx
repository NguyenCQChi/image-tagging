import { useTheme } from '@mui/material/styles';
import { Box, Grid } from '@mui/material';
import { Panel } from './components';
import { Player } from '@lottiefiles/react-lottie-player';

const Login = () => {
  const theme = useTheme();
  const gridStyle = {
    height: '100%',
    width: '100%',
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center'
  }

  const container = {
    background: `linear-gradient(to top right, ${theme.palette.primary.dark} 0%, ${theme.palette.primary.main} 40%, ${theme.palette.primary.main} 60%, ${theme.palette.primary.dark} 100%)`,
    width: '92%', 
    height: '92%',
    borderRadius: '11px'
  }

  const bodyContainer = {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    background: `${theme.palette.primary.light}`,
    height: '100vh',
    width: '100vw',
  }

  const imageContainer = {
    [theme.breakpoints.down('md')]: {
      display: 'none'
    },
  }

  return (
    <Box sx={bodyContainer}>
      <Box sx={{height: '90%', width: '90%'}}>
        <Grid container 
          sx={{...gridStyle,  
            background: `${theme.palette.secondary.main}`, 
            borderRadius: '11px',
            [theme.breakpoints.down('md')]: {
              background: 'transparent'
            },
        }}>
          <Grid item xs={12} md={6} sx={{...gridStyle}}>
            <Box sx={{...gridStyle}}>
              <Panel />
            </Box>
          </Grid>
          <Grid item xs={0} md={6} sx={{...gridStyle, ...imageContainer}}>
            <Box sx={{...gridStyle, ...container}}>
              <div style={{
                width: '100%',
                height: '100%', 
                display: 'flex', 
                flexDirection: 'row',
                alignItems: 'center',
                justifyContent: 'center',
              }}>
                <Player
                  autoplay
                  loop
                  src="https://lottie.host/8171fa86-8c3a-4b78-87dc-5fb46033e500/NxjaOiGPdO.json"
                />
              </div>
            </Box>
          </Grid>
        </Grid>
      </Box>
    </Box>
  )
}

export default Login;
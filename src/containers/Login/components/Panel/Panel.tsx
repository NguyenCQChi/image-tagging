import { useState, useContext } from 'react';
import { Paper, Divider, List, ListItem, Box } from '@mui/material';
import { Button } from '@mui/base';
import CreateAcc from "../CreateAcc";
import LoginPanel from "../LoginPanel";
import { styled, useTheme } from '@mui/material/styles';
import { motion } from 'framer-motion';
import { LoginContext } from '@contexts/LoginContext';

const Panel = () => {
  const theme = useTheme();
  const { isLogin, setIsLogin } = useContext(LoginContext);

  const CustomButton = styled(Button)(({theme}) => ({
    border: 'none',
    backgroundColor: 'transparent',
    fontFamily: 'Times New Roman, serif',
    fontSize: '12px',
    color: `${theme.palette.info.main}`,
    margin: '3px',
    cursor: 'pointer',
    ':hover': {
      color: `${theme.palette.primary.dark}`,
    }
  }))

  const Item = styled(Paper)(({theme}) => ({
    padding: '20px',
    width: '50%'
  }))

  const item = {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  }

  const handleClick = () => {
    setIsLogin(!isLogin)
  }

  return (
    <Item>
      <List sx={{width: '100%'}}>
        <ListItem sx={{...item, mb: '10px'}}>
          <div style={{fontSize: '22px'}}>Welcome to </div>
        </ListItem>
        <Divider/>
        <ListItem sx={{...item, marginY: '10px'}}>
          { isLogin ? <LoginPanel /> : <CreateAcc /> }
        </ListItem>
        <Divider />
        <ListItem sx={{...item, mt: '10px'}}>
          <Box sx={{display: 'flex', flexDirection: 'row', alignItems: 'end'}}>
            <div style={{fontSize: '12px', display: 'flex', flexDirection: 'row', marginBottom: '5px'}}> { isLogin ? "Do not have account? " : "Already have an account?" } </div>
            <motion.div
              className="box"
              whileHover={{scale:1.05}}
              transition={{type: "spring", stiffness: 400, damping: 10}}
            >
              <CustomButton onClick={handleClick}> 
              { isLogin ? "Create Account" : "Login" } 
              </CustomButton> 
            </motion.div>
          </Box>
          { isLogin && (
            <Box sx={{display: 'flex', flexDirection: 'row', alignItems: 'end'}}>
              <motion.div
                className="box"
                whileHover={{scale:1.05}}
                transition={{type: "spring", stiffness: 400, damping: 10}}
              >
                <CustomButton onClick={handleClick}> 
                  Forgot password
                </CustomButton> 
              </motion.div>
            </Box>
          )}
        </ListItem>
      </List>
    </Item>
  )
}

export default Panel;
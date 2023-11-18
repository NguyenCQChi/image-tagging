import { useState } from 'react';
import { Paper, Divider, List, ListItem } from '@mui/material';
import { Button } from '@mui/base';
import CreateAcc from "../CreateAcc";
import LoginPanel from "../LoginPanel";
import { styled } from '@mui/material/styles';
import { motion } from 'framer-motion';
import { useTheme } from '@mui/material/styles';

const Panel = () => {
  const theme = useTheme();
  const [ loginState, setLoginState ] = useState<boolean>(true); 

  const CustomButton = styled(Button)(({theme}) => ({
    border: 'none',
    backgroundColor: 'transparent',
    fontSize: '12px',
    color: theme.palette.info.main,
    margin: '3px',
    cursor: 'pointer',
    ':hover': {
      color: theme.palette.primary.dark,
    }
  }))

  const paperStyle = {
    width: '50%'
  }

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
    setLoginState(!loginState)
  }

  return (
    <Item>
      <List sx={{width: '100%'}}>
        <ListItem sx={{...item, mb: '10px'}}>
          <div style={{fontSize: '22px'}}>Welcome to </div>
        </ListItem>
        <Divider/>
        <ListItem sx={{...item, marginY: '10px'}}>
          { loginState ? <LoginPanel /> : <CreateAcc /> }
        </ListItem>
        <Divider />
        <ListItem sx={{...item, mt: '10px'}}>
          <div style={{fontSize: '12px', display: 'flex', flexDirection: 'row', marginBottom: '5px'}}> { loginState ? "Do not have account? " : "Already have an account?" } </div>
          <motion.div
            className="box"
            whileHover={{scale:1.05}}
            transition={{type: "spring", stiffness: 400, damping: 10}}
          >
            <CustomButton onClick={handleClick}> 
            { loginState ? "Create Account" : "Login" } 
            </CustomButton> 
          </motion.div>
        </ListItem>
      </List>
    </Item>
  )
}

export default Panel;
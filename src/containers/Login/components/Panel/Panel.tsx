import { useState, useContext } from 'react';
import { Paper, Divider, List, ListItem, Box, Modal, Alert } from '@mui/material';
import { Formik, Form as FormBase, FastField } from 'formik';
import * as Yup from 'yup';
import { Button } from '@mui/base';
import CreateAcc from "../CreateAcc";
import LoginPanel from "../LoginPanel";
import { styled, useTheme } from '@mui/material/styles';
import { motion } from 'framer-motion';
import { LoginContext } from '@contexts/LoginContext';
import { Input } from '@components';
import { API_AUTH_SERVER, API_AUTH_RESETPASSWORD } from '@constants/strings';
import { api } from '@src/utils/api';

const Panel = () => {
  const theme = useTheme();
  const { isLogin, setIsLogin, setIsSent } = useContext(LoginContext);
  const [ forgot, setForgot ] = useState(false);
  const [ failToast, setFailToast ] = useState(false)

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

  const SubmitButton = styled(Button)(({theme}) => ({
    width: '100%',
    backgroundColor: `${theme.palette.primary.main}`,
    border: 'none',
    padding: '12px 0',
    borderRadius: '4px',
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

  const modalStyle = {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
  }

  const containerStyle = {
    background: 'white', 
    padding: '30px',
    borderRadius: '11px', 
    border: 'none',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    width: '20%',
    paddingTop: '80px',
    paddingBottom: '80px',
    gap: '20px'
  }

  const buttonContainer = {
    width: '100%',
    display: 'flex',
    justifyContent: 'center',
    marginTop: '10px',
}

const hoverButton = {
    cursor: 'pointer',
    color: 'white'
}

  const mailReg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

  const validationSchema = Yup.object({
    email: Yup.string().required('Email is required').matches(mailReg, 'Email address is not valid')
  })

  const initialValue = {
    email: ''
  }

  const handleClick = () => {
    setIsLogin(!isLogin)
  }

  const handleForgot = () => {
    setForgot(true)
  }

  const handleClose = () => {
    setForgot(false)
  }

  const onSubmit = (value) => {
    console.log('on submit')
    const email = value.email
    const server_url = `${API_AUTH_SERVER}${API_AUTH_RESETPASSWORD}?email=${email}`
    const apiResponse = api.get(server_url);

    apiResponse.then((response) => {
      console.log('get the response back')
      console.log(response)
      setIsSent(true);
      handleClose();
    }, (response) => {
      setFailToast(true)
    })
  }

  return (
    <Item>
      <List sx={{width: '100%'}}>
        <ListItem sx={{...item, mb: '10px'}}>
          <div style={{fontSize: '22px'}}>Welcome</div>
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
            <div>
              <Box sx={{display: 'flex', flexDirection: 'row', alignItems: 'end'}}>
                <motion.div
                  className="box"
                  whileHover={{scale:1.05}}
                  transition={{type: "spring", stiffness: 400, damping: 10}}
                >
                  <CustomButton onClick={handleForgot}> 
                    Forgot password
                  </CustomButton> 
                </motion.div>
              </Box>
              <Modal
                open={forgot}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
                sx={modalStyle}
              >
                <Box sx={containerStyle}>
                  <List sx={{width: '100%'}}>
                    <ListItem sx={{...item, marginBottom: '10px'}}>
                      <div style={{fontSize: '22px'}}>Reset Password</div>
                    </ListItem>
                    <Divider/>
                    <ListItem sx={{...item, marginBottom: '10px'}}>
                      <Formik
                        initialValues={initialValue}
                        validationSchema={validationSchema}
                        onSubmit={onSubmit}
                        validationOnChange
                      >
                        {(formik) => {
                          const { isValid, dirty } = formik;

                          return (
                            <FormBase className='form'>
                              <FastField 
                                name='email'
                                placeholder='Email'
                                required
                                component={Input}
                              />
                              { failToast && <Alert variant='outlined' severity='error'>Email address is not valid!</Alert>}
                              <div style={buttonContainer}>
                                {(isValid && dirty) ? (
                                  <motion.div
                                    className='box'
                                    whileHover={{scale:1.05}}
                                    transition={{type: 'spring', stiffness: 400, damping: 10}}
                                    style={{width: '100%', display: 'flex', flexDirection: 'row', justifyContent: 'center'}}
                                  >
                                    <SubmitButton
                                      disabled={!(isValid && dirty)}
                                      sx={hoverButton}
                                      type='submit'
                                    >
                                      Submit
                                    </SubmitButton>
                                  </motion.div>
                                ) : (
                                  <SubmitButton
                                    disabled={true}
                                    sx={{width: '100%'}}
                                  >
                                    Submit
                                  </SubmitButton>
                                )}
                              </div>
                            </FormBase>
                          )
                        }}
                      </Formik>
                    </ListItem>
                  </List>
                </Box>
              </Modal>
            </div>
          )}
        </ListItem>
      </List>
    </Item>
  )
}

export default Panel;
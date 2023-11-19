import React, { useState, useContext } from 'react';
import * as Yup from 'yup';
import { Formik, Form as FormBase, FastField } from 'formik';
import { Input, PasswordInput } from '@components';
import { styled } from '@mui/material/styles';
import { Button } from '@mui/base';  
import { motion } from 'framer-motion';
import { Alert } from '@mui/material';
import { useRouter } from 'next/router';

const CreateAcc = () => {
  const router = useRouter()
  const [ failToast, setFailToast ] = useState(false);

  const mailReg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;

  const validationSchema = Yup.object({
    name: Yup.string().required('Username is required').min(1, 'Username is required'),
    email: Yup.string().required('Email is required').matches(mailReg, 'Email address is not valid'),
    password: Yup.string().required('Password is required').min(8, 'Password should be of minimum 8 characters length')
  })

  const initialValue = {
    name: '',
    email: '',
    password: '',
  }

  const onSubmit = async (value: any) => {
    console.log(value);
    //TO-DO: Create a new user
    // router.push('/landing')
  }

  const CustomButton = styled(Button)(({theme}) => ({
    width: '90%',
    backgroundColor: `${theme.palette.primary.main}`,
    border: 'none',
    padding: '12px 0',
    borderRadius: '4px',
  }))

  const buttonContainer = {
    width: '100%',
    display: 'flex',
    justifyContent: 'center',
    marginTop: '10px',
  }

  const hoverButton = {
    cursor: 'pointer'
  }

  return (
    <Formik
      initialValues={initialValue}
      validationSchema={validationSchema}
      onSubmit={onSubmit}
      validateOnChange
    >
      {(formik) => {
        const { isValid, dirty } = formik;

        return (
          <FormBase className='form'>
            <FastField 
              name='name'
              placeholder='Username'
              required
              component={Input}
            />
            <FastField 
              name='email'
              placeholder='Email'
              required
              component={Input}
            />
            <FastField 
              name='password'
              placeholder='Password'
              required
              component={PasswordInput}
              />
            { failToast && <Alert variant='outlined' severity='error'> Cannot create account! </Alert> }
            <div style={buttonContainer}>
              {(isValid && dirty) ? (
                <motion.div
                  className='box'
                  whileHover={{scale:1.05}}
                  transition={{type: 'spring', stiffness: 400, damping: 10}}
                  style={{width: '60%', display: 'flex', flexDirection: 'row', justifyContent: 'center'}}
                >
                  <CustomButton
                    disabled={!(isValid && dirty)}
                    sx={hoverButton}
                    type='submit'
                  >
                    Create
                  </CustomButton>
                </motion.div>
              ) : (
                <CustomButton
                  disabled={true}
                  sx={{width: '60%'}}
                >
                  Create
                </CustomButton>
              )}
            </div>
          </FormBase>
        )
      }}
    </Formik>
  )
}

export default CreateAcc;

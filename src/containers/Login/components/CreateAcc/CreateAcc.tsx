import React, { useState, useContext } from 'react';
import * as Yup from 'yup';
import { Formik, Form as FormBase, FastField } from 'formik';
import { Input, PasswordInput } from '@components';
import { styled } from '@mui/material/styles';
import { Button } from '@mui/base';  
import { motion } from 'framer-motion';
import { Alert } from '@mui/material';
import {
    API_AUTH_REGISTER,
    API_AUTH_SERVER,
    ROLE_USER,
    ROLE_ADMIN,
} from '@src/constants/strings';
import { string_object } from '@src/constants/hardcoded_string';
import { LoginContext } from '@src/contexts/LoginContext';

const CreateAcc = () => {
  const [ failToast, setFailToast ] = useState(false);
  const { setIsLogin } = useContext(LoginContext);

    const mailReg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    const passwordReg = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)/;

    const validationSchema = Yup.object({
        name: Yup.string().required(string_object.VALIDATION.NAME).min(1, string_object.VALIDATION.NAME),
        userName: Yup.string().required(string_object.VALIDATION.USER_NAME).min(1, string_object.VALIDATION.USER_NAME),
        email: Yup.string().required(string_object.VALIDATION.EMAIL_REQUIRED).matches(mailReg, string_object.VALIDATION.EMAIL_VALID),
        password: Yup.string().required(string_object.VALIDATION.PASSWORD_REQUIRED).min(6, string_object.VALIDATION.PASSWORD_LENGTH).matches(passwordReg, string_object.VALIDATION.PASSWORD_CHECK)
    })

    const initialValue = {
        name: '',
        userName: '',
        email: '',
        password: '',
    }

    const onSubmit = async (value: any) => {
        const email = value.email
        const name = value.name
        const userName = value.userName
        const password = value.password

        let account_information = {
            "userName": userName,
            "name": name,
            "password": password,
            "role": ROLE_USER,
            "email": email,
        }
        console.log(JSON.stringify(account_information))
        // Perform the login API request
        const server_url = `${API_AUTH_SERVER}${API_AUTH_REGISTER}`

        const response = await fetch(server_url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(account_information),
        });

        const data = await response.json();
        if (response.ok) {
            console.log("Register Success")
            setFailToast(false)
            setIsLogin(true)
        } else {
            // Handle login error
            console.error('Register Failed');
            console.log(response)
            setFailToast(true)
        }
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
        cursor: 'pointer',
        color: 'white'
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
                            placeholder='Name'
                            required
                            component={Input}
                        />
                        <FastField 
                            name='userName'
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

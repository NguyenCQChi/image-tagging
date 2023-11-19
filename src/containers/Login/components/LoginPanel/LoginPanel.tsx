import React, { useState } from 'react';
import * as Yup from 'yup';
import { Formik, Form as FormBase, FastField } from 'formik';
import { Input, PasswordInput } from '@components';
import { styled, useTheme } from '@mui/material/styles';
import { Button } from '@mui/base';
import { motion } from 'framer-motion';
import { Alert } from '@mui/material';
import { useRouter } from 'next/router';
import {
    ADMIN_USERNAME,
    ADMIN_PASSWORD,
    USER_TEST,
    USER_PASSWORD,
    API_AUTH_REGISTER,
    API_AUTH_LOGIN,
    API_AUTH_SERVER,
} from '@constants/strings';

const LoginPanel = () => {
    const router = useRouter();
    const [ failToast, setFailToast ] = useState(false);

    const validationSchema = Yup.object({
        username: Yup.string().required('User name is required'),
        password: Yup.string().required('Password is required').min(1, 'Password should be of minimum 1 characters length')
    })

    const initialValue = {
        username: '',
        password: '',
    }

    const onSubmit = async (value: any) => {
        console.log(value)

        // TO-DO: implement client request to login
        // if(value.username === ADMIN_USERNAME && value.password === ADMIN_PASSWORD) {
        //     router.push('/admin')
        // } else if(value.username === USER_TEST && value.password === USER_PASSWORD) {
        //     router.push('/landing')
        // }

        let account_information = {
            "userName": value.username,
            "password": value.password
        }

        // Perform the login API request
        const server_url = `${API_AUTH_SERVER}${API_AUTH_LOGIN}`

        const response = await fetch(server_url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(account_information),
        });

        if (response.ok) {
            const data = await response.json();

            console.log(data)
            // Store tokens securely (in this example, using localStorage)
            localStorage.setItem('accessToken', data.result.accessToken);
            localStorage.setItem('refreshToken', data.result.refreshToken);
            // Redirect to a protected page
            router.push('/landing');
            console.log("Login Success!")
        } else {
            // Handle login error
            console.error('Login failed');
            // router.push('/landing')
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
                            name='username'
                            placeholder='Username'
                            required
                            component={Input}
                        />
                        <FastField 
                            name='password'
                            placeholder='Password'
                            required
                            component={PasswordInput}
                        />
                        { failToast && <Alert variant='outlined' severity='error'> Incorrect username or password! Please try again </Alert> }
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
                                        sx={{...hoverButton, color: 'white'}}
                                        type='submit'
                                    >
                                        Login
                                    </CustomButton>
                                </motion.div>
                            ) : (
                                    <CustomButton
                                        disabled={true}
                                        sx={{width: '60%'}}
                                    >
                                        Login
                                    </CustomButton>
                                )}
                        </div>
                    </FormBase>
                )
            }}
        </Formik>
    )
}

export default LoginPanel;

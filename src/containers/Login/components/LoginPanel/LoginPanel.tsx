import React, { useState, useContext, useEffect } from 'react';
import * as Yup from 'yup';
import { Formik, Form as FormBase, FastField } from 'formik';
import { Input, PasswordInput } from '@components';
import { styled, useTheme } from '@mui/material/styles';
import { Button } from '@mui/base';
import { Alert } from '@mui/material';
import { motion } from 'framer-motion';
import { useRouter } from 'next/router';
import { decode } from "jsonwebtoken";
import {
    ROLE_USER,
    ROLE_ADMIN,
    API_AUTH_LOGIN,
    API_AUTH_SERVER,
} from '@src/constants/strings';
import { string_object } from '@src/constants/hardcoded_string';
import { LoginContext } from '@contexts/LoginContext';

const LoginPanel = () => {
    const router = useRouter();
    const [ failToast, setFailToast ] = useState(false);
    const [ errMsg, setErrMsg ] = useState(string_object.LOG_IN_FAIL)
    const [ successToast, setSuccessToast ] = useState(false)
    const { isSent, setIsSent } = useContext(LoginContext);

    const validationSchema = Yup.object({
        username: Yup.string().required(string_object.VALIDATION.USER_NAME),
        password: Yup.string().required(string_object.VALIDATION.PASSWORD_REQUIRED).min(6, string_object.VALIDATION.PASSWORD_LENGTH)
    })

    const initialValue = {
        username: '',
        password: '',
    }

    const onSubmit = async (value: any) => {
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

            // Redirect to a protected page depending on account type
            const decodedToken = decode(data.result.accessToken) as { [key: string]: any } | null;

            const userRole = decodedToken.role

            if (userRole == ROLE_USER) {
                router.push('/landing')
            } else if (userRole == ROLE_ADMIN) {
                router.push('/admin')
            } else {
                console.log("Undefined user role")
            }
        } else {
            // Handle login error
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
    }

    const toastContainer = {
        position: 'fixed',
        bottom: 0,
        left: 0,
    }

    useEffect(() => {
        if(isSent) {
            setSuccessToast(true);
            setTimeout(() => {
                setSuccessToast(false);
                setIsSent(false)
            }, 7000)
        }
    }, [isSent])

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
                        { failToast && <Alert variant='outlined' severity='error'> {errMsg} </Alert> }
                        { successToast && (
                            <motion.div
                                animate={{ x: 100 }}
                                transition={{ delay: 1 }}
                            >
                                <Alert variant='outlined' severity='success'> {string_object.VALIDATION.PASSWORD_RESET} </Alert>
                            </motion.div>
                        )}
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

import React, { useState, useEffect, useContext } from 'react';
import { Box } from '@mui/material';
import { useTheme, styled } from '@mui/material/styles';
import { motion } from 'framer-motion';
import { Button } from '@mui/base';
import { useRouter } from 'next/router';
import { api } from '@src/utils/api';
import {
    API_AUTH_ACCESS_TOKEN,
    API_AUTH_REFRESH_TOKEN,
    API_AUTH_REVOKE,
    API_AUTH_SERVER,
} from '@src/constants/strings';
import { string_object } from '@src/constants/hardcoded_string';
import { LoginContext } from '@contexts/LoginContext';

const Navigation = ({ admin } : { admin? : boolean }) => {
    const router = useRouter();
    const theme = useTheme()
    const [ isScrolled, setIsScrolled ] = useState(false)
    const { isImageCaption, setIsImageCaption, setSignoutFail } = useContext(LoginContext)

    const container = {
        padding: '15px 35px',
        background: isScrolled ? `${theme.palette.secondary.main}` : `${theme.palette.primary.light}`,
        boxShadow: isScrolled ? `0 6px 10px -6px rgba(0, 0, 0, 0.5)` : `0 6px 10px -6px ${theme.palette.primary.main}`,
        zIndex: 100,
        position: 'sticky',
        display: 'flex',
        justifyContent: admin ? 'end' : 'space-between',
        alignItems: 'center',
        top: 0,
        transition: 'background-color 0.5s ease'
    }

    const CustomButton = styled(Button)(({theme}) => ({
        backgroundColor: `${theme.palette.primary.main}`,
        border: 'none',
        padding: '12px 16px',
        borderRadius: '4px',
        color: isScrolled ? 'black' : 'white',
        '&:hover': {
            cursor: 'pointer',
            backgroundColor: `${theme.palette.primary.main}`,
        }
    }))

    const CustomButtonNav = styled(Button)(({theme}) => ({
        border: 'none',
        backgroundColor: 'transparent',
        fontFamily: 'Times New Roman, serif',
        fontSize: '16px',
        color: 'white',
        margin: '3px',
        cursor: 'pointer',
        ':hover': {
          color: `${theme.palette.primary.dark}`,
          transition: '0.3s ease-in-out',
        }
      }))

    const signOut = (e) => {
        // TO-DO: display front end with sign out error
        const accessToken = localStorage.getItem(API_AUTH_ACCESS_TOKEN);
        const refreshToken = localStorage.getItem(API_AUTH_REFRESH_TOKEN);
        const delete_body = {
            [API_AUTH_ACCESS_TOKEN]: accessToken,
            [API_AUTH_REFRESH_TOKEN]: refreshToken,
        }
        const response = api.delete(`${API_AUTH_SERVER}${API_AUTH_REVOKE}`, {data: delete_body});
        response.then(
            () => {
                console.log(string_object.SIGN_OUT_SUCCESS)
            },
            () => {
                console.log(string_object.SIGN_OUT_FAIL)
                setSignoutFail(true)
            }
        )
        localStorage.removeItem(API_AUTH_REFRESH_TOKEN);
        localStorage.removeItem(API_AUTH_ACCESS_TOKEN);
        router.push('/')
        e.preventDefault();
    }

    const handleScroll = () => {
        setIsScrolled(window.scrollY > 150);
    }

    const handleClick = () => {
        setIsImageCaption(!isImageCaption)
    }

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        }
    }, [])

    return (
        <Box sx={container}>
            {!admin && (
                <CustomButtonNav onClick={handleClick}>
                    {isImageCaption ? "History" : "Home"}
                </CustomButtonNav>
            )}
            <motion.div
                className='box'
                whileHover={{scale:1.05}}
                transition={{type: 'spring', stiffness: 400, damping: 10}}
            >
                <CustomButton onClick={signOut}>
                    Sign out
                </CustomButton>
            </motion.div>
        </Box>
    )
}

export default Navigation;

import React, { useState, useEffect } from 'react';
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
} from '@constants/strings';

const Navigation = () => {
    const router = useRouter();
    const theme = useTheme()
    const [ isScrolled, setIsScrolled ] = useState(false)

    const container = {
        padding: '15px 35px',
        background: isScrolled ? `${theme.palette.secondary.main}` : `${theme.palette.primary.light}`,
        boxShadow: isScrolled ? `0 6px 10px -6px rgba(0, 0, 0, 0.5)` : `0 6px 10px -6px ${theme.palette.primary.main}`,
        zIndex: 100,
        position: 'sticky',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        top: 0,
        transition: 'background-color 0.5s ease'
    }

    const CustomButton = styled(Button)(({theme}) => ({
        backgroundColor: `${theme.palette.primary.main}`,
        border: 'none',
        padding: '12px 16px',
        borderRadius: '4px',
        color: 'white',
        '&:hover': {
            cursor: 'pointer',
            backgroundColor: `${theme.palette.primary.main}`,
        }
    }))

    const signOut = (e) => {
        // TO-DO: add more functionality to sign out if needed
        console.log("Signing out");
        const accessToken = localStorage.getItem(API_AUTH_ACCESS_TOKEN);
        const refreshToken = localStorage.getItem(API_AUTH_REFRESH_TOKEN);
        const post_body = {
            [API_AUTH_ACCESS_TOKEN]: accessToken,
            [API_AUTH_REFRESH_TOKEN]: refreshToken,
        }
        const response = api.post(`${API_AUTH_SERVER}${API_AUTH_REVOKE}`, post_body)
        response.then(
            () => {
                console.log("Signed out successfully")
            },
            () => {
                console.log("Error when trying to log out")
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

    useEffect(() => {
        window.addEventListener('scroll', handleScroll);

        return () => {
            window.removeEventListener('scroll', handleScroll);
        }
    }, [])

    return (
        <Box sx={container}>
            <div>Navigation</div>
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

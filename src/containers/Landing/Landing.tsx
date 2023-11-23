import React, { useState } from 'react';
import { Navigation } from '@components';
import { Box, Skeleton } from '@mui/material';
import { useTheme, styled } from '@mui/material/styles';
import { TextField } from '@mui/material';
import { Button } from '@mui/base';
import { motion } from 'framer-motion';
import { api } from '@src/utils/api';
import {
    API_IMAGE_SERVER,
    API_IMAGE_GET_CAPTION,
    API_IMAGE_GET_CAPTION_URL_PARAM,
} from '@constants/strings';

const Landing = () => {
    const theme = useTheme()
    const [ value, handleChange ] = useState(null)
    const [imageLoading, setImageLoading] = useState(false);
    const [pulsing, setPulsing] = useState(true);
    const [ submit, setSubmit ] = useState(false);
    const [ result, setResult ] = useState('');
    const [ link, setLink ] = useState('')

    const outterContainer = {
        width: '100vw',
        height: '100vh',
        display: 'flex',
        flexDirection: 'column',
        background: 'white'
    }

    const container = {
        flexGrow: 1,
        background: `${theme.palette.myBackground.light}`,
        padding: '20px 35px'
    }

    const formContainer = {
        display: 'flex',
        flexDirection: 'row',
        gap: '35px'
    }

    const CustomButton = styled(Button)(({theme}) => ({
        backgroundColor: `${theme.palette.primary.main}`,
        border: 'none',
        padding: '12px 0',
        borderRadius: '4px',
        color: 'white',
        width: '100%',
        '&:hover': {
            cursor: 'pointer'
        }
    }))

    const imageContainer = {
        marginTop: '35px',
        display: 'flex',
        justifyContent: 'center'
    }

    const resultContainer = {
        marginTop: '35px',
        marginBottom: '35px',

        padding: '0 50px'
    }

    const imageLoaded = () => {
        setSubmit(false)
        setImageLoading(false);
        setTimeout(() => setPulsing(false), 600);
    };

    const onSubmit = () => {
        setSubmit(true)
        setLink(value)
        // TO-DO: Submit the link
        console.log(value)
        const imageGetURL = `${API_IMAGE_SERVER}${API_IMAGE_GET_CAPTION}?${API_IMAGE_GET_CAPTION_URL_PARAM}=${value}`
        const response = api.get(imageGetURL)
        console.log(response)
        response.then((response) => {
            console.log(response.data)
            setResult(response.data.caption)
        })
    }


    return (
        <Box sx={outterContainer}>
            <Navigation />
            <Box sx={container}>
                <h1 style={{textAlign: 'center'}}>Get your Images tagging</h1>
                <Box sx={formContainer}>
                    <TextField
                        label='Image link'
                        variant='outlined'
                        onChange={(event) => handleChange(event.target.value)}
                        sx={{width: '80%', fontSize: '14px'}}
                        size="small"
                    >
                        Image link
                    </TextField>
                    <motion.div
                        whileHover={{scale:1.05}}
                        transition={{type: 'spring', stiffness: 400, damping: 10}}
                        style={{flexGrow: 1}}
                    >
                        <CustomButton onClick={onSubmit}>Get caption</CustomButton>
                    </motion.div>
                </Box>
                <Box sx={imageContainer}>
                    {!submit ? (
                        <Skeleton  variant="rectangular" sx={{width: '65%', height: '20rem', borderRadius: '11px'}} />
                    ) : (
                            <div
                                className={`${pulsing ? "pulse" : ""} loadable`}
                                style={{ background: "#ccc", width: '50%' }}
                            >
                                <motion.img
                                    initial={{ height: "20rem", opacity: 0 }}
                                    animate={{
                                        height: imageLoading ? "20rem" : "auto",
                                        opacity: imageLoading ? 0 : 1
                                    }}
                                    transition={{opacity: { delay: 0.5, duration: 0.4 }}}
                                    onLoad={imageLoaded}
                                    width="100%"
                                    src={link}
                                />
                            </div>
                        )}
                </Box>
                <Box sx={resultContainer}>
                    <div>Result: {result}</div>
                </Box>
            </Box>
        </Box>
    )
}

export default Landing;

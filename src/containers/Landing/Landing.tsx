import React, { useState, useContext, useEffect } from 'react';
import { Navigation } from '@components';
import { Box, Skeleton, CircularProgress } from '@mui/material';
import { useTheme, styled } from '@mui/material/styles';
import { TextField } from '@mui/material';
import { Button } from '@mui/base';
import { motion } from 'framer-motion';
import { api } from '@src/utils/api';
import {
    API_IMAGE_SERVER,
    API_IMAGE_GET_CAPTION,
    API_IMAGE_GET_CAPTION_URL_PARAM,
    API_GET_ENTRIES
} from '@src/constants/strings';
import { string_object } from '@src/constants/hardcoded_string';
import { LoginContext } from '@contexts/LoginContext'; 
import { History } from './components';
import { Alert } from '@mui/material';

const Landing = () => {
    const theme = useTheme()
    const [ value, handleChange ] = useState(null)
    const [imageLoading, setImageLoading] = useState(true);
    const [pulsing, setPulsing] = useState(true);
    const [ submit, setSubmit ] = useState(false);
    const [ result, setResult ] = useState<any>();
    const [ link, setLink ] = useState('')
    const [ totalEntries, setEntries ] = useState(0)
    const [ passLimit, setPassLimit ] = useState(false)
    const { isImageCaption } = useContext(LoginContext)

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
        padding: '20px 35px',
        display: 'flex',
        flexDirection: 'column',
    }

    const formContainer = {
        display: 'flex',
        flexDirection: 'row',
        gap: '35px',
        [theme.breakpoints.down('md')]: {
            flexDirection: 'column',
            gap: '20px'
        },
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

    const CustomAlert = styled(Alert) (({theme}) => ({
        transform: 'none !important'
    }))

    const imageContainer = {
        marginTop: '35px',
        display: 'flex',
        justifyContent: 'center'
    }

    const resultContainer = {
        marginTop: '35px',
        padding: '0 50px',
        flexGrow: 1,
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'space-between',
        marginBottom: '35px',
        [theme.breakpoints.down('md')]: {
            padding: 0
        },
    }

    const imageLoaded = () => {
        setImageLoading(false);
        setTimeout(() => setPulsing(false), 600);
    };

    const onSubmit = () => {
        setSubmit(true);
        setLink(value);
        setResult(<CircularProgress sx={{color: `${theme.palette.primary.light}`}} />)
        // TO-DO: Submit the link
        const imageGetURL = `${API_IMAGE_SERVER}${API_IMAGE_GET_CAPTION}?${API_IMAGE_GET_CAPTION_URL_PARAM}=${value}`;
        const apiResponse = api.get(imageGetURL);
        apiResponse.then((response) => {
            const entries = totalEntries + 1
            console.log('getting data back and entries ' + entries);
            setEntries(entries)
            setResult(response.data.caption);
        }, (res) => setResult(res.data))
    }

    useEffect(() => {
        console.log(totalEntries)
        if(totalEntries > 20) {
            console.log('pass limit')
            setPassLimit(true)
        }

        setTimeout(() => {
            setPassLimit(false)
        }, 5000)
    }, [totalEntries])

    useEffect(() => {
        const url = `${API_IMAGE_SERVER}${API_GET_ENTRIES}`
        const apiResponse = api.get(url)

        apiResponse.then((res) => {
        console.log(res)
        setEntries(res.data.TotalEntries)
        }, (res) => {
        console.log(res)
        })
    }, [])

    return (
        <Box sx={outterContainer}>
            <Navigation />
            {isImageCaption ? (
                <Box sx={container}>
                    <h1 style={{textAlign: 'center'}}>Get your Images tagging</h1>
                    <Box sx={formContainer}>
                        <TextField
                            label='Image link'
                            variant='outlined'
                            onChange={(event) => handleChange(event.target.value)}
                            sx={{width: '80%', fontSize: '14px', [theme.breakpoints.down('md')]: {width: '100%'}}}
                            size="small"
                        >
                            {string_object.IMAGE_LINK}
                        </TextField>
                        <motion.div
                            whileHover={{scale:1.05}}
                            transition={{type: 'spring', stiffness: 400, damping: 10}}
                            style={{flexGrow: 1}}
                        >
                            <CustomButton onClick={onSubmit}>{string_object.GET_CAPTION}</CustomButton>
                        </motion.div>
                    </Box>
                    <Box sx={imageContainer}>
                        {!submit ? (
                            <Skeleton  variant="rectangular" sx={{width: '50%', height: '20rem', borderRadius: '11px', [theme.breakpoints.down('md')]: {width: '100%'}}} />
                        ) : (
                                <div
                                    className={`${pulsing ? "pulse" : ""} loadable`}
                                    style={{ background: "#ccc", width: '50%', height: 'auto', [theme.breakpoints.down('md')]: {width: '100%'}}}
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
                        <Box sx={{width: 'fit-content'}}>
                        {passLimit && (
                            <Alert variant='outlined' severity='error'> {string_object.PASS_LIMIT} </Alert>
                        )}
                        </Box>
                    </Box>
                </Box>
            ) : (
                <Box sx={container}>
                    <History />
                </Box>    
            )}
        </Box>
    )
}

export default Landing;

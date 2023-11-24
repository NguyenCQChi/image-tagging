import React, { useState, useEffect } from 'react';
import { Navigation } from "@components";
import { Box, Table } from '@mui/material';
import { useTheme } from "@mui/material/styles";
import { UserTable } from './components';

const Admin = () => {
  const theme = useTheme();
  const [ userList, setUserList ] = useState([])

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

  useEffect(() => {
    const refreshToken = localStorage.getItem('refreshToken')
    const accessToken = localStorage.getItem('accessToken')
    console.log('refresh token: ' + refreshToken)
    console.log('access token: ' + accessToken)
    //TO-DO: Get all the users from the database and set to userList
    // setUserList()
  }, [])

  return (
    <Box sx={outterContainer}>
      <Navigation />
      <Box sx={container}>
        <h1 style={{textAlign: 'center'}}> All Users </h1>
        {/* <UserTable users={userList}/> */}
      </Box>
    </Box>
  )
}

export default Admin;
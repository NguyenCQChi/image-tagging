import React, { useState, useEffect } from 'react';
import { Navigation } from "@components";
import { Box, Table } from '@mui/material';
import { useTheme } from "@mui/material/styles";
import { UserTable } from './components';
import { API_AUTH_GET_USERS, API_AUTH_SERVER } from '@constants/strings';

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

  const getUsers = async() => {
    console.log('getting users')
    const refreshToken = localStorage.getItem('refreshToken')
    const accessToken = localStorage.getItem('accessToken')
    
    const server_url = `${API_AUTH_SERVER}${API_AUTH_GET_USERS}`

    const response = await fetch(server_url, { 
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${accessToken}`,
        'X-Refresh-Token': refreshToken
      }
    })

    if(response.ok) {
      const data = await response.json();
      console.log(data)
    } else {
      console.log('Cannot get users')
    }
  }

  useEffect(() => {
    getUsers()
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
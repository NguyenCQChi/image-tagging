import React, { useState, useEffect } from 'react';
import { Navigation } from "@components";
import { Box, Table } from '@mui/material';
import { useTheme } from "@mui/material/styles";
import { API_AUTH_GET_USERS, API_AUTH_SERVER, users } from '@constants/strings';
import { api } from '@src/utils/api';
import { UserTable } from './components';
import dynamic from 'next/dynamic';

// const UserTable = dynamic(() => import('./components'))

// {
//   "id": "2005d64f-efeb-4bcb-988e-8854a3af08fb",
//   "userName": "manjot",
//   "name": "manjot",
//   "email": "manjotsinghrandhawa.beprod16@pec.edu.in"
// },

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
    padding: '20px 35px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
  }

  const tableContainer = {
    width: '80%'
  }

  useEffect(() => {
    setUserList(users.result)
    const server_url = `${API_AUTH_SERVER}${API_AUTH_GET_USERS}`

    const apiResponse = api.get(server_url);

    apiResponse.then((response) => {
      console.log(response)
    })
  }, [])

  return (
    <Box sx={outterContainer}>
      <Navigation admin={true}/>
      <Box sx={container}>
        <h1 style={{textAlign: 'center'}}> All Users </h1>
        <Box sx={tableContainer}>
          <UserTable users={userList}/>
        </Box>
      </Box>
    </Box>
  )
}

export default Admin;
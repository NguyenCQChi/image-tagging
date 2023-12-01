import React, { useState, useEffect } from 'react';
import { Navigation } from "@components";
import { Box } from '@mui/material';
import { useTheme } from "@mui/material/styles";
import { API_AUTH_GET_USERS, API_AUTH_SERVER, API_USER_STAT, API_IMAGE_SERVER } from '@src/constants/strings';
import { api } from '@src/utils/api';
import { UserTable } from './components';
import { history } from '@src/constants/hardcoded_string';

const Admin = () => {
  const theme = useTheme();
  const [ userList, setUserList ] = useState([]);

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
    width: '80%',
    [theme.breakpoints.down('md')]: {
      width: '100%'
    },
  }

  useEffect(() => {
    const server_url = `${API_AUTH_SERVER}${API_AUTH_GET_USERS}`
    const apiResponse = api.get(server_url);

    apiResponse.then((response) => {
      console.log('user list from Manjot')
      console.log(response.data.result)
      setUserList(response.data.result)
    }, (res) => console.log(res))
  }, [])

  useEffect(() => {
    console.log('trying to get userList')
    userList.forEach(user => {
      const const_server = `${API_IMAGE_SERVER}${API_USER_STAT}?userID=${user.userName}`
      console.log(const_server)
      console.log(user.userName)
      const server_url = 'https://simarcodes.com/COMP4537/projects/image-caption/api/v1/user-stats?userID=Chichi1901'
      // https://simarcodes.com/COMP4537/projects/image-caption/api/v1/user-stats?userID=Chichi1901
      const apiResponse = api.get(const_server);

      apiResponse.then((response) => {
        console.log(response)
      }, (res) => {console.log(res)})
    })
  }, [userList])

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
import * as React from 'react';
import { 
  Paper, 
  Table, 
  TableBody, 
  TableCell, 
  TableContainer, 
  TableHead, 
  TablePagination, 
  TableRow,
  TableFooter,
  Collapse,
  IconButton, 
  Box,
  Typography
} from '@mui/material';
import { KeyboardArrowDown, KeyboardArrowUp, KeyboardArrowLeft, KeyboardArrowRight, FirstPage, LastPage } from '@mui/icons-material';
import { UserType } from '@src/types/users.type';
import dynamic from 'next/dynamic';

const createData = (
  userName: string,
  email: string,
  token: string,
  num_request: number,
  stat_array: any
) : UserType => {
  return {
    userName,
    email,
    token,
    num_request,
    stat: [

    ]
  }
}

interface TablePaginationActionsProps {
  count: number;
  page: number;
  rowsPerPage: number;
  onPageChange: (
    e: React.MouseEvent<HTMLButtonElement>,
    newPage: number
  ) => void;
}

const Row = ({ row } : { row: UserType}) => {
  const [ open, setOpen ] = React.useState(false);
  return (
    <Box>
      <TableRow sx={{ 
        width: '100%',
        '& > *': { borderBottom: 'unset' }
      }}>
        <TableCell>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.userName}
        </TableCell>
        <TableCell align="right">{row.email}</TableCell>
        <TableCell align="right">row.token</TableCell>
        <TableCell align="right">row.num_request</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{paddingBottom: 0, paddingTop: 0}} colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{margin: 1}}>
              <Typography variant="h6" gutterBottom component="div">
                User Stat
              </Typography>
              <Table size="small" aria-label="purchase">
                <TableHead>
                  <TableRow>
                    <TableCell>Method</TableCell>
                    <TableCell>Endpoint</TableCell>
                    <TableCell>Requests</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {/* {row.stat} */}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </Box>
  )
}

const UserTable = ({ users, props } : { users : any[], props? : TablePaginationActionsProps}) => {
  const tableStyle = {
    width: '100%',
  }
  return (
    <TableContainer component={Paper} sx={tableStyle}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableCell></TableCell>
          <TableCell>User Name</TableCell>
          <TableCell>Email</TableCell>
          <TableCell>Token</TableCell>
          <TableCell>Number of requests</TableCell>
        </TableHead>
        <TableBody>
          {users.map((user, index) => <Row key={index} row={user} />)}
        </TableBody>
      </Table>
    </TableContainer>
  )
}

export default UserTable;
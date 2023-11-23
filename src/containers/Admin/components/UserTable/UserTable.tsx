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

const createData = (
  username: string,
  email: string,
  token: string,
  num_request: number,
  stat_array: any
) : UserType => {
  return {
    username,
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

const Row = (props : { row: UserType}) => {
  const { row } = props;
  const [ open, setOpen ] = React.useState(false);

  return (
    <React.Fragment>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
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
          {row.username}
        </TableCell>
        <TableCell align="right">{row.email}</TableCell>
        <TableCell align="right">{row.token}</TableCell>
        <TableCell align="right">{row.num_request}</TableCell>
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
    </React.Fragment>
  )
}

const UserTable = ({ users, props } : { users : any[], props : TablePaginationActionsProps}) => {

  return (
    <div>UserTable</div>
  )
}

export default UserTable;
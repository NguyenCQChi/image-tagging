import React, { useState } from 'react';
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
import { StatType } from '@src/types/stats.type';
import { useTheme } from '@mui/material/styles';
import dynamic from 'next/dynamic';

const createData = (row: any) : UserType => {
  const pattern = /^(GET|POST|PUT|DELETE|PATCH)\b(.*)$/;
  const userName = row.userName
  const email = row.email
  const refreshToken = row.refreshToken
  let totalRequest = 0
  let endpointInfo = []
  for(const endpoint in row.endpointInfo) {
    const match = endpoint.match(pattern)
    const method = match[1]
    const url = match[2]
    const number_request = row.endpointInfo[endpoint]
    totalRequest += number_request
    const stat_object : StatType = {
      method: method,
      endpoint: url,
      num_request: number_request
    }
    endpointInfo.push(stat_object)
  }
  return {
    userName,
    email,
    refreshToken,
    totalRequest,
    endpointInfo
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

const TablePaginationActions = (props: TablePaginationActionsProps) => {
  const theme = useTheme();
  const { count, page, rowsPerPage, onPageChange } = props;

  const handleFirstPageButtonClick = (e: React.MouseEvent<HTMLButtonElement>) => {
    onPageChange(e, 0)
  }

  const handleBackButtonClick = (e: React.MouseEvent<HTMLButtonElement>) => {
    onPageChange(e, page - 1)
  }

  const handleNextButtonClick = (e: React.MouseEvent<HTMLButtonElement>) => {
    onPageChange(e, page + 1)
  }

  const handleLastPageButtonClick = (e: React.MouseEvent<HTMLButtonElement>) => {
    onPageChange(e, Math.max(0, Math.ceil(count / rowsPerPage) - 1))
  }

  return (
<Box sx={{ flexShrink: 0, ml: 2.5 }}>
      <IconButton
        onClick={handleFirstPageButtonClick}
        disabled={page === 0}
        aria-label="first page"
      >
        {theme.direction === 'rtl' ? <LastPage /> : <FirstPage />}
      </IconButton>
      <IconButton
        onClick={handleBackButtonClick}
        disabled={page === 0}
        aria-label="previous page"
      >
        {theme.direction === 'rtl' ? <KeyboardArrowRight /> : <KeyboardArrowLeft />}
      </IconButton>
      <IconButton
        onClick={handleNextButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="next page"
      >
        {theme.direction === 'rtl' ? <KeyboardArrowLeft /> : <KeyboardArrowRight />}
      </IconButton>
      <IconButton
        onClick={handleLastPageButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="last page"
      >
        {theme.direction === 'rtl' ? <FirstPage /> : <LastPage />}
      </IconButton>
    </Box>
  )
}

const Row = ({ row } : { row: UserType}) => {
  const theme = useTheme();
  const [ open, setOpen ] = React.useState(false);

  const data = createData(row);

  return (
    <React.Fragment>
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
          {data.userName}
        </TableCell>
        <TableCell align="right">{data.email}</TableCell>
        <TableCell align="right">{data.refreshToken}</TableCell>
        <TableCell align="right">{data.totalRequest}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{paddingBottom: 0, paddingTop: 0}} colSpan={5}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{margin: 1}}>
              <Typography variant="h6" gutterBottom component="div">
                User API Consumption
              </Typography>
              <Table size="small" aria-label="purchase">
                <TableHead>
                  <TableRow sx={{background: `${theme.palette.secondary.light}`}}>
                    <TableCell>Method</TableCell>
                    <TableCell>Endpoint</TableCell>
                    <TableCell>Requests</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data.endpointInfo.map((stat, index) => (
                    <TableRow key={index}>
                      <TableCell component='th' scope='row'>
                        {stat.method}
                      </TableCell>
                      <TableCell>{stat.endpoint}</TableCell>
                      <TableCell>{stat.num_request}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  )
}

const UserTable = ({ users, props } : { users : any[], props? : TablePaginationActionsProps}) => {
  const theme = useTheme();
  const [ page, setPage ] = useState(0);
  const [ rowsPerPage, setRowsPerPage ] = useState(5);

  const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - users.length) : 0;

  const handleChangePage = (e: React.MouseEvent<HTMLButtonElement> | null, newPage: number) => {
    setPage(newPage);
  }

  const handleChangeRowsPerPage = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setRowsPerPage(parseInt(e.target.value, 10));
    setPage(0);
  }

  return (
    <TableContainer component={Paper}>
      <Table aria-label="collapsible table">
        <TableHead>
          <TableRow sx={{background: `${theme.palette.secondary.light}`}}>
            <TableCell/>
            <TableCell>User Name</TableCell>
            <TableCell align='right'>Email</TableCell>
            <TableCell align='right'>Token</TableCell>
            <TableCell align='right'>Total requests</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {(rowsPerPage > 0
            ? users.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
            : users
          ).map((user, index) => <Row key={index} row={user} />)}
          {emptyRows > 0 && (
            <TableRow style={{ height: 53 * emptyRows }}>
              <TableCell colSpan={6} />
            </TableRow>
          )}
        </TableBody>
        <TableFooter sx={{width: '100%'}}>
          <TableRow>
            <TablePagination 
              rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
              count={users.length}
              rowsPerPage={rowsPerPage}
              page={page}
              onPageChange={handleChangePage}
              onRowsPerPageChange={handleChangeRowsPerPage}
              ActionsComponent={TablePaginationActions}
            />
          </TableRow>
        </TableFooter>
      </Table>
    </TableContainer>
  )
}

export default UserTable;
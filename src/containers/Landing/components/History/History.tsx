import { history } from '@constants/hardcoded_string'
import React, { useState, useEffect } from 'react';
import { 
  Paper,
  Table, 
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TablePagination,
  TableRow,
  Box
} from '@mui/material';
import { EntryType } from '@src/types/entry.type';
import { useTheme } from '@mui/material/styles';
import { api } from '@src/utils/api';
import { API_IMAGE_SERVER, API_GET_ENTRIES } from '@constants/strings'


interface Column {
  id: 'UserEntryNumber' | 'ImageAddress' | 'Caption' | 'EntryTimeStamp'
  label: string;
  minWidth?: number;
  align?: 'right';
  format?: (value: number) => string;
}

const columns: readonly Column[] = [
  { id: 'UserEntryNumber', label: 'Entry Number', minWidth: 90 },
  { id: 'ImageAddress', label: 'Image URL', minWidth: 100 },
  {
    id: 'Caption',
    label: 'Caption',
    minWidth: 170,
    align: 'right',
    format: (value: number) => value.toLocaleString('en-US'),
  },
  {
    id: 'EntryTimeStamp',
    label: 'Time Stamp',
    minWidth: 170,
    align: 'right',
    format: (value: number) => value.toLocaleString('en-US'),
  }
];

const createData = (entry) : EntryType => {
  const id = entry.UserEntryNumber;
  const image_address = entry.ImageAddress
  const caption = entry.Caption;
  const time = entry.EntryTimeStamp;
  return {
    id,
    image_address,
    caption,
    time
  }
}


const History = () => {
  const theme = useTheme();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [ entries, setEntries ] = useState(history.Entries)

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  useEffect(() => {
    const url = `${API_IMAGE_SERVER}${API_GET_ENTRIES}`
    
  }, [])

  return (
    <Box sx={{padding: '60px 100px'}}>
      <Paper sx={{ width: '100%', overflow: 'hidden'}}>
        <TableContainer sx={{ maxHeight: 800 }}>
          <Table stickyHeader aria-label="sticky table">
            <TableHead>
              <TableRow>
                {columns.map((column) => (
                  <TableCell
                    key={column.id}
                    align={column.align}
                    style={{ minWidth: column.minWidth, background: `${theme.palette.secondary.light}` }}
                  >
                    {column.label}
                  </TableCell>
                ))}
              </TableRow>
            </TableHead>
            <TableBody>
              {entries
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map((row, index) => {
                  return (
                    <TableRow hover role="checkbox" tabIndex={-1} key={index}>
                      {columns.map((column, id) => {
                      const value = row[column.id];
                        return (
                          <TableCell key={column.id} align={column.align}>
                            {column.id != 'UserEntryNumber' ? (
                              column.format && typeof value === 'number'
                              ? column.format(value)
                              : value
                            ) : (index)}
                          </TableCell>
                        );
                      })}
                    </TableRow>
                  );
                })}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[10, 25, 100]}
          component="div"
          count={entries.length}
          rowsPerPage={rowsPerPage}
          page={page}
          onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
          />
      </Paper>
    </Box>
  );
}

export default History;

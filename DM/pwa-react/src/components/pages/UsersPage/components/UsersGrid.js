import * as React from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

const columns: GridColDef[] = [
  { field: 'name', headerName: 'name', width: 200 },
  { field: 'login', headerName: 'login', width: 200 },
  { field: 'email', headerName: 'email', width: 200 },
];

export default function UsersGrid(props) {
  return (
    <div style={{ height: 650, width: '80%', marginTop: 20 }}>
      <DataGrid
        rows={props.users}
        columns={columns}
        pageSize={10}
        rowsPerPageOptions={[10]}
        checkboxSelection
      />
    </div>
  );
}

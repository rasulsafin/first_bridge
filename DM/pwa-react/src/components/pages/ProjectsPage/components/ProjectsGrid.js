import * as React from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';

const columns: GridColDef[] = [
  { field: 'title', headerName: 'Title', width: 200 },
  { field: 'email', headerName: 'Description', width: 200 },
];

export default function ProjectsGrid(props) {
  console.log(props.projects);
  return (
    <div style={{ height: 650, width: '80%', marginTop: 20 }}>
      <DataGrid
        rows={props.projects}
        columns={columns}
        pageSize={10}
        rowsPerPageOptions={[10]}
      />
    </div>
  );
}

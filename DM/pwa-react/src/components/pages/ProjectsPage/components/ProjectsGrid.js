import * as React from "react";
import { DataGrid, GridColDef, GridColumnHeaderParams, GridEvents } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { fetchProjects, selectAllProjects } from "../../../../services/projectsSlice";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { GridEventListener } from "@mui/x-data-grid";

const columns: GridColDef[] = [
  {
    field: "title",
    width: 200,
    renderHeader: (params: GridColumnHeaderParams) => (
      <strong>
        <h4>
        {"Title"}
      </h4>
      </strong>
    )
  },
  {
    field: "email",
    width: 200,
    renderHeader: (params: GridColumnHeaderParams) => (
      <strong>
        <h4>
        {"Description"}
      </h4>
      </strong>
    )
  }
];

export default function ProjectsGrid() {
  const dispatch = useDispatch();
  const projects = useSelector(selectAllProjects);
  const navigate = useNavigate();
  
  
  const [shawDrawer, setShawDrawer] = useState(false);

  
  useEffect(() => {
    dispatch(fetchProjects());
  }, []);

  const handleRowDoubleClick: GridEventListener<GridEvents.rowClick> = ({ row }) => {
    navigate(`/project/${row.id}`);
  };

  function handleRowClick() {
    setShawDrawer(true);
  }

  return (
    <div style={{
      height: 650,
      width: "100%",
      marginTop: 20
    }}>
      <DataGrid
        rows={projects}
        columns={columns}
        pageSize={10}
        rowsPerPageOptions={[10]}
        onRowClick={handleRowClick}
        onRowDoubleClick={handleRowDoubleClick}
        sx={{
          "& .MuiDataGrid-row:hover": {
            color: "green"
          },
          border: 2,
          boxShadow: 2,
          background: "white",
          fontSize: 18
        }}
      />
    </div>
  );
}

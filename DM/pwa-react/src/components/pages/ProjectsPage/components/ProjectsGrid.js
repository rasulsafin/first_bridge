import * as React from "react";
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { fetchProjects, selectAllProjects } from "../../../../services/projectsSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import SuccessSnackbar from "../../../snackbar/SuccessSnackbar";

const columns = [
  {
    field: "title",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>{"Title"}</h4>
      </strong>
    )
  },
  {
    field: "email",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>{"Description"}</h4>
      </strong>
    )
  }
];

export default function ProjectsGrid() {
  const dispatch = useDispatch();
  const projects = useSelector(selectAllProjects);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  const handleRowDoubleClick = ({ row }) => {
    navigate(`/project/${row.id}`);
  };

  function handleRowClick() {
  }

  return (
    <div style={{
      height: 550,
      width: "100%",
      marginTop: 20
    }}>
      <SuccessSnackbar />
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
          ".MuiDataGrid-columnSeparator": {
            display: "none"
          },
          "& .MuiDataGrid-columnHeaders": {
            fontSize: 18
          },
          border: 0,
          boxShadow: 0,
          background: "white",
          fontSize: 16,
          cursor: "pointer"
        }}
      />
    </div>
  );
}

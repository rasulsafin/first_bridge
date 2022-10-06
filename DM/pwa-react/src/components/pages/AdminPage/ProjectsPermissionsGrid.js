import * as React from "react";
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { fetchProjects, selectAllProjects } from "../../../services/projectsSlice";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { Checkbox } from "@mui/material";
import { addNewPermission } from "../../../services/permissionsSlice";

export default function ProjectsPermissionsGrid(props) {
  const dispatch = useDispatch();
  const projects = useSelector(selectAllProjects);
  const navigate = useNavigate();
  const [readPermission, setReadPermission] = useState(false);
  const [createPermission, setCreatePermission] = useState(false);
  const [updatePermission, setUpdatePermission] = useState(false);
  const [deletePermission, setDeletePermission] = useState(false);
  const [projectId, setProjectId] = useState();

  const columns = [
    {
      field: "title",
      headerName: "Title",
      width: 250,
    },
    {
      field: "create",
      headerName: "Create",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={createPermission}
          // onChange={handleCreateChange}
        />
      )
    },
    {
      field: "read",
      headerName: "Read",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={readPermission}
          // onChange={handleReadChange}
        />
      )
    },
    {
      field: "update",
      headerName: "Update",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={updatePermission}
          // onChange={handleUpdateChange}
        />
      )
    },
    {
      field: "delete",
      headerName: "Delete",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={deletePermission}
          // onChange={event => setDeletePermission(event.target.checked)}
        />
      )
    }
  ];

  // const handleDeleteChange = (event: React.ChangeEvent<HTMLInputElement>) => {
  //   // setDeletePermission(event.target.checked);
  //   // const checked = !this.state.value;
  //   setDeletePermission(event.target.checked);
  // };
  //
  // const handleReadChange = (event) => {
  //   setReadPermission(event.target.checked);
  // };
  //
  // const handleCreateChange = (event) => {
  //   setCreatePermission(event.target.checked);
  // };
  //
  // const handleUpdateChange = (event) => {
  //   setUpdatePermission(event.target.checked);
  // };

  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  const handleRowDoubleClick = ({ row }) => {
    navigate(`/project/${row.id}`);
  };

  function handleRowClick({ row }) {
    setProjectId(row.id);
  }

  function handleSavePermission() {
    dispatch(addNewPermission({
      userId: props.userId,
      objectId: projectId,
      type: props.type,
      create: createPermission,
      read: readPermission,
      update: updatePermission,
      delete: deletePermission
    }));
  }

  return (
    <div style={{
      height: 450,
      width: "100%",
      marginTop: 20
    }}>
      <DataGrid
        rows={projects}
        columns={columns}
        pageSize={5}
        rowsPerPageOptions={[5]}
        onRowClick={handleRowClick}
        onRowDoubleClick={handleRowDoubleClick}
        checkboxSelection
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
          boxShadow: 2,
          background: "white",
          fontSize: 16,
          cursor: "pointer"
        }}
      />
      <div>
        <button
          onClick={handleSavePermission}
        >Save Permission
        </button>
      </div>
    </div>
  );
}

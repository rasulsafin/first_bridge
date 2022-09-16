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
      width: 250,
      renderHeader: () => (
        <strong>
          <h4>{"Title"}</h4>
        </strong>
      )
    },
    {
      field: "email",
      width: 250,
      renderHeader: () => (
        <strong>
          <h4>{"Description"}</h4>
        </strong>
      )
    },
    {
      field: "create",
      headerName: "create",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={createPermission}
          onChange={handleCreateChange}
        />
      )
    },
    {
      field: "read",
      headerName: "read",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={readPermission}
          onChange={handleReadChange}
        />
      )
    },
    {
      field: "update",
      headerName: "update",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={updatePermission}
          onChange={handleUpdateChange}
        />
      )
    },
    {
      field: "delete",
      headerName: "delete",
      width: 100,
      renderCell: () => (
        <Checkbox
          checked={deletePermission}
          onChange={handleDeleteChange}
        />
      )
    }
  ];

  const handleDeleteChange = (event) => {
    setDeletePermission(event.target.checked);
  };

  const handleReadChange = (event) => {
    setReadPermission(event.target.checked);
  };

  const handleCreateChange = (event) => {
    setCreatePermission(event.target.checked);
  };

  const handleUpdateChange = (event) => {
    setUpdatePermission(event.target.checked);
  };

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

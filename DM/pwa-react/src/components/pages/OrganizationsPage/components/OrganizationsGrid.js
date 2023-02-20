import * as React from "react";
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { fetchOrganizations, selectAllOrganizations } from "../../../../services/organizationsSlice";

const columns = [
  {
    field: "name",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>{"Title"}</h4>
      </strong>
    )
  },
  {
    field: "address",
    width: 250,
    renderHeader: () => (
      <strong>
        <h4>{"Address"}</h4>
      </strong>
    )
  },
  {
    field: "inn",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>{"Inn"}</h4>
      </strong>
    )
  },
  {
    field: "phone",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>{"Phone"}</h4>
      </strong>
    )
  },
  {
    field: "email",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>{"Email"}</h4>
      </strong>
    )
  }
];

export default function OrganizationsGrid() {
  const dispatch = useDispatch();
  const projects = useSelector(selectAllOrganizations);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchOrganizations());
  }, [dispatch]);

  const handleRowDoubleClick = ({ row }) => {
    navigate(`/organization/${row.id}`);
  };

  function handleRowClick() {
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
          ".MuiDataGrid-columnSeparator": {
            display: "none"
          },
          "& .MuiDataGrid-columnHeaders": {
            fontSize: 20
          },
          border: 0,
          background: "white",
          fontSize: 16,
          cursor: "pointer"
        }}
      />
    </div>
  );
}

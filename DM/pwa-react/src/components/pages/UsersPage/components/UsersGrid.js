import { DataGrid, GridColDef, GridEventListener, GridEvents } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { fetchUsers, selectAllUsers } from "../../../../services/usersSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { GridColumnHeaderParams } from "@mui/x-data-grid";

const columns: GridColDef[] = [
  {
    field: "name",
    width: 200,
    renderHeader: (params: GridColumnHeaderParams) => (
      <strong>
        <h4>
        {"Name"}
      </h4>
      </strong>
    )
  },
  {
    field: "login",
    width: 200,
    renderHeader: (params: GridColumnHeaderParams) => (
      <strong>
        <h4>
        {"Login"}
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
        {"Email"}
      </h4>
      </strong>
    )
  }
];

export default function UsersGrid() {
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchUsers());
  }, []);

  const handleRowDoubleClick: GridEventListener<GridEvents.rowClick> = ({ row }) => {
    navigate(`/user/${row.id}`);
  };

  return (
    <div style={{
      height: 650,
      width: "100%",
      marginTop: 20
    }}>
      <DataGrid
        rows={users}
        columns={columns}
        pageSize={10}
        rowsPerPageOptions={[10]}
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

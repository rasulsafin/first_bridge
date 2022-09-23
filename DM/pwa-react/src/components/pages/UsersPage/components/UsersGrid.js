import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { fetchUsers, selectAllUsers } from "../../../../services/usersSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router";

const columns = [
  {
    field: "name",
    width: 200,
    renderHeader: () => (
      <strong>{"Name"}</strong>
    )},
  {
    field: "login",
    width: 200,
    renderHeader: () => (
      <strong>{"Login"}</strong>
    )},
  {
    field: "email",
    width: 200,
    renderHeader: () => (
      <strong>{"Email"}</strong>
    )}
];

export default function UsersGrid() {
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchUsers());
  }, [dispatch]);

  const handleRowDoubleClick = ({ row }) => {
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
          '.MuiDataGrid-columnSeparator': {
            display: 'none',
          },
          "& .MuiDataGrid-columnHeaders": {
            fontSize: 20,
          },
          border: 0,
          boxShadow: 2,
          background: "white",
          fontSize: 16,
          cursor: 'pointer',
        }}
      />
    </div>
  );
}

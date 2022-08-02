import { DataGrid, GridColDef, GridColumnHeaderParams, GridEventListener, GridEvents } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { fetchRecords, selectAllRecords } from "../../../../services/recordsSlice";

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
  }
];

export default function RecordsGrid() {
  const dispatch = useDispatch();
  const records = useSelector(selectAllRecords);
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchRecords());
  }, [dispatch]);

  const handleRowDoubleClick: GridEventListener<GridEvents.rowClick> = ({ row }) => {
    navigate(`/record/${row.id}`);
  };

  return (
    <div style={{
      height: 650,
      width: "100%",
      marginTop: 20
    }}>
      <DataGrid
        rows={records}
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
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { fetchRecords, selectAllRecords } from "../../../../services/recordsSlice";

const columns = [
  {
    field: "name",
    width: 200,
    renderHeader: () => (
      <strong>
        <h4>
          {"Name"}
        </h4>
      </strong>
    )
  }
];

export default function RecordsGrid(props) {
  const dispatch = useDispatch();
  const records = useSelector(selectAllRecords);
  const navigate = useNavigate();
  const projectId = props.projectId;
  const recordsOfProject = [];
  let index,
    len;
  
  if (props.projectId !== undefined)
  {
    for (index = 0, len = records.length; index < len; ++index) {
      if (records[index].projectId === Number(projectId)) {
        recordsOfProject.push(records[index]);
      }
    }
  }
 
  useEffect(() => {
    dispatch(fetchRecords());
  }, [dispatch]);  
 
  const handleRowDoubleClick = ({ row }) => {
    navigate(`/record/${row.id}`);
  };

  return (
    <div style={{
      height: 450,
      width: "100%",
      marginTop: 20
    }}>
      <DataGrid
        rows={recordsOfProject}
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
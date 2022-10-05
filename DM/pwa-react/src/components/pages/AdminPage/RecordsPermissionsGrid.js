import * as React from "react";
import { DataGrid } from "@mui/x-data-grid";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { Checkbox } from "@mui/material";
import { fetchRecords, selectAllRecords } from "../../../services/recordsSlice";

export default function RecordsPermissionsGrid() {
  const dispatch = useDispatch();
  const records = useSelector(selectAllRecords);
  const navigate = useNavigate();
  const [del, setDel] = useState(false);

  const columns = [
    {
      field: "title",
      width: 250,
      renderHeader: () => (
        <strong>
          <h4>{"Title"}</h4>
        </strong>
      )},
    {
      field: "email",
      width: 250,
      renderHeader: () => (
        <strong>
          <h4>{"Description"}</h4>
        </strong>
      )},
    {
      field: "create",
      headerName: "create",
      width: 100,
      renderCell: (params) => (
        <Checkbox
          checked={params.rows?.confirmed}
          onChange={() => handleConfirmChange(params.row)}
        />
      )
    },
    {
      field: "read",
      headerName: "read",
      width: 100,
      renderCell: (params) => (
        <Checkbox
          checked={params.rows?.confirmed}
          onChange={() => handleConfirmChange(params.row)}
        />
      )
    },
    {
      field: "update",
      headerName: "update",
      width: 100,
      renderCell: (params) => (
        <Checkbox
          checked={params.rows?.confirmed}
          onChange={() => handleConfirmChange(params.row)}
        />
      )
    },
    {
      field: "delete",
      headerName: "delete",
      width: 100,
      renderCell: (params) => (
        <Checkbox
          checked={params.rows?.confirmed}
          onChange={() => handleConfirmChange(params.row)}
        />
      )
    }
  ];

  function handleConfirmChange(clickedRow) {
    console.log(clickedRow);
    console.log(clickedRow.confirmed);
    
    console.log(clickedRow.id);
      
    records.map((x) => {
      if (x.id === clickedRow.id) {
        return {
          confirmed: !clickedRow.confirmed,
        };
      }
      return x;
    });

    const varDel = !clickedRow.confirmed;
    console.log(varDel);
  }

  useEffect(() => {
    dispatch(fetchRecords());
  }, [dispatch]);

  const handleRowDoubleClick = ({ row }) => {
    navigate(`/record/${row.id}`);
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
        rows={records}
        columns={columns}
        pageSize={10}
        rowsPerPageOptions={[10]}
        onRowClick={handleRowClick}
        onRowDoubleClick={handleRowDoubleClick}
        sx={{
          "& .MuiDataGrid-row:hover": {
            color: "green"
          },
          '.MuiDataGrid-columnSeparator': {
            display: 'none',
          },
          "& .MuiDataGrid-columnHeaders": {
            backgroundColor: "rgb(29,97,172)",
            color: "rgb(253,253,253)",
            fontSize: 20,
          },
          border: 2,
          boxShadow: 2,
          background: "white",
          fontSize: 16,
          cursor: 'pointer',
        }}
      />
    </div>
  );
}

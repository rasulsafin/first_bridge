import { useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { selectAllRecords } from "../../../../services/recordsSlice";
import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";

export const RecordDetailPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const records = useSelector(selectAllRecords);

  const goBack = () => {
    navigate(-1);
  };

  const record = records.find(record => record.id === Number(id));
  
  return (
    <div className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
      </Toolbar>
      <hr />
      <h1>Record Detail Page</h1>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {record.name}</span></p>
      </div>
      <h3>Fields:</h3>
      <p>
        {/*{fields}*/}
      </p>
    </div>
  );
};
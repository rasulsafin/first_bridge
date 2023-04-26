import { useNavigate, useParams } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { editRecord, selectAllRecords } from "../../../../services/recordsSlice";
import { Button, Toolbar } from "@mui/material";
import * as React from "react";
import { Controls } from "../../../controls/Controls";
import { useState } from "react";

export const RecordEditPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();
  const records = useSelector(selectAllRecords);

  const goBack = () => {
    navigate(-1);
  };

  const record = records.find(record => record.id === Number(id));
  const fieldsObj = record.fields;
  const [values, setValues] = useState(fieldsObj);

  const fields = Object.entries(values);

  console.log(record)
  console.log("values", values);
  // const initialValues = fields.map(([key, value]) => [{key, value}])

  const onSubmit = () => {
    dispatch(editRecord({
      id: record.id,
      name: record.name,
      projectId: record.projectId,
      fields: values,
      comments: null
    }));
    navigate(`/project/${record.projectId}`);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value
    });
  };
  
  return (
    <div>
      <Toolbar>
        <Controls.Button onClick={goBack}>
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Record Edit Page</h3>
      <div>
        <Controls.Input
          label="Name"
          type="text"
          value={record.name}
        />
        <div>
          {fields.map(([key, value]) => {
            return (
              <div>
                <Controls.Input
                  name={key}
                  label={key}
                  type="text"
                  value={value}
                  onChange={handleInputChange}
                />
              </div>
            );
          })}
          <Button
            className="m-2"
            onClick={onSubmit}
            variant={"outlined"}>
            Submit
          </Button>
        </div>
      </div>
    </div>
  );
};
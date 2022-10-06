import { useNavigate, useParams } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { addNewRecord, editRecord, selectAllRecords } from "../../../../services/recordsSlice";
import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";
import { Controls } from "../../../controls/Controls";

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

  const fields = Object.entries(fieldsObj);

  const onSubmit = (data) => {
    dispatch(editRecord({
      // name: re.name,
      fields: data
    }));
    navigate(`/record/${id}`);
  };

  return (
    <div className="p-3">
      <Toolbar>
        <Button className="ml-o m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
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
                  label={key}
                  type="text"
                  value={value}
                />
              </div>
            );
          })}
          <Button
            className="m-2"
            // onClick={handleSubmit(onSubmit)}
            variant={"outlined"}>
            Submit
          </Button>
        </div>
      </div>
    </div>
  );
};
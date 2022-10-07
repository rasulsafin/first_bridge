import { Controls } from "../../../controls/Controls";
import { Button, Toolbar } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as React from "react";
import { fetchRecordTemplates, selectAllRecordTemplates } from "../../../../services/recordTemplatesSlice";
import { useForm } from "react-hook-form";
import { FormInputText } from "../../../controls/FormInputText";
import { addNewRecord } from "../../../../services/recordsSlice";
import { BiArrowBack } from "react-icons/bi";
import { useLocation, useNavigate } from "react-router";
import { maxNumberOfFields } from "../../../../constants/recordFields";
import { openSnackbar } from "../../../../services/snackbarSlice";

export const RecordCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const projectId = location.state.id;

  const [template, setTemplate] = useState();
  const templates = useSelector(selectAllRecordTemplates);
  const templateForm = templates.find(t => t.id === Number(template));
  let arrayFields = [];

  const goBack = () => {
    navigate(-1);
  };

  const methods = useForm();
  const { handleSubmit, reset, control, setValue, watch } = methods;
  const onSubmit = (data) => {
    dispatch(addNewRecord({
      name: templateForm.name,
      projectId: projectId,
      fields: data
    }));
    dispatch(openSnackbar());
    navigate(`/project/${projectId}`)
  };

  useEffect(() => {
    dispatch(fetchRecordTemplates(projectId));
  }, [dispatch]);

  if (templateForm !== undefined) {
    const fieldsObj = templateForm.recordTemplate;

    for (let i = 0; i < maxNumberOfFields; i++) {
      if (fieldsObj[i] !== undefined) {
        arrayFields.push(fieldsObj[i]);
      }
    }
  }

  const inputFields = arrayFields.map(item =>
    <FormInputText
      name={item.title}
      control={control}
      label={item.title}
      key={item.index}
      type={item.type}
      onChange={(event) => console.log(event.target.value)}
    />);

  return (
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Create record</h3>
      <Controls.SelectTemplate
        name="template"
        label="template"
        value={template}
        onChange={(event) => setTemplate(event.target.value)}
        options={templates}
        autoWidth={false}
        style={{
          margin: "10px"
        }}
      />
      <div>
      </div>
      <div style={{
        display: "flex",
        flexDirection: "column",
        width: "300px"
      }}>
        {inputFields}
        <Button
          className="m-2"
          onClick={handleSubmit(onSubmit)}
          variant={"outlined"}>
          Submit
        </Button>
        <Button
          className="m-2"
          onClick={() => reset()}
          variant={"outlined"}>
          Reset
        </Button>
      </div>
    </div>
  );
};
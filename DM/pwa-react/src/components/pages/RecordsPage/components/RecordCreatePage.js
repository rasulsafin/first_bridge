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
import { useNavigate } from "react-router";

export const RecordCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [template, setTemplate] = useState();
  const projectId = localStorage.getItem("projectId");
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
  };

  useEffect(() => {
    dispatch(fetchRecordTemplates(projectId));
  }, [dispatch]);

  if (templateForm !== undefined) {
    const fieldsObj = templateForm.recordTemplate;

    for (let i = 0; i < 20; i++) {
      if (fieldsObj[i] !== undefined) {
        arrayFields.push(fieldsObj[i]);
      }
    }
  }

  const inputFields = arrayFields.map(item =>
    <FormInputText
      name={item.title.toString()}
      control={control}
      label={item.title.toString()}
      key={item.index}
      type={item.type.toString()}
      onChange={(event) => console.log(event.target.value)}
    />);

  return (
    <div className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
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
        <Button className="m-2" onClick={handleSubmit(onSubmit)} variant={"outlined"}>
          {" "}
          Submit{" "}
        </Button>
        <Button className="m-2" onClick={() => reset()} variant={"outlined"}>
          {" "}
          Reset{" "}
        </Button>
      </div>
    </div>
  );
};
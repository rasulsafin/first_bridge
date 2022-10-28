import { Controls } from "../../../controls/Controls";
import { Button, MenuItem, Select, Toolbar } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as React from "react";
import { fetchRecordTemplates, selectAllRecordTemplates } from "../../../../services/recordTemplatesSlice";
import { useForm } from "react-hook-form";
import { addNewRecord } from "../../../../services/recordsSlice";
import { BiArrowBack } from "react-icons/bi";
import { useLocation, useNavigate } from "react-router";
import { maxNumberOfFields } from "../../../../constants/recordFields";
import { openSnackbar } from "../../../../services/snackbarSlice";
import { RecordCreateForm } from "./recordCreateForm/RecordCreateForm";

export const RecordCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const projectId = location.state.id;

  const [template, setTemplate] = useState();
  const templates = useSelector(selectAllRecordTemplates);
  const templateForm = templates.find(t => t.id === Number(template));
  let arrayFields = [];
  let enumFields = [];

  const goBack = () => {
    navigate(-1);
  };

  // const methods = useForm();
  // const { handleSubmit, reset, control, setValue, watch } = methods;
  //
  // const onSubmit = (data) => {
  //   dispatch(addNewRecord({
  //     name: templateForm.name,
  //     projectId: projectId,
  //     fields: data
  //   }));
  //   dispatch(openSnackbar());
  //   navigate(`/project/${projectId}`)
  // };

  useEffect(() => {
    dispatch(fetchRecordTemplates(projectId));
  }, [dispatch]);

  
  if (templateForm !== undefined) {
    const fieldsObj = templateForm.recordTemplate;

    for (let i = 0; i < maxNumberOfFields; i++) {
      if (fieldsObj[i] !== undefined) {
        if (fieldsObj[i].type === "enum") {
          enumFields.push(fieldsObj[i]);
        }
        else {
          arrayFields.push(fieldsObj[i])
        }
      }
    }
  }

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
      <RecordCreateForm
        arrayInput={arrayFields}
        arrayDropdown={enumFields}
        templateForm={templateForm}
        projectId={projectId}
      />
    </div>
  );
};
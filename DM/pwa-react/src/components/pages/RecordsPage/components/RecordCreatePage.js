import { Controls } from "../../../controls/Controls";
import { Button, MenuItem, Select, Toolbar, Modal, Typography, Box } from "@mui/material";
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
import ViewerIfc from "../../FilesPage/ViewerIfc";
import store from "../../../../redux/store";
import { selectIfcElementProps } from "../../../../services/ifcElementPropsSlice";
import { RecordInputTextForm } from "./recordCreateForm/RecordInputTextForm";
import { RecordInputDropdownForm } from "./recordCreateForm/RecordInputDropdownForm";
import IfcComponent from "../../../ifc/IfcComponent";

export const RecordCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const location = useLocation();
  const projectId = location.state.id;
  const ifcElementProps = useSelector(selectIfcElementProps);
  const [template, setTemplate] = useState("");
  const [elementProps, setElementProps] = useState({});
  const templates = useSelector(selectAllRecordTemplates);
  const templateForm = templates.find(t => t.id === Number(template));
  let arrayFields = [];
  let enumFields = [];
  let arrFields = [];
  const [open, setOpen] = useState(false);

  const goBack = () => {
    navigate(-1);
  };
  
  useEffect(() => {
  }, [template]);
  
  console.log("render", templateForm)
  
  const handleClose = () => setOpen(false);
  const handleOpen = () => setOpen(true);

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

  if (templateForm) {
    
    
    
    const fieldsObj = templateForm.recordTemplate;
    const arr = Object.entries(fieldsObj)
    console.log(arr)

    // for (let i = 0; i < maxNumberOfFields; i++) {
    //   if (fieldsObj[i] !== undefined) {
    //     arrFields.push(fieldsObj[i]);
    //   }
    // }
    // console.log("arrFields", arrFields)
    
    for (let i = 0; i < maxNumberOfFields; i++) {
      if (fieldsObj[i] !== undefined) {
        if (fieldsObj[i].type === "enum") {
          enumFields.push(fieldsObj[i]);
        } else {
          arrayFields.push(fieldsObj[i]);
        }
      }
    }
  }

  // const testForm = arrFields.map((item, index) => {
  //     switch (item.type) {
  //       case "number":
  //         return <Controls.Input
  //           key={index}
  //           name={item.title}
  //           label={item.title}
  //           type={item.type}
  //           required={item.required} />;
  //       case "text":
  //         return <Controls.Input
  //           key={index}
  //           name={item.title}
  //           label={item.title}
  //           type={item.type}
  //           required={item.required} />;
  //       case "enum":
  //         return <Controls.Select
  //           key={index}
  //           name={item.title}
  //           label={item.title}
  //           options={item.options} />;
  //     }
  //   }
  // );
  
  
  const createIfcElementInput = () => {
    if(ifcElementProps.expressId) {
      setElementProps(ifcElementProps)
    }
    
    handleClose();
  }

  return (
    <div className="component-container">
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
      </Toolbar>
      <hr />
      <div
        style={{
          display: "flex",
          flexWrap: "wrap",
          alignItems: "flex-start",
          justifyContent: "flex-start"
        }}
      >
        <div
          className="col-4"
        >
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
        <div
          className="col-4 mt-4"
        >
          <div>
            <Controls.Button
              onClick={handleOpen}>
              Select element
            </Controls.Button>
          </div>
        </div>
      </div>
      {/*{testForm}*/}
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box style={{
          position: "absolute",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
          width: "70%",
          height: "70%",
          backgroundColor: "#FFF",
          border: "1px solid #000",
          boxShadow: 24,
          padding: "20px"
        }}>
          <ViewerIfc />
          <Controls.Button
            onClick={createIfcElementInput}
          >Choose</Controls.Button>
        </Box>
      </Modal>
    </div>
  );
};
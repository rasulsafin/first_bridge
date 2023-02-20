import { FormProvider, useForm } from "react-hook-form";
import { RecordInputCheckboxForm } from "./RecordInputCheckboxForm";
import { RecordInputDropdownForm } from "./RecordInputDropdownForm";
import { RecordInputTextForm } from "./RecordInputTextForm";
import { Controls } from "../../../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import { addNewRecord } from "../../../../../services/recordsSlice";
import { openSnackbar } from "../../../../../services/snackbarSlice";
import { useNavigate } from "react-router";
import { Button } from "@mui/material";
import * as React from "react";
import { useState } from "react";
import { selectIfcElementProps } from "../../../../../services/ifcElementPropsSlice";

export const RecordCreateForm = (props) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const ifcElementProps = useSelector(selectIfcElementProps);

  const { arrayInput, arrayDropdown, projectId, templateForm, ...other } = props;
  const methods = useForm();
  const { handleSubmit } = methods;
  
  const onSubmit = (data) => {

    if (ifcElementProps.name) {
      data.element = {
        expressId: ifcElementProps.expressId,
        fileName: ifcElementProps.fileName,
        guid: ifcElementProps.guid ? ifcElementProps : null,
        name: ifcElementProps.name
      };
    }
    
    console.log("data", data)

    const arrayData = Object.entries(data).map(([key, value]) => [key, value]);
    
    // const initialValues = arrayData.map(([key, value]) => key, value)

    // console.log("arrayMod", initialValues, initialValues[0])
    console.log("arrayKey", arrayData[0])
    
    dispatch(addNewRecord({
      name: templateForm.name,
      projectId: projectId,
      fields: data
    }));
    dispatch(openSnackbar());
    navigate(`/project/${projectId}`);
  };

  const arrayInputForm = arrayInput.map((item, index) =>
    <RecordInputTextForm
      key={index}
      name={item.title}
      label={item.title}
      type={item.type}
      required={item.required}
      defaultValue=""
    />
  );

  console.log(arrayDropdown, arrayInput)
  
  const arrayDropdownForm = arrayDropdown.map((item, index) =>
    <RecordInputDropdownForm
      key={index}
      name={item.title}
      label={item.title}
      options={item.options}
      defaultValue=""
    />
  );


  console.log("ifcElementProps", ifcElementProps)  
  
  const handleToIfcViewer = () => {
    navigate(`/ifcViewer`);
  };
  
  // const ifcElementInput = (ifcElementProps) => {
  //   setElementList([...elementList, {
  //     expressId: expressId,
  //     guid: guid,
  //     name: required,
  //     fileName: fileName
  //   }]);
  // }
  
  return (
    <>
      <div
        style={{
          display: "grid",
          width: "40vw"
        }}>
        <FormProvider {...methods}>
          {arrayInputForm}
          {arrayDropdownForm}
        </FormProvider>
        {ifcElementProps.name
          ? <a href="/ifcViewer"><Controls.Button
          key={ifcElementProps.expressId}
          // onClick={handleToIfcViewer}
          >{ifcElementProps.name}</Controls.Button></a>
          : null}
        <Controls.Button
          sx={{ width: 200 }}
          onClick={handleSubmit(onSubmit)}
          variant="contained">
          Save Record
        </Controls.Button>
      </div>
    </>
  );
};
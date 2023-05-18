import React from "react";
import TextField from "@mui/material/TextField";
import { useField } from "formik";

const ValidationFormTextfield = ({ name, ...otherProps }) => {
  const [field, meta] = useField(name);

  const configTextfield = {
    ...field,
    ...otherProps,
    variant: "outlined",
    padding: "normal",
    
    InputProps: { 
      style: { fontSize: 16 }, 
      autoComplete: "off" },
    sx: {
      width: { md: 500 },
      "& .MuiInputBase-root": {
        height: 35,
        marginRight: 3
      },
      "& .MuiFormHelperText-root": {
        margin: 0
      },
      "&  .MuiFormHelperText-root.Mui-error": {
        color: "#C32A2A"
      }
    }
  };

  if (meta && meta.touched && meta.error) {
    configTextfield.error = true;
    configTextfield.helperText = meta.error;
  }

  return (
    <TextField
      {...configTextfield} />
  );
};

export default ValidationFormTextfield;

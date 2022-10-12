import React from "react";
import TextField from "@mui/material/TextField";
import { useField } from "formik";

const ValidationFormTextfield = ({ name, ...otherProps }) => {
  const [field, meta] = useField(name);

  const configTextfield = {
    ...field,
    ...otherProps,
    variant: "outlined",
    margin: "normal",
    InputProps: { style: { fontSize: 18 } },
    sx: {
      width: { sm: 200, md: 300 },
      "& .MuiInputBase-root": {
        height: 45,
        marginRight: 3
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

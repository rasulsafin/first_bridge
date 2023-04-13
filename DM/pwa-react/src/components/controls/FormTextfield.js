import React from "react";
import TextField from "@mui/material/TextField";

const FormTextfield = ({ ...otherProps }) => {

  const configTextfield = {
    ...otherProps,
    variant: "outlined",
    padding: "normal",
    InputProps: { style: { fontSize: 16 } },
    sx: {
      width: { md: 500 },
      "& .MuiInputBase-root": {
        height: 35,
        marginRight: 3
      }
    }
  };

  return (
    <TextField
      {...configTextfield} />
  );
};

export default FormTextfield;

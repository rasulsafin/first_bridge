import React from "react";
import TextField from "@mui/material/TextField";

export default function Input({ name, value, onChange, ...other }) {
  return (
    <TextField
      sx={{
        "& .MuiInputBase-root": {
          height: 35
        }
      }}
      variant="outlined"
      name={name}
      value={value}
      onChange={onChange}
      InputProps={{ style: { fontSize: 16 } }}
      {...other}
    />
  );
}

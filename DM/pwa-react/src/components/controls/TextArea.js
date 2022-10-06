import React from 'react'
import TextField from "@mui/material/TextField";

export default function TextArea(props) {
  const { name, label, value, onChange, ...other } = props;

  return (
    <TextField
      sx={{
        width: { sm: 200, md: 300 },
        "& .MuiInputBase-root": {
          marginRight: 3,
        }
      }}
      variant="outlined"
      label={label}
      name={name}
      value={value}
      onChange={onChange}
      autoFocus
      margin="normal"
      {...other}
    />
  )
}

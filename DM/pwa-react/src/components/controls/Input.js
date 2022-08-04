import React from 'react'
import TextField from "@mui/material/TextField";

export default function Input(props) {

  const { name, label, value, onChange, ...other } = props;
  return (
    <TextField
      variant="outlined"
      label={label}
      name={name}
      value={value}
      onChange={onChange}
      fullWidth
      autoFocus
      margin="normal"
      {...other}
    />
  )
}

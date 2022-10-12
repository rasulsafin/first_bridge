import React from 'react'
import TextField from "@mui/material/TextField";

export default function Input(props) {
  const { name, label, value, onChange, ...other } = props;
  
  return (
    <TextField
      sx={{
        width: { sm: 200, md: 300 },
        "& .MuiInputBase-root": {
          height: 45,
          marginRight: 3,
        },
      }}
      variant="outlined"
      label={label}
      name={name}
      value={value}
      onChange={onChange}
      autoFocus
      margin="normal"
      InputProps={{ style: { fontSize: 18 } }}
      {...other}
    />
  )
}

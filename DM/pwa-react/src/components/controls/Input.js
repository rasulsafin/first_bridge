import React from 'react'
import TextField from "@mui/material/TextField";

export default function Input(props) {
  const { name, label, value, onChange, ...other } = props;
  
  return (
    <TextField
      sx={{
        // width: { sm: 200, md: 300 },
        "& .MuiInputBase-root": {
          height: 35,
          // marginRight: 3,
        },
      }}
      variant="outlined"
      label={label}
      name={name}
      value={value}
      onChange={onChange}
      margin="normal"
      InputProps={{ style: { fontSize: 15 } }}
      {...other}
    />
  )
}

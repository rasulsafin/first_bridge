import React from "react";
import { FormControl, InputLabel, MenuItem, Select as MuiSelect } from "@mui/material";

export default function Select(props) {
  const { name, label, value, onChange, options, ...other } = props;

  return (
    <FormControl
      variant="outlined"
      sx={{
        width: { sm: 200, md: 300 },
        "& .MuiInputBase-root": {
          height: 60
        }
      }}>
      <InputLabel>{label}</InputLabel>
      <MuiSelect
        label={label}
        name={name}
        value={value}
        onChange={onChange}
        autoWidth={false}
        {...other}
      >
        <MenuItem value="">None</MenuItem>
        {
          options.map(
            item => (<MenuItem key={item.id} value={item.id}>{item.name}</MenuItem>)
          )
        }
      </MuiSelect>
    </FormControl>
  );
}
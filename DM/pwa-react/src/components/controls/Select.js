import React from "react";
import { FormControl, Select as MuiSelect } from "@mui/material";

export default function Select(props) {
  const { name, label, value, onChange, options, ...other } = props;

  return (
    <FormControl
      variant="outlined"
      fullWidth="true"
      sx={{
        "& .MuiInputBase-root": {
          height: 35
        }
      }}>
      <MuiSelect
        name={name}
        value={value}
        onChange={onChange}
        autoWidth={false}
        {...other}
      />
    </FormControl>
  );
}
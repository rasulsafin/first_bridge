import React from "react";
import { FormControl, Select as MuiSelect } from "@mui/material";

export default function Select(props) {
  const { name, label, value, onChange, options, ...other } = props;

  return (
    <FormControl
      variant="outlined"
      sx={{
        width: { sm: 500, md: 500 },
        "& .MuiInputBase-root": {
          height: 35,
          marginRight: 3,

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
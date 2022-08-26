import React from "react";
import { FormControl, FormControlLabel, Checkbox as MuiCheckbox } from "@mui/material";

export default function Checkbox(props) {

  const { name, label, value, onChange } = props;

  return (
    <FormControl>
      <FormControlLabel
        control={<MuiCheckbox
          name={name}
          checked={value}
          onChange={onChange}
        />}
        label={label}
      />
    </FormControl>
  );
}

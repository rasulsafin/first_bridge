import React from "react";
import { FormControl, FormControlLabel, Checkbox as MuiCheckbox } from "@mui/material";

export default function Checkbox({ name, label, value, onChange }) {
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

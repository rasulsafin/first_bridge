import React from "react";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { useFormContext, Controller } from "react-hook-form";

export const RecordInputDropdownForm = ({ name, label, options }) => {
  const { control } = useFormContext();

  const generateSingleOptions = () => {
    return options.map((option) => {
      return (
        <MenuItem
          key={option.value}
          value={option.value}
        >
          {option.value}
        </MenuItem>

      );
    });
  };

  return (
    <FormControl
      sx={{
        width: { sm: 200, md: 300 },
        "& .MuiInputBase-root": {
          height: 45
        }
      }}
      size="small"
      margin="normal"
    >
      <InputLabel>{label}</InputLabel>
      <Controller
        render={({ field }) => (
          <Select
            inputProps={{ autoFocus: true }} {...field}>
            {generateSingleOptions()}
          </Select>
        )}
        control={control}
        name={name}
      />
    </FormControl>
  );
};
import React from "react";
import { Controller, useFormContext } from "react-hook-form";
import TextField from "@mui/material/TextField";

export const RecordInputTextForm = ({ name, label, type }) => {
  const { control } = useFormContext();

  return (
    <Controller
      name={name}
      control={control}
      render={({ field: { onChange, value }, fieldState: { error }, formState }) => (
        <TextField
          sx={{
            width: { sm: 200, md: 300 },
            "& .MuiInputBase-root": {
              height: 45
            }
          }}
          helperText={error ? error.message : null}
          size="small"
          error={!!error}
          onChange={onChange}
          value={value}
          label={label}
          type={type}
          variant="outlined"
          margin="normal"
        />
      )}
    />
  );
};
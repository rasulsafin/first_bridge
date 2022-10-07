import React from "react";
import { Controller, useFormContext } from "react-hook-form";
import TextField from "@mui/material/TextField";

export const FormInputText = ({ name, control, label, type }) => {
  return (
    <Controller
      name={name}
      control={control}
      render={({
                 field: { onChange, value },
                 fieldState: { error },
                 formState,
               }) => (
        <TextField
          sx={{
            width: { sm: 200, md: 300 },
            "& .MuiInputBase-root": {
              height: 45
            }
          }}
          helperText={error ? error.message : null}
          error={!!error}
          onChange={onChange}
          value={value}
          label={label}
          type={type}
          margin="normal"
          variant="outlined"
        />
      )}
    />
  );
};

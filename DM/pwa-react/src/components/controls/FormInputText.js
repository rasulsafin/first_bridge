import React from "react";
import { Controller, useFormContext } from "react-hook-form";
import TextField from "@mui/material/TextField";


export interface FormInputProps {
  name: string;
  control: any;
  label: string;
  setValue?: any;
}


export const FormInputText = ({ name, control, label }: FormInputProps) => {
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
          margin="normal"
          variant="outlined"
        />
      )}
    />
  );
};

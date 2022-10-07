import React from "react";
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import Stack from '@mui/material/Stack';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import TextField from "@mui/material/TextField";
import { DesktopDatePicker } from "@mui/x-date-pickers";

export default function DatePicker(props) {

  const { name, label, value, onChange } = props;

  const convertToDefEventPara = (name, value) => ({
    target: {
      name, value
    }
  });

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <Stack  
        sx={{
        width: { sm: 200, md: 300 },
        "& .MuiInputBase-root": {
          height: 45,
          marginRight: 3,
        }
      }}>
        <DesktopDatePicker
          name={name}
          label={label}
          inputFormat="dd/MM/yyyy"
          value={value}
          onChange={date =>onChange(convertToDefEventPara(name,date))}
          renderInput={(params) => <TextField {...params} />}
        />
      </Stack>
    </LocalizationProvider>
  );
}

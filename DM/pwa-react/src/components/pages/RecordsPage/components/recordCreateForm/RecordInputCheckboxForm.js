import React, { useEffect, useState } from "react";
import { Checkbox, FormControl, FormControlLabel, FormHelperText, FormLabel } from "@mui/material";
import { Controller, useFormContext } from "react-hook-form";

const options = [
  {
    label: "Checkbox Option 1",
    value: "1"
  },
  {
    label: "Checkbox Option 2",
    value: "2"
  },
  {
    title: "Checkbox Option 66",
    id: "77"
  }
];

export const RecordInputCheckboxForm = ({ name, label }) => {
  const [selectedItems, setSelectedItems] = useState([]);
  const { control, setValue, formState: { errors } } = useFormContext();

  const handleSelect = (value) => {
    const isPresent = selectedItems.indexOf(value);
    if (isPresent !== -1) {
      const remaining = selectedItems.filter((item) => item !== value);
      setSelectedItems(remaining);
    } else {
      setSelectedItems((prevItems) => [...prevItems, value]);
    }
  };

  useEffect(() => {
    setValue(name, selectedItems);
  }, [selectedItems]);

  const errorMessage = errors[name] ? errors[name].message : null;

  return (
    <FormControl 
      size="small"
      variant="outlined"
    >
      <FormLabel
        component="legend"
      >
        {label}
      </FormLabel>
      <div>
        {options.map((option) => {
          return (
            <FormControlLabel
              control={
                <Controller
                  name={name}
                  render={({ field: { onChange: onCheckChange } }) => {
                    return <Checkbox checked={selectedItems.includes(option.value)}
                                     onChange={() => handleSelect(option.value)} />;
                  }}
                  control={control}
                />
              }
              label={option.label}
              key={option.value}
            />
          );
        })}
      </div>

      <FormHelperText>{errorMessage ? errorMessage : ""}</FormHelperText>
    </FormControl>
  );
};
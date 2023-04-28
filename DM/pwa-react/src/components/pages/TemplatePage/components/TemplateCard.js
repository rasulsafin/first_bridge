import React, { useState } from "react";
import {
  Checkbox,
  ListItem,
  ListItemButton,
  ListItemText
} from "@mui/material";

export const TemplateCard = (template) => {
  const [checked, setChecked] = useState([]);

  const handleToggle = (templateId) => () => {
    const currentIndex = checked.indexOf(templateId);
    const newChecked = [...checked];
    console.log(templateId)
    if (currentIndex === -1) {
      newChecked.push(templateId);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

  const handleOpenModal = () => {
    console.log("open modal")
  }

  return (
    <>
      <ListItem
        sx={{
          width: "425px",
          backgroundColor: "#FFF",
          margin: "10px",
          padding: "12px",
          borderRadius: "5px"
        }}
        dense
        key={template.template.id}
      >
        <Checkbox
          edge="start"
          onChange={handleToggle(template.template.id)}
          checked={checked.indexOf(template.template.id) !== -1}
          inputProps={{ 'aria-labelledby': template.template.id }}
          sx={{
            color: "#2D2926",
            '&.Mui-checked': {
              color: "#C32A2A",
            }
          }}
        />
        <ListItemButton
          onClick={handleOpenModal}
        >
          <ListItemText
            primary={template.template.name}
          />
          <ListItemText
            primary={template.template.createdAt}
          />
        </ListItemButton>
      </ListItem>
    </>
  );
};
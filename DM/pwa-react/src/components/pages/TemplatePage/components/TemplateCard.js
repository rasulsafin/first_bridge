import React from "react";
import {
  Checkbox,
  ListItem,
  ListItemButton,
  ListItemText
} from "@mui/material";
import { formatDate } from "../../../../utils/formatDate";

export const TemplateCard = (props) => {
  const { template, handleToggle, checked } = props;

  const handleOpenModal = () => {
    console.log("open modal");
  };

  return (
    <ListItem
      sx={{
        width: "425px",
        backgroundColor: "#FFF",
        margin: "10px",
        padding: "12px",
        borderRadius: "5px"
      }}
      dense
      key={template.id}
    >
      <Checkbox
        edge="start"
        onChange={handleToggle(template.id)}
        checked={checked.indexOf(template.id) !== -1}
        inputProps={{ "aria-labelledby": template.id }}
        sx={{
          color: "#2D2926",
          "&.Mui-checked": {
            color: "#C32A2A"
          }
        }}
      />
      <ListItemButton
        onClick={handleOpenModal}
      >
        <ListItemText
          primary={template.name}
        />
        <ListItemText
          primary={formatDate(template.createdAt)}
          primaryTypographyProps={{
            display: "flex",
            justifyContent: "end"
          }}
        />
      </ListItemButton>
    </ListItem>
  );
};
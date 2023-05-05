import React, { useState } from "react";
import {
  Avatar,
  Checkbox,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemText
} from "@mui/material";

export const RecordCard = (props) => {
  const { record } = props;

  const [checked, setChecked] = useState([]);

  const handleToggle = (recordId) => () => {
    const currentIndex = checked.indexOf(recordId);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(recordId);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

  const handleOpenModal = () => {
    console.log("open modal");
  };

  return (
    <ListItem
      sx={{
        height: "51px",
        backgroundColor: "#FFF",
        marginY: "10px",
        padding: "12px",
        borderRadius: "10px"
      }}
      dense
      key={record.id}
    >
      <Checkbox
        edge="start"
        onChange={handleToggle(record.id)}
        checked={checked.indexOf(record.id) !== -1}
        inputProps={{ "aria-labelledby": record.id }}
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
          id={record.id}
          primary="State"
        />
        <ListItemText
          id={record.id}
          primary={record.name}
        />
        <ListItemText
          id={record.id}
          primary="Description"
        />
        <ListItemText
          id={record.id}
          primary={record.createdAt}
        />
        <ListItemAvatar>
          <Avatar
          />
        </ListItemAvatar>
      </ListItemButton>
    </ListItem>
  );
};
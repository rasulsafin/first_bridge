import React, { useState } from "react";
import {
  Avatar,
  Checkbox,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemText
} from "@mui/material";

export const RecordCard = (record) => {
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
    <>
      <ListItem
        sx={{
          height: "51px",
          backgroundColor: "#FFF",
          marginY: "10px",
          padding: "12px",
          borderRadius: "10px"
        }}
        dense
        key={record.record.id}
      >
        <Checkbox
          edge="start"
          onChange={handleToggle(record.record.id)}
          checked={checked.indexOf(record.record.id) !== -1}
          inputProps={{ "aria-labelledby": record.record.id }}
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
            primary={`State`}
          />
          <ListItemText
            id={record.record.id}
            primary={record.record.name}
          />
          <ListItemText
            id={record.id}
            primary={`Description`}
          />
          <ListItemText
            id={record.id}
            primary={record.record.createdAt}
          />
          <ListItemAvatar>
            <Avatar
            />
          </ListItemAvatar>
        </ListItemButton>
      </ListItem>
    </>
  );
};
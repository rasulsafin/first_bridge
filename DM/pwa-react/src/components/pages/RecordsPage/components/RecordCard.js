import React, { useState } from "react";
import {
  Avatar,
  Checkbox,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemText
} from "@mui/material";
import { Controls } from "../../../controls/Controls";

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
    console.log("open modal")
  }
  
  return (
    <>
      <ListItem
        dense
        divider
        key={record.record.id}
        // disablePadding
      >
        <Checkbox
          edge="start"
          onChange={handleToggle(record.record.id)}
          checked={checked.indexOf(record.record.id) !== -1}
          inputProps={{ 'aria-labelledby': record.record.id }}
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
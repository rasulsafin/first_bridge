import React, { useState } from "react";
import {
  Avatar,
  Checkbox,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemIcon,
  ListItemText
} from "@mui/material";
import { formatDate } from "../../../../utils/formatDate";
import { ReactComponent as ModelIcon } from "../../../../assets/icons/models.svg";
import { statusEnum } from "../../../../constants/statusEnum";

export const RecordCard = (props) => {
  const { record } = props;
  const [checked, setChecked] = useState([]);

  const statusRecord = statusEnum.find(item => item.id === record.status);

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
          <span
            style={{
              paddingRight: "5px",
              paddingLeft: 0,
              marginLeft: 0,
              fontSize: "30px",
              color: statusRecord.color.toString()
            }}>
            &#x2022;
          </span>
        <ListItemText
          sx={{ width: "50px", flexDirection: "row" }}
          id={record.id}
          primary={statusRecord.title.toString()}
        />
        <ListItemText
          sx={{ width: "50px" }}
          id={record.id}
          primary={record.name}
        />
        <ListItemIcon>
          <ModelIcon />
        </ListItemIcon>
        <ListItemText
          id={record.id}
          primary="model title is too long"
        />
        <ListItemText
          id={record.id}
          primary={formatDate(record.createdAt)}
          primaryTypographyProps={{ align: "right", marginRight: "18px" }}
        />
        <ListItemAvatar>
          <Avatar
          />
        </ListItemAvatar>
      </ListItemButton>
    </ListItem>
  );
};
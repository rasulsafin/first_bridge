import React, { useState } from "react";
import {
  Avatar,
  Checkbox,
  Collapse,
  List,
  ListItem,
  ListItemAvatar,
  ListItemButton,
  ListItemIcon,
  ListItemText
} from "@mui/material";
import { formatDate } from "../../../../utils/formatDate";
import { ReactComponent as ModelIcon } from "../../../../assets/icons/models.svg";
import { ReactComponent as CaretIcon } from "../../../../assets/icons/caret.svg";
import { ReactComponent as CaretUpIcon } from "../../../../assets/icons/caretUp.svg";
import { statusEnum } from "../../../../constants/statusEnum";

export const RecordCard = (props) => {
  const { record, handleToggle, checked } = props;
  const [expandRecord, setExpandRecord] = useState(false);
  const isParentRecord = record.childRecords.length !== 0;
  const statusRecord = statusEnum.find(item => item.id === record.status);
  const isChild = record.parentId !== null;
  
  const handleOpenModal = () => {
    console.log("open modal");
  };

  const handleExpand = () => {
    setExpandRecord(!expandRecord);
  };

  return (
    <>
      <ListItem
        sx={{
          height: "51px",
          backgroundColor: "#FFF",
          marginY: "10px",
          padding: "12px",
          paddingLeft: isChild ? "45px" : null,
          borderRadius: "10px"
        }}
        dense
        key={record.id}
      >
        {isParentRecord ?
          <ListItemButton
            onClick={handleExpand}
            sx={{
              width: "20px",
              flexGrow: 0,
              marginRight: "10px",
              paddingRight: "20px",
              paddingLeft: "3px"
            }}
          >
            <ListItemIcon>
              {expandRecord ? <CaretUpIcon /> : <CaretIcon />}
            </ListItemIcon>
          </ListItemButton>
          : null
        }
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
            sx={{ width: "50px" }}
            id={record.id}
            primary="model title is too looooooooooooooooooong"
          />
          <ListItemText
            sx={{ width: "50px" }}
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
      {isParentRecord ?
        <Collapse in={expandRecord}>
          <List>
            {record.childRecords.map((child) =>
              <RecordCard
                key={child.id}
                record={child}
                handleToggle={handleToggle}
                checked={checked}
              />
            )}
          </List>
        </Collapse>
        : null
      }
    </>
  );
};
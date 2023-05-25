import React, { useState } from "react";
import { Checkbox, ListItem, ListItemButton, ListItemText } from "@mui/material";
import { formatDate } from "../../../../utils/formatDate";

const DocumentCard = (props) => {
  const { document } = props;
  const [checked, setChecked] = useState([]);

  const handleToggle = (documentId) => () => {
    const currentIndex = checked.indexOf(documentId);
    const newChecked = [...checked];
    console.log(documentId);
    if (currentIndex === -1) {
      newChecked.push(documentId);
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
        width: "425px",
        backgroundColor: "#FFF",
        margin: "10px",
        padding: "12px",
        borderRadius: "5px"
      }}
      dense
      key={document.id}
    >
      <Checkbox
        edge="start"
        onChange={handleToggle(document.id)}
        checked={checked.indexOf(document.id) !== -1}
        inputProps={{ "aria-labelledby": document.id }}
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
          primary={document.name}
        />
        <ListItemText
          primary={formatDate(document.createdAt)}
          primaryTypographyProps={{
            display: "flex",
            justifyContent: "end"
          }}
        />
      </ListItemButton>
    </ListItem>
  );
};

export default DocumentCard;
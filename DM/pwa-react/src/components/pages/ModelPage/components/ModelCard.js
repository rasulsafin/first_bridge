import React from "react";
import { Box, ListItemButton } from "@mui/material";

export const ModelCard = ({ file, handleToggle, checked }) => {
  
  console.log(checked)
  
  return (
    <ListItemButton
      key={file.id}
      sx={{
        margin: "4px",
        padding: 0,
        "&.Mui-selected": {
          backgroundColor: "#FFF",
          border: "1px gray solid",
          borderRadius: "5px"
        },
        "&.Mui-selected:hover": {
          backgroundColor: "#FFF"
        }
      }}
      autoFocus={false}
      onClick={() => handleToggle(file)}
      dense
      // selected={checked.indexOf(file.id) !== -1}
    >
      <Box
        key={file.id}
        sx={{
          width: "179px",
          height: "183px",
          backgroundColor: "#f4f4f4",
          display: "flex",
          alignItems: "end",
          borderRadius: "5px",
          marginLeft: 0
        }}>
        <span>{file.name}</span>
      </Box>
    </ ListItemButton>
  );
};

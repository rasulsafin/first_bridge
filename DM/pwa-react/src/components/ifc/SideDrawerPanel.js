import React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import { IconButton } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import ItemProperties from "./ItemProperties";
import { selectIfcElementProps } from "../../services/ifcElementPropsSlice";
import { toggleDrawer } from "../../services/controlUISlice";
import { ReactComponent as CancelIcon } from "../../assets/icons/cancel.svg";

function PanelTitle({ title, controlsGroup }) {
  return (
    <Box sx={{
      display: "flex",
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "space-between",
      borderRadius: "5px"
    }}
    >
      <Typography variant="h4">
        {title}
      </Typography>
      {controlsGroup}
    </Box>
  );
}

export function PropertiesPanel() {
  const ifcElement = useSelector(selectIfcElementProps);
  const dispatch = useDispatch();
  const selectedElement = true;

  return (
    <>
      <PanelTitle
        title={ifcElement.fileName}
        controlsGroup={
          <Box>
            <IconButton
              title="toggle drawer"
              onClick={() => dispatch(toggleDrawer())}
            >
              <CancelIcon />
            </IconButton>
          </Box>
        }
      />
      <Box sx={{
        paddingTop: "4px",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        top: 0,
        bottom: 0,
        overflowX: "hidden",
        overflowY: "auto"
      }}
      >
        {selectedElement ?
          <ItemProperties /> :
          <Box sx={{ width: "100%" }}>
            <Typography
              variant="h4"
              sx={{ textAlign: "left" }}
            >
              Please select an element
            </Typography>
          </Box>
        }
      </Box>
    </>
  );
}

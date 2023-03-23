import React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import ItemProperties from "./ItemProperties";
import { useSelector } from "react-redux";
import { selectIfcElementProps } from "../../services/ifcElementPropsSlice";
import { ToggleButton } from "@mui/material";

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

  const selectedElement = true;
  return (
    <>
      <PanelTitle
        title="Properties"
        controlsGroup={
          <Box>
            <ToggleButton
              title="toggle drawer"
              // onClick={toggleIsPropertiesOn}
              icon={
                <Box sx={{
                  display: "flex",
                  justifyContent: "center",
                  alignItems: "center",
                  width: "14px",
                  height: "14px"
                }}
                >
                  x
                </Box>}
              value={""}
            />
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
        {ifcElement ?
          (<>
            <p>
              Type: {ifcElement.name}
            </p>
            <p>
              ExpressedId: {ifcElement.expressId}
            </p>
          </>) : null
        }
      </Box>
    </>
  );
}

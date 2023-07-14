import React, { useEffect, useState } from "react";
import { Box, Drawer } from "@mui/material";
import { useSelector } from "react-redux";
import { PropertiesPanel } from "./SideDrawerPanel";
import { isDrawerOpenFromStore } from "../../services/controlUISlice";
import NavTree from "./NavTree";
import NavPanel from "./NavPanel";
import { selectIfcModel, selectRootElt } from "../../services/ifcModelSlice";
import { ModelsDrawerPage } from "../pages/ModelPage/components/ModelsDrawerPage";

export function SideDrawer({ isDrawerOpen, closeDrawer, isPropertiesOn, isModelsOn }) {
  const [defaultExpandedElements, setDefaultExpandedElements] = useState([])
  const [expandedElements, setExpandedElements] = useState([])
  const [defaultExpandedTypes, setDefaultExpandedTypes] = useState([])
  const [expandedTypes, setExpandedTypes] = useState([])
  const [navigationMode, setNavigationMode] = useState('spatial-tree')
  
  // useEffect(() => {
  //   if (!isPropertiesOn && isDrawerOpen) {
  //     closeDrawer();
  //   }
  // }, [isPropertiesOn, isDrawerOpen, closeDrawer]);
  
  const model = useSelector(selectIfcModel);
  const rootElement = useSelector(selectRootElt);
  
  return (
    <Drawer
      open={isDrawerOpen}
      anchor="right"
      variant="persistent"
      elevation={4}
      sx={{
        "& > .MuiPaper-root": {
          width: "415px",
          paddingLeft: "16px"
        },
        "& .MuiPaper-root": {
          marginTop: "0px",
          borderRadius: "0px"
        }
      }}
    >
      <Box 
        sx={{
        height: "100%",
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between",
        overflowX: "hidden",
        overflowY: "auto"
      }}
      >
        {/*<Box*/}
        {/*  sx={{*/}
        {/*    display: "none",*/}
        {/*    height: isPropertiesOn ? "50%" : "100%",*/}
        {/*    borderRadius: "0px",*/}
        {/*    borderBottom: `1px solid #212529`,*/}
        {/*    paddingTop: "20px",*/}
        {/*    overflowX: "hidden",*/}
        {/*    overflowY: "auto"*/}
        {/*  }}*/}
        {/*>*/}
        {/*  */}
        {/*</Box>*/}
        <Box 
          sx={{
          // display: isPropertiesOn ? "block" : "none",
          height: "100%",
          borderRadius: "5px",
          overflowX: "hidden",
          overflowY: "auto"
        }}
        >
          {/*{isModelsOn && <ModelsDrawerPage />}*/}
          {/*{isPropertiesOn && <PropertiesPanel />}*/}
          {/*{isLayersOn && <LayersPanel />}*/}
          {/*<NavTree />*/}
          
          {model &&
            <NavPanel
              model={model}
              element={rootElement}
              defaultExpandedElements={defaultExpandedElements}
              defaultExpandedTypes={defaultExpandedTypes}
              expandedElements={expandedElements}
              setExpandedElements={setExpandedElements}
              expandedTypes={expandedTypes}
              setExpandedTypes={setExpandedTypes}
              navigationMode={navigationMode}
              setNavigationMode={setNavigationMode}
            />
          }
        </Box>
      </Box>
    </Drawer>
  );
}

export default function SideDrawerWrapper() {
  const [isDrawerOpen, setIsDrawerOpen] = useState(false);
  const [isModelsOn, setIsModelsOn] = useState(true);
  const [closeDrawer, setCloseDrawer] = useState(false);
  const [isPropertiesOn, setIsPropertiesOn] = useState(false);
  const [openDrawer, setOpenDrawer] = useState(true);
  const isOpen = useSelector(isDrawerOpenFromStore);

  useEffect(() => {
    setIsDrawerOpen(isOpen);
    setIsPropertiesOn(isOpen)
  }, [isOpen]);

  return (
    <>
      {isDrawerOpen &&
        <SideDrawer
          isDrawerOpen={isDrawerOpen}
          closeDrawer={closeDrawer}
          isPropertiesOn={isPropertiesOn}
          openDrawer={openDrawer}
          isModelsOn={isModelsOn}
        />}
    </>
  );
}

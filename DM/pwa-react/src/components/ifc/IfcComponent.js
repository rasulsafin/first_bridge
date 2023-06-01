import React, { useEffect, useRef, useState } from "react";
import { IfcViewerAPI } from "web-ifc-viewer";
import { Backdrop, CircularProgress } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { selectIfcElementProps } from "../../services/ifcElementPropsSlice";
import { Color } from "three";
import SideDrawerWrapper from "./SideDrawer";
import { selectIfcModel, setElement, setIfcModel } from "../../services/ifcModelSlice";
import NavPanel from "./NavPanel";
import MenuOfElementModel from "./MenuOfElementModel";

const IfcComponent = () => {
  const viewerRef = useRef();
  const dispatch = useDispatch();
  const [currentElementId, setCurrentElementId] = useState();
  const [currentElementName, setCurrentElementName] = useState();
  const [guidEl, setGuidEl] = useState();
  const ifcElementProps = useSelector(selectIfcElementProps);
  const [instanceViewer, setInstanceViewer] = useState(null);
  const [stateLoading, setStateLoading] = useState(false);
  // const path = `../../${ifcElementProps.fileName}`;
  const model = useSelector(selectIfcModel);
  const [viewer, setViewer] = useState();
  const [loadingMessage, setLoadingMessage] = useState();
  const [anchorEl, setAnchorEl] = useState(null);
  const isMenuOpen = Boolean(anchorEl);
  const [openDialog, setOpenDialog] = useState(false);

  const handleMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  useEffect(() => {
    const container = document.getElementById("viewer-container");
    const viewerAPI = new IfcViewerAPI({ container, backgroundColor: new Color(0xf4f4f4) });

    viewerAPI.axes.setAxes();
    viewerAPI.grid.setGrid();
    viewerAPI.IFC.setWasmPath("../../");
    viewerAPI.IFC.loader.ifcManager.applyWebIfcConfig({
      COORDINATE_TO_ORIGIN: true,
      USE_FAST_BOOLS: true
    });
    viewerRef.current = viewerAPI;
    setViewer(viewerAPI);

    setInstanceViewer(viewerRef.current);

    return () => {
      viewerAPI.dispose();
    };
  }, []);

  const ifcOnLoad = async (e) => {
    const file = e && e.target && e.target.files && e.target.files[0];
    setStateLoading(true);
    if (file && viewerRef) {
      const ifcModel = await viewerRef.current.IFC.loadIfc(file, true);
      dispatch(setIfcModel(ifcModel));
    }

    setStateLoading(false);
  };

  const ifcOnClick = async (event) => {
    setAnchorEl(event.currentTarget);
    if (viewer) {
      const result = await viewer.IFC.selector.pickIfcItem(true);

      if (result) {
        const props = await viewer.IFC.getProperties(result.modelID, result.id, true, true);
        dispatch(setElement(props));

        const type = viewer.IFC.loader.ifcManager.getIfcType(result.modelID, result.id);
        setCurrentElementId(result.id);
        // setCurrentElementName(convertFromCodePoint(props.Name && props.Name?.value));

        if (props.psets) {
          props.psets.map(item =>
            item.HasProperties.map(i => {
              if (i.Name.value === "GUID") {
                if (i.NominalValue.value)
                  setGuidEl(i.NominalValue.value);
              }
            }));
        }
      }
    }
  };

  return (
    <>
      <input
        type="file"
        id="file"
        accept=".ifc"
        onChange={ifcOnLoad}
        style={{ display: "none" }}
      />
      <label htmlFor="file">
        Open File
      </label>
      <div
        id="viewer-container"
        style={{
          height: "90vh",
          width: "70vw"
        }}
        onDoubleClick={ifcOnClick}
        onMouseMove={viewer && (() => viewer.IFC.selector.prePickIfcItem())}
      >
      </div>
      <Backdrop
        style={{
          zIndex: 100,
          display: "flex",
          alignItems: "center",
          alignContent: "center"
        }}
        open={stateLoading}
      >
        {stateLoading && <CircularProgress
          color="error"
          size="10rem"
        />}
      </Backdrop>
      <SideDrawerWrapper />
      <MenuOfElementModel
        anchorEl={anchorEl}
        open={isMenuOpen}
        onClose={handleMenuClose}
      />
    </>
  );
};

export default IfcComponent;
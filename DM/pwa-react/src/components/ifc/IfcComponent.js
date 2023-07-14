import React, { useEffect, useState } from "react";
import { IfcViewerAPI } from "web-ifc-viewer";
import { Backdrop, CircularProgress } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { selectIfcElementProps } from "../../services/ifcElementPropsSlice";
import { Color } from "three";
import SideDrawerWrapper from "./SideDrawer";
import { selectIfcModel, setElement, setIfcModel, setRootElt, setViewerInstance } from "../../services/ifcModelSlice";
import NavPanel from "./NavPanel";
import MenuOfElementModel from "./MenuOfElementModel";

const IfcComponent = () => {
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
  const [elementsById] = useState({});

  const handleMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  useEffect(() => {
    const container = document.getElementById("viewer-container");
    const viewerAPI = new IfcViewerAPI({ container, backgroundColor: new Color(0xf4f4f4) });
    // viewerAPI.axes.setAxes();
    // viewerAPI.grid.setGrid();
    viewerAPI.IFC.setWasmPath("../../");
    viewerAPI.IFC.loader.ifcManager.applyWebIfcConfig({
      COORDINATE_TO_ORIGIN: true,
      USE_FAST_BOOLS: true
    });
    setViewer(viewerAPI);
    dispatch(setViewerInstance(viewerAPI));

    return () => {
      viewerAPI.dispose();
    };
  }, []);

  const ifcOnLoad = async (e) => {
    const file = e && e.target && e.target.files && e.target.files[0];
    setStateLoading(true);
    if (file && viewer) {
      const ifcModel = await viewer.IFC.loadIfc(file, true);
      const rootElt = await ifcModel.ifcManager.getSpatialStructure(0, true);

      console.log(ifcModel);

      dispatch(setRootElt(rootElt));
      dispatch(setIfcModel(ifcModel));

      const allIDs = getAllIds(ifcModel);
      const subset = getWholeSubset(viewer, ifcModel, allIDs);

      replaceOriginalModelBySubset(viewer, ifcModel, subset);
    }

    setStateLoading(false);
  };

  function getAllIds(ifcModel) {
    return Array.from(
      new Set(ifcModel.geometry.attributes.expressID.array)
    );
  }

  function replaceOriginalModelBySubset(viewer, ifcModel, subset) {
    const items = viewer.context.items;

    console.log("items in context", items);

    items.pickableIfcModels = items.pickableIfcModels.filter(model => model !== ifcModel);
    items.ifcModels = items.ifcModels.filter(model => model !== ifcModel);

    ifcModel.removeFromParent();

    items.ifcModels.push(subset);
    items.pickableIfcModels.push(subset);
  }

  function getWholeSubset(viewer, ifcModel, allIDs) {
    return viewer.IFC.loader.ifcManager.createSubset({
      modelID: ifcModel.modelID,
      ids: allIDs,
      applyBVH: true,
      scene: ifcModel.parent,
      removePrevious: true,
      customID: "full-model-subset"
    });
  }

  const ifcOnClick = async (event) => {
    setAnchorEl(event.currentTarget);
    if (viewer) {
      const result = await viewer.IFC.selector.pickIfcItem(true);

      if (result) {
        const props = await viewer.IFC.getProperties(result.modelID, result.id, true, true);
        dispatch(setElement(props));

        const type = viewer.IFC.loader.ifcManager.getIfcType(result.modelID, result.id);
        console.log("type", type);
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
          size="5rem"
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
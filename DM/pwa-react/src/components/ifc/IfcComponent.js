import React, { useCallback, useEffect, useRef, useState } from "react";
import { IfcViewerAPI } from "web-ifc-viewer";
import { Backdrop, CircularProgress } from "@mui/material";
import { FolderOpenOutlined } from "@mui/icons-material";
import { useDispatch, useSelector } from "react-redux";
import { selectIfcElementProps } from "../../services/ifcElementPropsSlice";
import NavPanel from "./NavPanel";
import { Color } from "three";
import SideDrawerWrapper from "./SideDrawer";

const IfcComponent = () => {
  const viewerRef = useRef();
  const ifcElementProps = useSelector(selectIfcElementProps);
  const [instanceViewer, setInstanceViewer] = useState();
  const [stateLoading, setStateLoading] = useState(false);
  const path = `../../${ifcElementProps.fileName}`;
  
  
  const [loadingMessage, setLoadingMessage] = useState()

  useEffect(() => {
    const container = document.getElementById("viewer-container");
    const viewerAPI = new IfcViewerAPI({ container, backgroundColor: new Color(0xffffff) });

    viewerAPI.axes.setAxes();
    viewerAPI.grid.setGrid();
    viewerAPI.IFC.setWasmPath("../../");
    viewerAPI.IFC.loader.ifcManager.applyWebIfcConfig({
      COORDINATE_TO_ORIGIN: true,
      USE_FAST_BOOLS: true
    });
    viewerRef.current = viewerAPI;
    
    viewerRef.current.IFC.loadIfcUrl(path, true, (progressEvent) => {
      if (Number.isFinite(progressEvent.loaded)) {
        const loadedBytes = progressEvent.loaded
        const loadedMegs = (loadedBytes / (1024 * 1024)).toFixed(2)
        setLoadingMessage(`${loadedMegs} MB`)
      }
    });

    setTimeout(() => {
      viewerRef.current.IFC.selector.pickIfcItemsByID(0, ifcElementProps.expressId, true);

    }, 1000)
    
  }, []);


  // useEffect(() => {
  //   if(instanceViewer) {
  //     console.log("pick")
  //     instanceViewer.IFC.selector.pickIfcItemsByID(0, ifcElementProps.expressId, true);
  //   }
  // }, [instanceViewer])


  const ifcOnLoad = async (e) => {
    const file = e && e.target && e.target.files && e.target.files[0];
    setStateLoading(true);
    if (file && viewerRef) {
      const ifcModel = await viewerRef.current.IFC.loadIfc(file, true);
    }
    setStateLoading(false);
  };

  // const ifcOnClick = async (event) => {
  //   if (viewer) {
  //     const result = await viewer.IFC.selector.pickIfcItem(true);
  //     if (result) {
  //       const props = await viewer.IFC.getProperties(result.modelID, result.id, true, true);
  //       // const type = viewer.IFC.loader.ifcManager.getIfcType(result.modelID, result.id);
  //       setCurrentElementId(result.id);
  //       setCurrentElementName(convertFromCodePoint(props.Name && props.Name?.value));
  //
  //       if (props.psets) {
  //         props.psets.map(item =>
  //           item.HasProperties.map(i => {
  //             if (i.Name.value === "GUID") {
  //               // if (i.NominalValue.value)
  //               setGuidEl(i.NominalValue.value);
  //             }
  //           }));
  //       }
  //     }
  //   }
  // };

  return (
    <>
      {/*{loadingMessage ? loadingMessage : null}*/}
      {/*<label htmlFor="file">*/}
      {/*  <FolderOpenOutlined />*/}
      {/*  Open File*/}
      {/*</label>*/}
      <div
        id="viewer-container"
        style={{
          height: "70%",
          width: "80%"
        }}
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
        <CircularProgress />
      </Backdrop>
      {/*<NavPanel/>*/}
      <SideDrawerWrapper/>
    </>

  );
};

export default IfcComponent;
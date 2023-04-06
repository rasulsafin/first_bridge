import React, { createRef, useEffect, useState } from "react";
import { IfcContainer } from "./IfcContainer";
import { Color } from "three";
import { IfcViewerAPI } from "web-ifc-viewer";
import { Backdrop, CircularProgress } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { selectIfcModel, selectViewerInstance, setIfcModel, setViewerInstance } from "../../../services/ifcModelSlice";
import { selectAllProjects } from "../../../services/projectsSlice";

const ViewerIfc = (props) => {
  const ifcContainer = createRef();
  const [viewer, setViewer] = useState();
  const [instanceViewer, setInstanceViewer] = useState(false);
  const [ids, setIds] = useState([]);
  const [fileName, setFileName] = useState();
  const [stateLoading, setStateLoading] = useState(false);
  const [modelTest, setModelTest] = useState();
  const dispatch = useDispatch();
  const model = useSelector(selectIfcModel);
  
  useEffect(() => {
    if (instanceViewer === false) {
      setInstanceViewer(true);
      const container = ifcContainer.current;
      const ifcViewer = new IfcViewerAPI({ container, backgroundColor: new Color(0xffffff) });
      // dispatch(setViewerInstance(ifcViewer));
      // ifcViewer.grid.setGrid(50, 20, new Color(0xcccccc), new Color(0xaaaaaa));
      ifcViewer.axes.setAxes();
      ifcViewer.IFC.setWasmPath("../../");
      ifcViewer.IFC.loader.ifcManager.applyWebIfcConfig({
        COORDINATE_TO_ORIGIN: true,
        USE_FAST_BOOLS: true
      });
      setViewer(ifcViewer);
    }
  }, []);

  const ifcOnLoad = async (e) => {
    const file = e && e.target && e.target.files && e.target.files[0];
    setStateLoading(true);
    if (file && viewer) {
      console.log(file);
      const ifcModel = await viewer.IFC.loadIfc(file, true);
      setFileName(file.name);

      const ifcProject = await viewer.IFC.getSpatialStructure(ifcModel.modelID, true);

      setModelTest(ifcProject);

      dispatch(setIfcModel(ifcModel));

      const allIds = getAllIds(ifcModel);
      setIds(allIds);
      console.log(allIds);
    }
    setStateLoading(false);
  };

  function getAllIds(model) {
  return Array.from(
    new Set(model.geometry.attributes.expressID.array),
  );
}

console.log("model", modelTest)

  return (
    <div>
      <input
        type="file"
        id="file"
        accept=".ifc"
        onChange={ifcOnLoad}
        style={{ display: "none" }}
      />
      <label htmlFor="file">
        {/*<FolderOpenOutlined />*/}
        Open File
      </label>
    <br/>
      {/*<button*/}
      {/*onClick={() => deleteModelHandle()}*/}
      {/*>delete</button>*/}
      <IfcContainer
        ref={ifcContainer}
        viewer={viewer}
        file={fileName}
      />
      <Backdrop
        style={{
          zIndex: 100,
          display: "flex",
          alignItems: "center",
        }}
        open={stateLoading}
      >
        <CircularProgress/>
      </Backdrop>
    </div>
  );
};

export default ViewerIfc;
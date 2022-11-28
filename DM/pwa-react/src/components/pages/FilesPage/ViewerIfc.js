import React, { createRef, useEffect, useState } from "react";
import { IfcContainer } from "./IfcContainer";
import { Color } from "three";
import { IfcViewerAPI } from "web-ifc-viewer";
import { FolderOpenOutlined } from "@mui/icons-material";

const ViewerIfc = () => {
  const ifcContainer = createRef();
  const [viewer, setViewer] = useState();
  const [instanceViewer, setInstanceViewer] = useState(false);

  useEffect(() => {
    if (instanceViewer === false) {
      setInstanceViewer(true);
      const container = ifcContainer.current;
      const ifcViewer = new IfcViewerAPI({ container, backgroundColor: new Color(0xffffff) });
      ifcViewer.grid.setGrid(50, 20, new Color(0xcccccc), new Color(0xaaaaaa));
      ifcViewer.axes.setAxes(10);
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
    if (file && viewer) {

      await viewer.IFC.loadIfc(file, true);
    }
  };

  function getAllItems() {
    console.log("get all items")
  }

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
        <FolderOpenOutlined />
        Open File
      </label>
      <IfcContainer
        ref={ifcContainer}
        viewer={viewer} />
      
    </div>
  );
};

export default ViewerIfc;
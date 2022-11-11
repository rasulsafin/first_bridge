import { useEffect, useState } from "react";
import { IfcViewerAPI } from "web-ifc-viewer";
import { Color } from 'three';


export const ViewerIFC = () => {
  const [container, setContainer] = useState();
  const [input, setInput] = useState();

  useEffect(() => {
    const elementContainer = document.querySelector("#viewer-container");
    const elementInput = document.getElementById("file-input");
    setContainer(elementContainer);
    setInput(elementInput);
  }, []);

  if (container !== undefined && container !== null) {

    const viewer = new IfcViewerAPI({ container, backgroundColor: new Color(255, 255, 255) });
    viewer.axes.setAxes();
    viewer.grid.setGrid();

    input.addEventListener(
      "change",

      async (changed) => {
        const file = changed.target.files[0];
        const ifcURL = URL.createObjectURL(file);
        await viewer.IFC.loadIfcUrl(ifcURL);
      },

      false
    );
  }

  return (
    <div>
      <h3>Viewer IFC</h3>
      <input type="file" id="file-input" accept=".ifc, .ifcXML, .ifcZIP" />
        <div id="viewer-container"></div>
    </div>
  )
}
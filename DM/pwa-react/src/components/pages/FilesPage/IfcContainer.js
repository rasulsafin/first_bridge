import React, { forwardRef, useState } from "react";

const IfcContainer = forwardRef((props, ref) => {
  const [expressId, setExpressId] = useState("");
  const [currentModel, setCurrentModel] = useState();
  const viewer = props.viewer;
  

  const ifcOnClick = async (event) => {
    if (viewer) {
      const result = await viewer.IFC.pickIfcItem(true);
      setCurrentModel(result)
      if (result) {
        const props = await viewer.IFC.getProperties(result.modelID, result.id, true, true);
        console.log(props);
        const type = viewer.IFC.loader.ifcManager.getIfcType(result.modelID, result.id);
        console.log("type", viewer, type, result.modelID, result.id);
        
        const structure = await viewer.IFC.getSpatialStructure(result.modelID, true)
        console.log(structure)
        // const items = await viewer.IFC.getAllItemsOfType(result.modelID, props.type, true)
        // console.log("items", items);
      }
    }
  };

  console.log(currentModel)
  const selectItemById = async () => {
    if (viewer) {
      const modelID = viewer.IFC.getModelID();

      const itemById = await viewer.IFC.selector.pickIfcItemsByID(modelID, 39385, true);
      console.log(itemById)
    }
    
  }

  const getAllItems = async () => {
    if (viewer) {
      console.log(currentModel.id)
      const items = await viewer.IFC.getAllItemsOfType(currentModel.id, props.type, true)
      console.log("items", items);
    }

  }
  
  return (
    <>
      <div>
        <input 
          type="text" 
          width="100px"
          onChange={event => setExpressId(event.target.value)}
        ></input>
        <button
        onClick={selectItemById}
        >select by id</button>
          <button
            onClick={getAllItems}
          >get all items</button>
      </div>
      <div
        style={{
          position: "relative",
          width: "80vw",
          height: "80vh",
          overflow: "hidden"
        }}
        className="ifcContainer"
           ref={ref}
           onDoubleClick={ifcOnClick}
           onMouseMove={viewer && (() => viewer.IFC.selector.prePickIfcItem())}
      />
    </>
  );
});

export { IfcContainer };

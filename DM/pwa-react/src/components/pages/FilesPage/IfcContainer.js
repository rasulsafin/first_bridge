import React, { forwardRef, useEffect, useState } from "react";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import { selectGuid, setIfcGuid } from "../../../services/ifcGuidSlice";

const IfcContainer = forwardRef((props, ref) => {
  const dispatch = useDispatch();
  const [expressId, setExpressId] = useState("");
  const [currentModel, setCurrentModel] = useState();
  const viewer = props.viewer;
  const [memberId, setMemberId] = useState();
  const expressIds = props.ids;
  const [curIfcRecords, setIfcRecords] = useState();
  const [guidEl, setGuidEl] = useState();
  const ifcGuid = useSelector(selectGuid);

  const [isClippingPaneSelected, setClippingPaneSelected] = useState(false);

  const toggleClippingPlanes = () => {
    if (viewer) {
      viewer.clipper.toggle();
      if (viewer.clipper.active) {
        setClippingPaneSelected(true);
      } else {
        setClippingPaneSelected(false);
      }
    }
  };
  
  const changeExpressId = (event) => {
    setExpressId(event.target.value);
    expressIds.push(expressId);
  };

  const ifcOnClick = async (event) => {
    if (viewer) {
      const result = await viewer.IFC.selector.pickIfcItem(true);
      setCurrentModel(result);
      if (result) {
        const props = await viewer.IFC.getProperties(result.modelID, result.id, true, true);
        console.log("props", props);
        const type = viewer.IFC.loader.ifcManager.getIfcType(result.modelID, result.id);
        console.log("type", type, result.modelID, "result.id:", result.id, result);
        setMemberId(result.id);
        // if (props.psets) {
        //   props.psets.map(item => item.HasProperties.map(i => {
        //     if (i.Name.value === "GUID") {
        //       if (i.NominalValue.value)
        //         setGuidEl(i.NominalValue.value);
        //     }
        //   }));
        // }

        if (props) {
          const ifcRecords = {};
          ifcRecords["Entity Type"] = type;
          ifcRecords["GlobalId"] = props.GlobalId && props.GlobalId?.value;
          ifcRecords["Name"] = props.Name && props.Name?.value;
          ifcRecords["ObjectType"] =
            props.ObjectType && props.ObjectType?.value;
          ifcRecords["PredefinedType"] =
            props.PredefinedType && props.PredefinedType?.value;
          setIfcRecords(ifcRecords);
        }
      }
    }
  };

  console.log(curIfcRecords);

  // if (guidEl !== null) {
  //   dispatch(setIfcGuid(guidEl));
  // }

  const selectItemsById = async () => {
    if (viewer) {
      console.log("select all", currentModel);
      await viewer.IFC.selector.pickIfcItemsByID(0, expressIds, true);
    }
  };

  const keyPressHandle = (event) => {
    console.log("key");
    if (event.code === "KeyC") {
      viewer.IFC.selector.unpickIfcItems();
      viewer.IFC.selector.unHighlightIfcItems();
    }
  };

  useEffect(() => {
    document.addEventListener("keydown", keyPressHandle);
    return () => document.removeEventListener("keydown", keyPressHandle);
  }, [keyPressHandle]);

  const ifcOnRightClick = async () => {
    await viewer.clipper.deleteAllPlanes();
    await viewer.clipper.createPlane();
    console.log("right button");
  };

  function convertFromCodePoint(str) {
    const fromStr = str.split("\\");
    const arr = [];
    let normalString = [];

    for (let i = 0; i < fromStr.length; i++) {
      if (fromStr[i].startsWith("04")) {
        arr.push(fromStr[i]);
      }
    }

    for (let i = 0; i < arr.length; i++) {
      const innerArr = arr[i].split("04");
      for (let i = 0; i < innerArr.length; i++) {
        if (innerArr[i] !== "") {
          const normalChar = String.fromCodePoint(Number(`0x04${innerArr[i]}`));
          normalString.push(normalChar);
        } else {
          normalString.push(" ");
        }
      }
    }
    return normalString.join("");
  }

  return (
    <>
      <div>
        {/*<button*/}
        {/*  onClick={selectItemsById}*/}
        {/*>select all*/}
        {/*</button>*/}
        <br/>
        <button
          onClick={() => toggleClippingPlanes()}
          // selected={isClippingPaneSelected}
        > clipping plane mode
        </button>
        <br/>
        <p>
          {/*{guidEl}*/}
          <br/>
          {curIfcRecords ? convertFromCodePoint(curIfcRecords.Name) : null}
        </p>

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
        // onMouseMove={viewer && (() => viewer.IFC.selector.prePickIfcItem())}
        onContextMenu={ifcOnRightClick}
        onKeyDown={keyPressHandle}
      />
    </>
  );
});

export { IfcContainer };

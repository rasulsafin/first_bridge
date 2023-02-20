import React, { forwardRef, useEffect, useState, useCallback } from "react";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import { setIfcElementProps } from "../../../services/ifcElementPropsSlice";

const IfcContainer = forwardRef((props, ref) => {
  const dispatch = useDispatch();
  const viewer = props.viewer;
  const expressIds = props.ids;
  const fileName = props.file;
  const [currentElementId, setCurrentElementId] = useState();
  const [currentElementName, setCurrentElementName] = useState();
  const [guidEl, setGuidEl] = useState();
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

  const ifcOnClick = async (event) => {
    if (viewer) {
      const result = await viewer.IFC.selector.pickIfcItem(true);
      if (result) {
        const props = await viewer.IFC.getProperties(result.modelID, result.id, true, true);
        // const type = viewer.IFC.loader.ifcManager.getIfcType(result.modelID, result.id);
        setCurrentElementId(result.id);
        setCurrentElementName(convertFromCodePoint(props.Name && props.Name?.value));

        if (props.psets) {
          props.psets.map(item =>
            item.HasProperties.map(i => {
              if (i.Name.value === "GUID") {
                // if (i.NominalValue.value)
                setGuidEl(i.NominalValue.value);
              }
            }));
        }
      }
    }
  };

  const keyPressHandle = useCallback(event => {
    console.log("key");
    if (event.code === "KeyC") {
      viewer.IFC.selector.unpickIfcItems();
      viewer.IFC.selector.unHighlightIfcItems();
    }
  }, [viewer]);

  useEffect(() => {
      console.log("useEffect first render");
    }, []
  );

  useEffect(() => {
    console.log("ifcClick", currentElementName);
    dispatch(setIfcElementProps({
      guid: guidEl,
      name: currentElementName,
      fileName: fileName,
      expressId: currentElementId,
      isElement: true,
    }));
  }, [currentElementId]);

  console.log("render");

  useEffect(() => {
    document.addEventListener("keydown", keyPressHandle);
    return () => document.removeEventListener("keydown", keyPressHandle);
  }, [keyPressHandle]);

  const ifcOnRightClick = async () => {
    await viewer.clipper.deleteAllPlanes();
    await viewer.clipper.createPlane();
  };

  function convertFromCodePoint(str) {
    if (str.includes("04")) {
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
    } else {
      return str;
    }
  }

  return (
    <div>
      {/*<div>*/}
      {/*  <br />*/}
      {/*  <Controls.Checkbox*/}
      {/*    checked={isClippingPaneSelected}*/}
      {/*    onChange={() => toggleClippingPlanes()}*/}
      {/*    label="Clipping plane mode"*/}
      {/*  />*/}
      {/*  <br />*/}
      {/*  <p>*/}
      {/*    GUID: {guidEl}*/}
      {/*    <br />*/}
      {/*    Name: {currentElementName}*/}
      {/*    <br />*/}
      {/*    ExpressId: {currentElementId}*/}
      {/*    <br />*/}
      {/*    FileName: {fileName}*/}
      {/*  </p>*/}
      {/*</div>*/}
      <div
        style={{
          // position: "relative",
          width: "100%",
          height: "500px",
          // overflow: "hidden"
        }}
        className="ifcContainer"
        ref={ref}
        onDoubleClick={ifcOnClick}
        // onMouseMove={viewer && (() => viewer.IFC.selector.prePickIfcItem())}
        onContextMenu={ifcOnRightClick}
        onKeyDown={keyPressHandle}
      />
    </div>
  );
});

export { IfcContainer };

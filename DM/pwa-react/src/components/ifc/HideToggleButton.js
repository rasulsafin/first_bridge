import React, { useState } from "react";
// import useStore from '../store/useStore'
// import IfcIsolator from '../Infrastructure/IfcIsolator'
import { ReactComponent as VisibilityIcon } from "../../assets/icons/visibilityIcon.svg";
import { ReactComponent as VisibilityOffIcon } from "../../assets/icons/visibilityOffIcon.svg";
import { useSelector } from "react-redux";
import { selectIfcModel, selectViewerInstance } from "../../services/ifcModelSlice";

/**
 // * @param {IfcIsolator} The IFC isoaltor
 * @param {number} IFC element id
 * @return {object} React component
 */
export default function HideToggleButton({ elementId, viewer }) {
  
  // const viewer = useSelector(selectViewerInstance);
  // const isHidden = useStore((state) => state.hiddenElements[elementId])
  // const updateHiddenStatus = useStore((state) => state.updateHiddenStatus)
  // const isIsolated = useStore((state) => state.isolatedElements[elementId])
  // const isTempIsolationModeOn = useStore((state) => state.isTempIsolationModeOn)
  // const viewer = useStore((state) => state.viewerStore)

  const isHidden = true;
  // const isIsolated = true;
  const ids = [];
  ids.push(elementId);

  const toggleHide = async () => {

    // await viewer.IFC.selector.pickIfcItemsByID(0, ids);

    // const result = viewer.context.castRayIfc();

    // if (!result) return;
    
    await viewer.IFC.loader.ifcManager.removeFromSubset(
      0,
      [ids],
      "full-model-subset"
    );

    console.log("ids", ids);

    // console.log("result", result)

    // const toBeHidden = viewer.isolator.flattenChildren(elementId)
    // if (!isHidden) {
    //   viewer.isolator.hideElementsById(toBeHidden)
    //   if (!Number.isInteger(elementId)) {
    //     updateHiddenStatus(elementId, true)
    //   }
    // } else {
    //   viewer.isolator.unHideElementsById(toBeHidden)
    //   if (!Number.isInteger(elementId)) {
    //     updateHiddenStatus(elementId, false)
    //   }
    // }
  };

  // const iconStyle = {
  //   float: 'right',
  //   marginTop: '2px',
  //   height: '20px',
  //   opacity: 0.3,
  //   visibility: 'hidden',
  // }
  // if (isTempIsolationModeOn) {
  //   iconStyle.pointerEvents = 'none'
  //   if (isIsolated) {
  //     iconStyle.opacity = 1
  //     iconStyle.width = '27px'
  //   }
  // }

  // if (isIsolated) {
  //   return <VisibilityIcon onClick={toggleHide} />;
  // } else 
    if (!isHidden) {
    return <VisibilityIcon onClick={toggleHide} />;
  } else {
    return <VisibilityOffIcon onClick={toggleHide} />;
  }
}

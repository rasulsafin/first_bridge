import React from 'react'
// import useStore from '../store/useStore'
// import IfcIsolator from '../Infrastructure/IfcIsolator'
import { ReactComponent as VisibilityIcon } from "../../assets/icons/visibilityIcon.svg";
import { ReactComponent as VisibilityOffIcon } from "../../assets/icons/visibilityOffIcon.svg";

/**
 // * @param {IfcIsolator} The IFC isoaltor
 * @param {number} IFC element id
 * @return {object} React component
 */
export default function HideToggleButton({elementId}) {
  
  // const isHidden = useStore((state) => state.hiddenElements[elementId])
  // const updateHiddenStatus = useStore((state) => state.updateHiddenStatus)
  // const isIsolated = useStore((state) => state.isolatedElements[elementId])
  // const isTempIsolationModeOn = useStore((state) => state.isTempIsolationModeOn)
  // const viewer = useStore((state) => state.viewerStore)

  const isHidden = false;
  const isIsolated = false;

  const toggleHide = () => {
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
  }

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

  if (isIsolated) {
    return <VisibilityIcon onClick={toggleHide}/>
  } else if (!isHidden) {
    return <VisibilityIcon onClick={toggleHide}/>
  } else {
    return <VisibilityOffIcon onClick={toggleHide}/>
  }
}

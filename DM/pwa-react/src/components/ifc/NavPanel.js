import React, { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { useSelector } from "react-redux";
import Paper from "@mui/material/Paper";
import TreeView from "@mui/lab/TreeView";
import TypesNavTree from "./TypesNavTree";
import { ReactComponent as NodeOpenIcon } from "../../assets/icons/caret.svg";
import { ReactComponent as NodeCloseIcon } from "../../assets/icons/caretRight.svg";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import Tooltip from "@mui/material/Tooltip";
import NavTree from "./NavTree";
import { styled } from "@mui/material/styles";
import { selectElement } from "../../services/ifcModelSlice";
import { SearchBar } from "../searchBar/SearchBar";

export function useExistInFeature(name) {
  const [existInFeature, setExistInFeature] = useState(false);
  const [searchParams] = useSearchParams();

  useEffect(() => {
    setExistInFeature(false);
    if (!name) {
      return;
    }
    const lowerName = name.toLowerCase();
    const enabledFeatures = searchParams.get("feature");
    if (!enabledFeatures) {
      return;
    }
    const enabledFeatureArr = enabledFeatures.split(",");

    for (let i = 0; i < enabledFeatureArr.length; i++) {
      if (enabledFeatureArr[i].toLowerCase() === lowerName) {
        setExistInFeature(true);
        return;
      }
    }
  }, [name, searchParams]);


  return existInFeature;
}


/**
 * If cond is true, do nothing.  Otherwise, throw error with msg.
 *
 * @param {boolean} cond Test condition.
 * @param {string} msg path to the button icon.
 * @throws If the condition is false.
 */
export function assert(cond, msg) {
  if (cond) {
    return;
  }
  throw new Error(msg);
}

/**
 * Equivalent to calling assertDefined on each parameter.
 *
 * @param {any} args Variable length arguments to assert are defined.
 * @return {any} args That was passed in
 * @throws If any argument is not defined.
 */
export function assertDefined(...args) {
  for (const ndx in args) {
    if (Object.prototype.hasOwnProperty.call(args, ndx)) {
      const arg = args[ndx];
      assert(arg !== null && arg !== undefined, `Arg ${ndx} is not defined`);
    }
  }
  if (args.length === 1) {
    return args[0];
  }
  return args;
}


/**
 * @param {object} model
 * @param {object} element
 * @param {Array} selectedElements
 * @param {Array} defaultExpandedElements
 * @param {Array} expandedElements
 * @param {Function} setExpandedElements
 * @return {object}
 */
export default function NavPanel(
  {
    model,
    element,
    defaultExpandedElements,
    defaultExpandedTypes,
    expandedElements,
    setExpandedElements,
    expandedTypes,
    setExpandedTypes,
    navigationMode,
    setNavigationMode
  }
) {
  assertDefined(...arguments);

  const darkModeSearchBar = true;

  const selectedElements = "";
  // useStore((state) => state.selectedElements);

  const elementTypesMap = useSelector(selectElement);
  // useStore((state) => state.elementTypesMap);

  const existNavTypesInFeature = useExistInFeature("navtypes");

  const onTreeViewChanged = (event, value) => {
    if (value !== null) {
      setNavigationMode(value);
    }
  };

  const filterByInput = () => {

  };

  const StyledToggleButtonGroup = styled(ToggleButtonGroup)(({ theme }) => ({
    "& .MuiToggleButtonGroup-grouped": {
      "margin": theme.spacing(0.5),
      "border": 0,
      "&.Mui-disabled": {
        border: 0
      },
      "&:not(:first-of-type)": {
        borderRadius: theme.shape.borderRadius
      },
      "&:first-of-type": {
        borderRadius: theme.shape.borderRadius
      }
    }
  }));

  const isNavTree = existNavTypesInFeature ? navigationMode === "spatial-tree" : true;
  return (
    <div style={{
      width: "100%"
    }}
    >
      <div className="header-model-toolbar">
        <div className="header-model-title">
          Слои
        </div>
        <SearchBar
          darkMode={darkModeSearchBar}
          onChange={e => filterByInput(e)}
        />
      </div>
      <Paper
        elevation={0}
        aria-label="Navigation Panel"
        variant="control"
        sx={{
          "marginTop": "14px",
          "overflow": "auto",
          "width": "100%",
          "height": "100%",
          "opacity": .8,
          "justifyContent": "space-around",
          "alignItems": "center",
          // "maxHeight": "400px",
          "&:hover #togglegrp": {
            visibility: "visible !important"
          },
          "&:hover svg": {
            visibility: "visible !important"
          },
          "@media (max-width: 900px)": {
            maxHeight: "150px",
            top: "86px"
          }
        }}
      >
        <div>
          {/*{existNavTypesInFeature &&*/}
          {/*  <StyledToggleButtonGroup*/}
          {/*    exclusive*/}
          {/*    id={"togglegrp"}*/}
          {/*    value={navigationMode}*/}
          {/*    size="small"*/}
          {/*    sx={{*/}
          {/*      "marginLeft": "16px",*/}
          {/*      "marginTop": "8px",*/}
          {/*      "visibility": "hidden",*/}
          {/*      "& button": {*/}
          {/*        height: "30px",*/}
          {/*        width: "30px"*/}
          {/*      },*/}
          {/*      "& svg": {*/}
          {/*        height: "20px",*/}
          {/*        width: "20px"*/}
          {/*      }*/}
          {/*    }}*/}
          {/*    onChange={onTreeViewChanged}*/}
          {/*  >*/}
          {/*    <ToggleButton value="spatial-tree" aria-label="spatial-tree">*/}
          {/*      <Tooltip*/}
          {/*        title={"Spatial Structure"}*/}
          {/*        describeChild*/}
          {/*        placement={"bottom-end"}*/}
          {/*        PopperProps={{ style: { zIndex: 0 } }}*/}
          {/*      >*/}
          {/*        <div>Icon</div>*/}
          {/*      </Tooltip>*/}
          {/*    </ToggleButton>*/}
          {/*    <ToggleButton value="element-types" aria-label="element-types">*/}
          {/*      <Tooltip*/}
          {/*        title={"Element Types"}*/}
          {/*        describeChild*/}
          {/*        placement={"bottom-end"}*/}
          {/*        PopperProps={{ style: { zIndex: 0 } }}*/}
          {/*      >*/}
          {/*        /!*<ListIcon />*!/*/}
          {/*      </Tooltip>*/}
          {/*    </ToggleButton>*/}
          {/*  </StyledToggleButtonGroup>*/}
          {/*}*/}
          <TreeView
            aria-label={isNavTree ? "IFC Navigator" : "IFC Types Navigator"}
            defaultCollapseIcon={<NodeOpenIcon className="caretToggle" />}
            defaultExpandIcon={<NodeCloseIcon className="caretToggle" />}
            defaultExpanded={isNavTree ? defaultExpandedElements : defaultExpandedTypes}
            expanded={isNavTree ? expandedElements : expandedTypes}
            selected={selectedElements}
            onNodeToggle={(event, nodeIds) => {
              if (isNavTree) {
                setExpandedElements(nodeIds);
              } else {
                setExpandedTypes(nodeIds);
              }
            }}
            key="tree"
            sx={{
              "padding": existNavTypesInFeature ? "7px 0 14px 0" : "14px 0",
              "maxWidth": "380px",
              "overflowY": "auto",
              "overflowX": "hidden",
              "flexGrow": 1,
              "&:focus svg": {
                visibility: "visible !important"
              }
            }}
          >
            <NavTree
              model={model}
              element={element}
            />
            {/*{isNavTree ?*/}
            {/*  <NavTree*/}
            {/*    model={model}*/}
            {/*    element={element}*/}
            {/*  /> :*/}
            {/*  <TypesNavTree*/}
            {/*    model={model}*/}
            {/*    types={elementTypesMap}*/}
            {/*  />}*/}
          </TreeView>
        </div>
      </Paper>
    </div>
  );
}

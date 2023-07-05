import React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import TreeItem, { useTreeItem } from "@mui/lab/TreeItem";
import HideToggleButton from "./HideToggleButton";
import { reifyName } from "./utils/itemProperties";

/**
 * @param {object} model IFC model
 * @param {object} element IFC element of the model
 * @param {string} pathPrefix URL prefix for constructing links to
 *   elements, recursively grown as passed down the tree
 * @return {object} React component
 */
export default function NavTree(
  {
    model,
    element,
    pathPrefix,
    selectWithShiftClickEvents
  }
) {
  const CustomContent = React.forwardRef(function CustomContent(props, ref) {
    const {
      label,
      nodeId,
      icon: iconProp,
      expansionIcon,
      displayIcon,
      hasHideIcon
    } = props;

    const {
      handleExpansion,
      handleSelection,
      preventSelection
    } = useTreeItem(nodeId);

    const icon = iconProp || expansionIcon || displayIcon;

    const handleMouseDown = (event) => preventSelection(event);

    const handleExpansionClick = (event) => handleExpansion(event);

    const handleSelectionClick = (event) => {
      handleSelection(event);
      selectWithShiftClickEvents(event.shiftKey, element.expressID);
    };

    return (
      <div
        onMouseDown={handleMouseDown}
        ref={ref}
      >
        <Box
          onClick={handleExpansionClick}
          sx={{ margin: "0px 14px 0px 14px" }}
        >
          {icon}
        </Box>
        <div style={{ width: "300px" }}>
          <Typography
            variant="tree"
            onClick={handleSelectionClick}
          >
            {label}
          </Typography>
          {hasHideIcon &&
            <div style={{ display: "contents" }}>
              <HideToggleButton elementId={element.expressID} />
            </div>
          }
        </div>
      </div>
    );
  });

  // CustomContent.propTypes = NavTreePropTypes;

  const CustomTreeItem = (props) => {
    return <TreeItem
      ContentComponent={CustomContent} 
      {...props} />;
  };

  const viewer = {};

  // useStore((state) => state.viewerStore);

  const hasHideIcon = {};

  // viewer.isolator.canBeHidden(element.expressID);

  let i = 0;

  return (
    <CustomTreeItem
      nodeId={element.expressID.toString()}
      label={reifyName({ properties: model }, element)}
      ContentProps={{
        hasHideIcon
      }}
    >
      {element.children && element.children.length > 0 ?
        element.children.map((child) => {
          const childKey = `${pathPrefix}-${i++}`;
          return (
            <React.Fragment key={childKey}>
              <NavTree
                model={model}
                element={child}
                pathPrefix={pathPrefix}
                selectWithShiftClickEvents={selectWithShiftClickEvents}
              />
            </React.Fragment>
          );
        }) :
        null}
    </CustomTreeItem>
  );
}

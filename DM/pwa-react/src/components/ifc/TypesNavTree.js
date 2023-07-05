import React from "react";
import NavTree from "./NavTree";
import TreeItem, { useTreeItem } from "@mui/lab/TreeItem";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import HideToggleButton from "./HideToggleButton";

/**
 * @param {object} model IFC model
 * @param {object} collection of element types
 * @param {string} pathPrefix URL prefix for constructing links to
 *   elements, recursively grown as passed down the tree
 * @return {object} React component
 */
export default function TypesNavTree(
  {
    model,
    types,
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
    };

    return (
      // eslint-disable-next-line jsx-a11y/no-static-element-interactions
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
              <HideToggleButton elementId={nodeId} />
            </div>
          }
        </div>
      </div>
    );
  });

  CustomContent.propTypes = TypesNavTreePropTypes;

  const CustomTreeItem = (props) => {
    return <TreeItem ContentComponent={CustomContent} {...props} />;
  };

  let i = 0;

  return types.map((type) =>
    <CustomTreeItem
      key={type.name}
      nodeId={type.name}
      label={type.name}
      ContentProps={{
        hasHideIcon: type.elements && type.elements.length > 0
      }}
    >
      {type.elements && type.elements.length > 0 ?
        type.elements.map((e) => {
          const childKey = `${pathPrefix}-${i++}`;
          return (
            <NavTree
              key={childKey}
              model={model}
              element={e}
              pathPrefix={pathPrefix}
              selectWithShiftClickEvents={selectWithShiftClickEvents}
            />
          );
        }) : null}
    </CustomTreeItem>);
}

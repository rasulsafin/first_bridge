import React from "react";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import TreeItem, { useTreeItem } from "@mui/lab/TreeItem";
import HideToggleButton from "./HideToggleButton";
import { reifyName } from "./utils/itemProperties";
import clsx from "clsx";
import { useSelector } from "react-redux";
import { selectViewerInstance } from "../../services/ifcModelSlice";

/**
 * @param {object} model IFC model
 * @param {object} element IFC element of the model
 * @return {object} React component
 */
export default function NavTree(
  {
    model,
    element
  }
) {
  const CustomContent = React.forwardRef(function CustomContent(props, ref) {
    const {
      classes,
      className,
      label,
      nodeId,
      icon: iconProp,
      expansionIcon,
      displayIcon,
      hasHideIcon
    } = props;

    const {
      disabled,
      expanded,
      selected,
      focused,
      handleExpansion,
      handleSelection,
      preventSelection
    } = useTreeItem(nodeId);

    const ids = [];
    ids.push(element.expressID);

    const icon = iconProp || expansionIcon || displayIcon;

    const handleMouseDown = (event) => {
      preventSelection(event);
    };

    const handleExpansionClick = (event) => {
      handleExpansion(event);
    };

    const handleSelectionClick = (event) => {
      handleSelection(event);

    };

    return (
      <div
        className={clsx(className, classes.root, {
          [classes.expanded]: expanded,
          [classes.selected]: selected,
          [classes.focused]: focused,
          [classes.disabled]: disabled
        })}
        onMouseDown={handleMouseDown}
        ref={ref}
      >
        <Box
          onClick={handleExpansionClick}
          sx={{ margin: "0px 14px 0px 14px" }}
        >
          {icon}
        </Box>
        <div
          style={{
            width: "300px",
            display: "flex",
            direction: "row",
            alignItems: "center",
            gap: "10px"
          }}>
          {hasHideIcon &&
            <div
              style={{
                display: "contents"
              }}>
              <HideToggleButton elementId={element.expressID} viewer={viewer} />
            </div>
          }
          <Typography
            // variant="tree"
            component="div"
            className={classes.label}
            onClick={handleSelectionClick}
            onMouseMove={viewer && (() => viewer.IFC.selector.prepickIfcItemsByID(0, ids))}
            onDoubleClick={viewer && (() => viewer.IFC.selector.pickIfcItemsByID(0, ids))}
          >
            {label}
          </Typography>
        </div>
      </div>
    );
  });

  const CustomTreeItem = (props) => {
    return <TreeItem
      ContentComponent={CustomContent}
      {...props} />;
  };

  const viewer = useSelector(selectViewerInstance);

  const hasHideIcon = {};

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
          const childKey = `${element.expressID}-${i++}`;
          return (
            <React.Fragment
              key={childKey}
            >
              <NavTree
                model={model}
                element={child}
              />
            </React.Fragment>
          );
        }) :
        null}
    </CustomTreeItem>
  );
}

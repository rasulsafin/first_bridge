import React from "react";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import TreeView from "@mui/lab/TreeView";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';

/**
 * @param {object} model
 * @param {object} element
 * @param {Array} selectedElements
 * @param {Array} defaultExpandedElements
 * @param {Array} expandedElements
 * @param {Function} setExpandedElements
 * @param {string} pathPrefix
 * @return {object}
 */
export default function NavPanel({
model,
element,
defaultExpandedElements,
expandedElements,
setExpandedElements,
pathPrefix
}) {

  return (
    <Paper
      sx={{
        "marginTop": "14px",
        "overflow": "auto",
        "width": "300px",
        "opacity": .8,
        "justifyContent": "space-around",
        "alignItems": "center",
        "maxHeight": "100%",
        "backgroundColor": "#b7d5f5"
      }}
      elevation={0}
    >
      <Box>
        <TreeView
          sx={{ flexGrow: 1, maxWidth: "400px", overflowY: "auto", overflowX: "hidden" }}
          aria-label="IFC Navigator"
          defaultCollapseIcon={<ExpandMoreIcon />}
          defaultExpandIcon={<ChevronRightIcon />}
          defaultExpanded={defaultExpandedElements}
          expanded={expandedElements}
          onNodeToggle={(event, nodeIds) => {
            setExpandedElements(nodeIds);
          }}
          key="tree"
        >
        </TreeView>
      </Box>
    </Paper>
  );
}

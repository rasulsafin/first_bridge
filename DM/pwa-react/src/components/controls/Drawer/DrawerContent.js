import React from "react";
import { useDispatch } from "react-redux";
import { Box, IconButton, Typography } from "@mui/material";
import { Controls } from "../Controls";
import { toggleDrawer } from "../../../services/controlUISlice";
import { ReactComponent as CancelIcon } from "../../../assets/icons/cancel.svg";

export const DrawerContent = (props) => {
  const {
    children,
    title,
    cancelButtonProps,
    confirmButtonProps,
    isWithActions
  } = props;
  const dispatch = useDispatch();

  return (
    <Box
      sx={{
        display: "flex",
        alignItems: "flex-start",
        flexDirection: "column",
        justifyContent: "flex-start",
        padding: "24px"
      }}
    >
      <Box
        sx={{
          display: "flex"
        }}
      >
        <Typography variant="h5">{title}</Typography>
        <IconButton
          sx={{
            marginLeft: "252px"
          }}
          title="toggle drawer"
          onClick={() => dispatch(toggleDrawer())}
        >
          <CancelIcon />
        </IconButton>
      </Box>

      <Box sx={{ height: "80%", overflow: "auto", py: 2 }}>
        {children}
      </Box>
      {isWithActions && (
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-end",
            py: 2,
            marginTop: "auto"
          }}
        >
          <Controls.Button
            className="ml-0"
            color="primary"
            variant="contained"
            type="submit"
            {...confirmButtonProps}
          >
            {confirmButtonProps?.children || "Применить"}
          </Controls.Button>
          <Controls.Button
            color="primary"
            variant="outlined"
            {...cancelButtonProps}
          >
            {cancelButtonProps?.children || "Отмена"}
          </Controls.Button>
        </Box>
      )}
    </Box>
  );
};

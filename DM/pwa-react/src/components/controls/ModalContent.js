import React from "react";
import { Box, Typography } from "@mui/material";
import { Controls } from "./Controls";

export const ModalContent = (props) => {
  const {
    children,
    title,
    cancelButtonProps,
    confirmButtonProps,
    isWithActions
  } = props;

  return (
    <>
      <Box
        sx={{
          display: "flex",
          alignItems: "center",
          paddingY: "8px"
        }}
      >
        <Typography variant="h5">{title}</Typography>
      </Box>
      <Box sx={{ height: "80%", overflow: "auto" }}>
        {children}
      </Box>
      {isWithActions && (
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "flex-start",
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
            {confirmButtonProps?.children || "Сохранить"}
          </Controls.Button>
          <Controls.Button
            variant="outlined"
            {...cancelButtonProps}
          >
            {cancelButtonProps?.children || "Отменить"}
          </Controls.Button>
        </Box>
      )}
    </>
  );
};

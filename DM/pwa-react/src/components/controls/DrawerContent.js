import React from "react";
import { Box, Typography } from "@mui/material";
import { Controls } from "./Controls";

export const DrawerContent = (props) => {
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
          borderBottom: "1px solid #DFE3E6"
        }}
      >
        <Typography variant="h6">{title}</Typography>

        <Box sx={{ flexGrow: 1, overflow: "auto", p: 2 }}>{children}</Box>
        {isWithActions && (
          <Box
            sx={{
              display: "flex",
              alignItems: "center",
              justifyContent: "flex-end",
              borderTop: "1px solid #DFE3E6",
              p: 2,
              marginTop: "auto"
            }}
          >
            <Controls.Button
              color="primary"
              variant="outlined"
              {...cancelButtonProps}
            >
              {cancelButtonProps?.children || "Отмена"}
            </Controls.Button>

            <Controls.Button
              color="primary"
              variant="contained"
              type="submit"
              {...confirmButtonProps}
            >
              {confirmButtonProps?.children || "Сохранить"}
            </Controls.Button>
          </Box>
        )}
      </Box>
    </>
  );
};

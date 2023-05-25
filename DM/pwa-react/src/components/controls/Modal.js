import React from "react";
import {
  Box,
  Modal as MuiModal
} from "@mui/material";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "none",
  borderRadius: 3,
  boxShadow: 24,
  pt: 2,
  px: 4,
  pb: 3
};

const Modal = (props) => {
  const { open, onClose, children } = props;

  return (
    <div>
      <MuiModal
        open={open}
        onClose={onClose}
      >
        <Box sx={{ ...style, width: "70%", height: "90%" }}>
          {children}
        </Box>
      </MuiModal>
    </div>
  );
};

export default Modal;
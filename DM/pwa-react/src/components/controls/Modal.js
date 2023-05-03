import React, { useState } from "react";
import { List, Modal as MuiModal } from "@mui/material";
import Box from "@mui/material/Box";
import { Grid } from "@mui/material";
import { Controls } from "./Controls";
import { ReactComponent as TrashIcon } from "../../assets/icons/trashcan.svg";

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
  const { titleModal, onClose, ...other } = props;
  
  
  return (
    <div>
      <MuiModal
        {...props}
      >
        <Box sx={{ ...style, width: "70%", height: "90%" }}>
          <h2 id="parent-modal-title" style={{ marginBottom: "30px" }}>{titleModal}</h2>
          <Box style={{ height: "80%" }}>
            <List style={{ height: "100%", overflow: "auto" }}>
              {props.children}
            </List>
          </Box>
          <Box
            m={1}
            display="flex"
            justifyContent="flex-end"
            alignItems="flex-end"
          >
            <Grid container>
              <Grid item xs={10}>
                <Controls.Button
                >Сохранить
                </Controls.Button>
                <Controls.Button
                onClick={onClose}
                >
                  Отменить
                </Controls.Button>
              </Grid>
              <Grid item xs={2}>
                <Controls.Button
                  startIcon={<TrashIcon />}
                >
                  В архив
                </Controls.Button>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </MuiModal>
    </div>
  );
};

export default Modal;
import React, { useRef, useState } from "react";
import { Modal as MuiModal } from "@mui/material";
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
  
const {titleModal, ...other} = props;


  return (
    <div>
      <MuiModal
        {...props}
        // aria-labelledby="parent-modal-title"
        // aria-describedby="parent-modal-description"
      >
        <Box sx={{ ...style, width: "70%", height: "90%" }}>
          <h2 id="parent-modal-title" style={{ marginBottom: "30px" }}>{titleModal}</h2>
          <Grid container>
            <Grid item xs={12}>
             
            </Grid>
          </Grid>
          <Grid container>
            <Grid item>
              {props.children}
            </Grid>
          </Grid>
          <Grid style={{ marginTop: "100px" }} container>
            <Grid item xs={10}>
              <Controls.Button
              >Сохранить</Controls.Button>
              <Controls.Button>Отменить</Controls.Button>
            </Grid>
            <Grid item xs={2}>
              <Controls.Button startIcon={<TrashIcon />}>В архив</Controls.Button>
            </Grid>
          </Grid>
        </Box>
      </MuiModal>
    </div>
  );
}

export default Modal;
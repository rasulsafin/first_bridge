import React from "react";
import { Dialog, IconButton } from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { reduceTitle } from "../utils/reduceTitle";

export const ProjectDeleteDialog = (props) => {
  const { project, handleDeleteProject, open, close } = props;

  return (
    <Dialog
      PaperProps={{
        sx: {
          borderRadius: "10px",
          minWidth: "35vw",
          height: "58px",
          position: "fixed",
          bottom: "16px",
          left: "30vw"
        }
      }}
      open={open}
      onClose={close}
      hideBackdrop
    >
      <div
        style={{
          display: "flex",
          flexDirection: "row",
          alignItems: "center",
          justifyContent: "space-evenly",
          height: "58px",
          fontSize: "16px",
          fontFamily: "Myriad Pro",
          marginLeft: "20px",
          marginRight: "20px",
          letterSpacing: "0.02rem"
        }}
      >
        {`Вы действительно хотите удалить проект ${reduceTitle(project.title)}?`}
        <Controls.Button
          onClick={handleDeleteProject}
          style={{
            backgroundColor: "#2D2926",
            color: "#FFF",
            border: "none"
          }}
        >Да</Controls.Button>
        <Controls.Button
          onClick={close}
          variant="outlined"
          autoFocus
        >
          Нет
        </Controls.Button>
        <IconButton
          onClick={close}
        >
          <CancelIcon />
        </IconButton>
      </div>
    </Dialog>
  );
};

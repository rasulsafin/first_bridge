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
          minWidth: "40vw",
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
          justifyContent: "center",
          height: "58px"
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

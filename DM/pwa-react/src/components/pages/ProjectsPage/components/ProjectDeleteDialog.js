import React from "react";
import { Dialog, Grid } from "@mui/material";
import { Controls } from "../../../controls/Controls";

export const ProjectDeleteDialog = (props) => {
  const { project, handleDeleteProject, open, close} = props;
  
  return (
    <Dialog
      PaperProps={{ sx: { position: "fixed", bottom: 50, left: "35vw", maxWidth: "md", m: 0, padding: 2 } }}
      open={open}
      onClose={close}
      hideBackdrop
    >
      <Grid container>
        <Grid item md={12}>
          <span>
           {`Вы действительно хотите удалить проект ${project.title} ?`}
          </span>
          <Controls.Button 
            onClick={handleDeleteProject}
            variant="outlined"
            color="error"
          >Да</Controls.Button>
          <Controls.Button 
            onClick={close} 
            variant="outlined" 
            autoFocus
          >
            Нет
          </Controls.Button>
        </Grid>
      </Grid>
    </Dialog>
  );
};

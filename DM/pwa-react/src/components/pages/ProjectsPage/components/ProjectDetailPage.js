import { useNavigate, useParams } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { deleteProject, selectAllProjects } from "../../../../services/projectsSlice";
import { Button, DialogContentText, Toolbar } from "@mui/material";
import RecordsGrid from "../../RecordsPage/components/RecordsGrid";
import { BiArrowBack } from "react-icons/bi";
import { Controls } from "../../../controls/Controls";
import * as React from "react";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import Dialog from "@mui/material/Dialog";
import { useState } from "react";
import { deleteOrganization } from "../../../../services/organizationsSlice";
import { openSnackbar } from "../../../../services/snackbarSlice";

export const ProjectDetailPage = () => {
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { id } = useParams();
  const projects = useSelector(selectAllProjects);
  const project = projects.find(project => project.id === Number(id));
  const projectId = id;

  const goBack = () => {
    navigate(-1);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  function handleDeleteProject() {
    dispatch(deleteProject(id));
    dispatch(openSnackbar());
    navigate(`/projects`);
  }

  function handleToCreateTemplatePage() {
    navigate(`/template/create`);
  }

  function handleToCreateRecordPage() {
    navigate(`/record/create`, { state: { id: projectId } });
  }

  function handleToFilesPage() {
    navigate(`/project/${id}/files`);
  }

  function handleToProjectEditPage() {
    navigate(`/project/${id}/edit`);
  }

  function handleToIfc() {
    navigate(`/ifc`);
  }

  return (
    <div>
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
        <Controls.Button onClick={handleToProjectEditPage}>Edit project</Controls.Button>
        <Controls.Button color="error" onClick={handleClickOpen}>Delete project</Controls.Button>
        <Controls.Button onClick={handleToCreateRecordPage}>Add Record</Controls.Button>
        <Controls.Button onClick={handleToCreateTemplatePage}>Add Template</Controls.Button>
        <Controls.Button onClick={handleToFilesPage}>Files</Controls.Button>
        <Controls.Button onClick={handleToIfc}>ifc</Controls.Button>
      </Toolbar>
      <hr />
      <div style={{
        padding: 5,
        display: "flex",
        flexDirection: "column",
        justifyContent: "flex-start",
        flexWrap: "wrap"
      }}>
        <Controls.Input
          name="title"
          label="title"
          type="text"
          value={project.title}
          inputProps={{ readOnly: true }}
        />
        <Controls.TextArea
          name="description"
          label="description"
          type="text"
          value={project.description}
          inputProps={{ readOnly: true }}
          multiline
          rows={4}
        />
      </div>
      <div style={{
        marginTop: 10,
        display: "flex",
        flexDirection: "row"
      }}>
      </div>
      <RecordsGrid projectId={id} />
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {"You really delete this Project?"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDeleteProject} variant="outlined" color="error">Delete</Button>
          <Button onClick={handleClose} variant="outlined" autoFocus>
            Cancel
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};
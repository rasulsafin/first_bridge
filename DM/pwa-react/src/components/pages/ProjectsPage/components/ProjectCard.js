import React, { useEffect, useState } from "react";
import "./ProjectCard.css";
import { Controls } from "../../../controls/Controls";
import { Grid, IconButton, MenuItem } from "@mui/material";
import { ReactComponent as MoreIcon } from "../../../../assets/icons/more.svg";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";
import { ReactComponent as EditIcon } from "../../../../assets/icons/edit.svg";
import Menu from "@mui/material/Menu";
import { ProjectModal } from "./ProjectModal";
import { useDispatch, useSelector } from "react-redux";
import { fetchFiles, selectAllFiles } from "../../../../services/filesSlice";
import Dialog from "@mui/material/Dialog";
import { deleteProject } from "../../../../services/projectsSlice";
import { formatDate } from "../utils/formatDate";

export const ProjectCard = (project) => {
  const [anchorEl, setAnchorEl] = useState(null);
  const [openModal, setOpenModal] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);
  const dispatch = useDispatch();
  const isMenuOpen = Boolean(anchorEl);
  const files = useSelector(selectAllFiles);
  const projectId = project.project.id;

  const handleOpenModal = () => {
    setAnchorEl(null);
    setOpenModal(true);
  };

  useEffect(() => {
    dispatch(fetchFiles(projectId));
  }, [openModal]);

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  const handleOpenDialog = () => {
    setAnchorEl(null);
    setOpenDialog(true);
  };

  const handleMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  function handleDeleteProject() {
    dispatch(deleteProject(projectId));
  }

  const renderMenu = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: "top",
        horizontal: "right"
      }}
      keepMounted
      transformOrigin={{
        vertical: "top",
        horizontal: "right"
      }}
      open={isMenuOpen}
      onClose={handleMenuClose}
    >
      <MenuItem
        onClick={handleOpenModal}>
        <IconButton>
          <EditIcon />
        </IconButton>
        Редактировать
      </MenuItem>
      <MenuItem
        onClick={handleOpenDialog}>
        <IconButton>
          <TrashIcon />
        </IconButton>
        В архив
      </MenuItem>
    </Menu>
  );

  const deleteDialog = (
    <Dialog
      PaperProps={{ sx: { position: "fixed", bottom: 50, left: "35vw", maxWidth: "md", m: 0, padding: 2 } }}
      open={openDialog}
      onClose={handleCloseDialog}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
      hideBackdrop
    >
      <Grid container>
        <Grid item md={12}>
          <span>
           {`Вы действительно хотите удалить проект ${project.project.title} ?`}
          </span>
          <Controls.Button onClick={handleDeleteProject} variant="outlined" color="error">Да</Controls.Button>
          <Controls.Button onClick={handleCloseDialog} variant="outlined" autoFocus>
            Нет
          </Controls.Button>
        </Grid>
      </Grid>
    </Dialog>
  );

  return (
    <div className="project-card">
      <div className="project-title-container">
        <span className="project-title">{project.project.title}</span>
        <div>
          <IconButton
            onClick={handleMenuOpen}
          >
            <MoreIcon />
          </IconButton>
        </div>
        {renderMenu}
        {deleteDialog}
      </div>
      <ProjectModal
        files={files}
        project={project}
        open={openModal}
        onClose={handleCloseModal}
      />
      <span className="project-date">{formatDate(project.project.createdAt)}</span>
      <div className="users-in-project">
        <span className="quantity-users-text">Участников {
          project.project.users === null
            ? 0
            : project.project.users.length
        }</span>
      </div>
      <div className="btn-holder">
        <Controls.Button
          className="m-0"
          style={{
            width: "340px",
            height: "43px",
            backgroundColor: "#2D2926",
            color: "#FFF",
            border: "none"
          }}
        >Выбрать</Controls.Button>
      </div>
    </div>
  );
};

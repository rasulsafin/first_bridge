import React, { useState } from "react";
import "./ProjectCard.css";
import { Controls } from "../../../controls/Controls";
import { MenuItem, IconButton, Button, DialogContentText } from "@mui/material";
import { ReactComponent as MoreIcon } from "../../../../assets/icons/more.svg";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";
import { ReactComponent as EditIcon } from "../../../../assets/icons/edit.svg";
import Menu from "@mui/material/Menu";
import { ProjectModal } from "./ProjectModal";
import { useDispatch, useSelector } from "react-redux";
import { selectAllUsers } from "../../../../services/usersSlice";
import { selectAllFiles } from "../../../../services/filesSlice";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import Dialog from "@mui/material/Dialog";
import { deleteProject } from "../../../../services/projectsSlice";

export const ProjectCard = (project) => {
  const [anchorEl, setAnchorEl] = useState(null);
  const [openModal, setOpenModal] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);
  const dispatch = useDispatch();
  const isMenuOpen = Boolean(anchorEl);
  const users = useSelector(selectAllUsers);
  const files = useSelector(selectAllFiles);
  const projectId = project.project.id;
  const handleOpenModal = () => {
    setAnchorEl(null);
    setOpenModal(true);
  };

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
      PaperProps={{ sx: { position: "fixed", bottom: 50, left: "35vw", m: 0 } }}
      open={openDialog}
      onClose={handleCloseDialog}
      aria-labelledby="alert-dialog-title"
      aria-describedby="alert-dialog-description"
      hideBackdrop
    >
      <DialogContent>
        <DialogContentText id="alert-dialog-description">
          Вы действительно хотите удалить проект {project.project.title}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={handleDeleteProject} variant="outlined" color="error">Да</Button>
        <Button onClick={handleCloseDialog} variant="outlined" autoFocus>
          Нет
        </Button>
      </DialogActions>
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
        users={users}
        project={project}
        open={openModal}
        onClose={handleCloseModal}
      />
      <span className="project-date">12 ноября 2022</span>
      <div className="users-in-project">
        <span className="quantity-users-text">Участников {users.length}</span>
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

import React, { useState } from "react";
import { useDispatch } from "react-redux";
import {
  IconButton,
  MenuItem,
  Menu,
} from "@mui/material";
import { ReactComponent as MoreIcon } from "../../../../assets/icons/more.svg";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";
import { ReactComponent as EditIcon } from "../../../../assets/icons/edit.svg";
import { Controls } from "../../../controls/Controls";
import "./ProjectCard.css";
import { ProjectModal } from "./ProjectModal";
import { deleteProject } from "../../../../services/projectsSlice";
import { formatDate } from "../utils/formatDate";
import { reduceTitle } from "../utils/reduceTitle";
import { ProjectDeleteDialog } from "./ProjectDeleteDialog";

export const ProjectCard = (props) => {
  const { project } = props;
  const [anchorEl, setAnchorEl] = useState(null);
  const [openModal, setOpenModal] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);
  const dispatch = useDispatch();
  const isMenuOpen = Boolean(anchorEl);
  const projectId = project.id;

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
    setOpenDialog(false);
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

  return (
    <div className="project-card">
      <div className="project-title-container">
        <span className="project-title">
          {project.title.length > 20 ? reduceTitle(project.title) : project.title}
        </span>
        <div>
          <IconButton
            onClick={handleMenuOpen}
          >
            <MoreIcon />
          </IconButton>
        </div>
        {renderMenu}
        <ProjectDeleteDialog
          open={openDialog}
          close={handleCloseDialog}
        project={project}
        handleDeleteProject={handleDeleteProject}
        />
      </div>
      <ProjectModal
        project={project}
        open={openModal}
        onClose={handleCloseModal}
      />
      <span className="project-date">{formatDate(project.createdAt)}</span>
      <div className="users-in-project">
        <span className="quantity-users-text">Участников {
          project.users === null
            ? 0
            : project.users.length
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

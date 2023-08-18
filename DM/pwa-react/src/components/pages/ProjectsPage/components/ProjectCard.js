import React, { useCallback, useMemo, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { IconButton } from "@mui/material";
import { ReactComponent as MoreIcon } from "../../../../assets/icons/more.svg";
import { Controls } from "../../../controls/Controls";
import "./ProjectCard.css";
import { deleteProject, selectCurrentProject, setCurrentProject } from "../../../../services/projectsSlice";
import { reduceTitle } from "../utils/reduceTitle";
import { ProjectDeleteDialog } from "./ProjectDeleteDialog";
import { MenuProjectCard } from "./MenuProjectCard";
import { formatDate } from "../../../../utils/formatDate";
import { ProjectUpdateModal } from "./ProjectUpdateModal";

export const ProjectCard = ({ project }) => {
  const dispatch = useDispatch();
  const currentProject = useSelector(selectCurrentProject);
  const [anchorEl, setAnchorEl] = useState(null);
  const [openModal, setOpenModal] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);
  const projectId = useMemo(() => project.id, [project.id]);
  const isActiveButton = useMemo(() => projectId === currentProject, [projectId, currentProject]);
  const titleUsersInProject = useMemo(() =>
    `Участников ${project.users === null ? 0 : project.users.length}`, [project.users]);
  const titleButton = useMemo(() => isActiveButton ? "Выбран" : "Выбрать", [isActiveButton]);

  const handleOpenModal = useCallback(() => {
    setAnchorEl(null);
    setOpenModal(true);
  }, []);

  const handleCloseModal = useCallback(() => {
    setOpenModal(false);
  }, []);

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  const handleOpenDialog = useCallback(() => {
    setAnchorEl(null);
    setOpenDialog(true);
  }, [dispatch, projectId]);

  const handleMenuOpen = useCallback((event) => {
    setAnchorEl(event.currentTarget);
  }, []);

  const handleMenuClose = useCallback(() => {
    setAnchorEl(null);
  }, []);

  const handleDeleteProject = () => {
    dispatch(deleteProject(projectId));
    setOpenDialog(false);
  };

  const handleSelectedProject = useCallback(() => {
    dispatch(setCurrentProject(projectId));
  }, [dispatch, projectId]);

  const reducedTitle = useMemo(() =>
    project.title.length > 20 ? reduceTitle(project.title) : project.title, [project.title]);

  return (
    <div className="project-card">
      <div className="project-title-container">
        <span className="project-title">
          {reducedTitle}
        </span>
        <div>
          <IconButton
            onClick={handleMenuOpen}
          >
            <MoreIcon />
          </IconButton>
        </div>
        <MenuProjectCard
          anchorEl={anchorEl}
          open={Boolean(anchorEl)}
          handleOpenModal={handleOpenModal}
          onClose={handleMenuClose}
          handleOpenDialog={handleOpenDialog}
        />
        <ProjectDeleteDialog
          open={openDialog}
          close={handleCloseDialog}
          project={project}
          handleDeleteProject={handleDeleteProject}
        />
      </div>
      <ProjectUpdateModal
        project={project}
        open={openModal}
        onClose={handleCloseModal}
      />
      <span className="project-date">{formatDate(project.createdAt)}</span>
      <div className="users-in-project">
        <span className="quantity-users-text">{titleUsersInProject}
        </span>
      </div>
      <div className="btn-holder">
        <Controls.Button
          className="m-0"
          style={{
            width: "340px",
            height: "43px",
            backgroundColor: isActiveButton ? "#B3B3B3" : "#2D2926",
            color: "#FFF",
            border: "none"
          }}
          onClick={handleSelectedProject}
        >{titleButton}
        </Controls.Button>
      </div>
    </div>
  );
};

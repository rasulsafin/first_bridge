import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import "./Projects.css";
import {
  fetchProjects,
  selectAllProjects,
  selectCurrentProject
} from "../../../services/projectsSlice";
import { fetchUsers, selectAllUsers } from "../../../services/usersSlice";
import { ProjectCard } from "./components/ProjectCard";
import { ReactComponent as PlusIcon } from "../../../assets/icons/plus.svg";
import { SearchAndSortProjectToolbar } from "./components/SearchAndSortProjectToolbar";
import { useModal } from "../../../hooks/useModal";
import { ProjectCreateModal } from "./components/ProjectCreateModal";
import "../../layout/Layout.css";

export function Projects() {
  const [openModal, toggleModal] = useModal();
  const dispatch = useDispatch();
  const projects = useSelector(selectAllProjects);
  const users = useSelector(selectAllUsers);
  const currentProject = useSelector(selectCurrentProject);

  useEffect(() => {
    dispatch(fetchProjects());
    dispatch(fetchUsers());
  }, []);

  return (
    <div className="component-container">
      <div className="header-toolbar">
        <div className="header-title">Проекты</div>
        <SearchAndSortProjectToolbar />
      </div>
      <div className="card-container">
        {projects.map(project =>
          <ProjectCard
            key={project.id}
            project={project}
            currentProject={currentProject}
          />)}
        <div className="new-project-card">
          <button
            type="button"
            className="btn-add-project"
            onClick={toggleModal}
          >
            <PlusIcon />
          </button>
          <span className="label-add-project">Добавить проект</span>
        </div>
      </div>
      {openModal &&
        <ProjectCreateModal
          toggleModal={toggleModal}
          projects={projects}
          users={users}
        />
      }
    </div>
  );
}

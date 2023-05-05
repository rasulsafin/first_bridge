import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";
import { Box } from "@mui/material";
import "./Projects.css";
import {
  fetchProjects,
  selectAllProjects
} from "../../../services/projectsSlice";
import { fetchUsers } from "../../../services/usersSlice";
import { ProjectCard } from "./components/ProjectCard";
import { ReactComponent as PlusIcon } from "../../../assets/icons/plus.svg";
import { SearchAndSortProjectToolbar } from "./components/SearchAndSortProjectToolbar";

export function Projects() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const projects = useSelector(selectAllProjects);

  useEffect(() => {
    dispatch(fetchProjects());
    dispatch(fetchUsers());
  }, []);

  function handleToCreatePage() {
    navigate(`/project/create`);
  }

  return (
    <div className="component-container">
      <h3 className="mb-2">Проекты</h3>
      <Box>
        <SearchAndSortProjectToolbar />
      </Box>
      <div className="card-container">
        {projects.map(project => <ProjectCard key={project.id} project={project} />)}
        <div className="new-project-card">
          <button
            type="button"
            className="btn-add-project"
            onClick={handleToCreatePage}
          >
            <PlusIcon />
          </button>
          <span className="label-add-project">Добавить проект</span>
        </div>
      </div>
    </div>
  );
}

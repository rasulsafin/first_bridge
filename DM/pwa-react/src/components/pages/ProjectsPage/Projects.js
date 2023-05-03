import { useDispatch, useSelector } from "react-redux";
import {
  fetchProjects,
  selectAllProjects,
} from "../../../services/projectsSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { ProjectCard } from "./components/ProjectCard";
import * as React from "react";
import "./Projects.css";
import { fetchUsers } from "../../../services/usersSlice";
import { ReactComponent as PlusIcon } from "../../../assets/icons/plus.svg";
import { SearchAndSortProjectToolbar } from "./components/SearchAndSortProjectToolbar";
import { Box } from "@mui/material";

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

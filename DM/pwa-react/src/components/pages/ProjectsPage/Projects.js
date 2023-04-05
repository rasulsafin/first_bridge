import { useDispatch, useSelector } from "react-redux";
import { fetchProjects, searchByTitle, selectAllProjects } from "../../../services/projectsSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import SuccessSnackbar from "../../snackbar/SuccessSnackbar";
import { Controls } from "../../controls/Controls";
import { ProjectCard } from "./components/ProjectCard";
import * as React from "react";
import "./Projects.css";
import { fetchUsers, selectAllUsers } from "../../../services/usersSlice";
import { SearchBar } from "../../searchBar/SearchBar";
import { fetchFiles } from "../../../services/filesSlice";

export function Projects() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const projects = useSelector(selectAllProjects);
  const users = useSelector(selectAllUsers);

  useEffect(() => {
    dispatch(fetchProjects());
    dispatch(fetchUsers());
    dispatch(fetchFiles());
  }, []);
  
  function handleToCreatePage() {
    navigate(`/project/create`);
  }

  function filterByInput(e) {
    dispatch(searchByTitle(e.target.value));
  }

  return (
    <div className="component-container">
      <SuccessSnackbar />
      <h3 className="mb-2">Проекты</h3>
      <div className="toolbar-project">
        <SearchBar
          onChange={e => filterByInput(e)}
        />
        <div>
          <Controls.Button
            className="ml-0"
            style={{
              backgroundColor: "#2D2926",
              color: "#FFF",
              border: "none"
            }}
          >Сначала новые</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
          >Сначала старые</Controls.Button>
        </div>
      </div>
      <div className="card-container">
        {projects.map(project => <ProjectCard users={users} project={project} />)}
        <div className="new-project-card">
          <button
            className="btn-add-project"
            onClick={handleToCreatePage}
          >+
          </button>
          <span className="label-add-project">Добавить проект</span>
        </div>
      </div>
    </div>
  );
}

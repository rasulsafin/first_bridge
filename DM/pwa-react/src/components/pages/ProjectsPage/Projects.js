import { useDispatch, useSelector } from "react-redux";
import {
  fetchProjects,
  searchProjectsByTitle,
  selectAllProjects,
  sortProjectsByDateAsc,
  sortProjectsByDateDesc
} from "../../../services/projectsSlice";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { Controls } from "../../controls/Controls";
import { ProjectCard } from "./components/ProjectCard";
import * as React from "react";
import "./Projects.css";
import { fetchUsers } from "../../../services/usersSlice";
import { SearchBar } from "../../searchBar/SearchBar";
import { ReactComponent as PlusIcon } from "../../../assets/icons/plus.svg";

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

  function filterByInput(e) {
    dispatch(searchProjectsByTitle(e.target.value));
  }

  const handleSortByAsc = () => {
    dispatch(sortProjectsByDateDesc());
  };

  const handleSortByDesc = () => {
    dispatch(sortProjectsByDateAsc());
  };

  return (
    <div className="component-container">
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
            onClick={handleSortByAsc}
          >Сначала новые</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
            onClick={handleSortByDesc}
          >Сначала старые</Controls.Button>
        </div>
      </div>
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

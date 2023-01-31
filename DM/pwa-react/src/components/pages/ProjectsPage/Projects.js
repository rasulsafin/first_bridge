import { useDispatch } from "react-redux";
import { fetchProjects } from "../../../services/projectsSlice";
import { useEffect } from "react";
import { Toolbar } from "@mui/material";
import { useNavigate } from "react-router";
import ProjectsGrid from "./components/ProjectsGrid";
import SuccessSnackbar from "../../snackbar/SuccessSnackbar";
import { Controls } from "../../controls/Controls";

export function Projects() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  function handleToCreatePage() {
    navigate(`/project/create`);
  }

  return (
    <div className="component-container">
      <SuccessSnackbar />
      <h3 className="mb-2">Проекты</h3>
      <Toolbar>
        <Controls.Button
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
      </Toolbar>
      <hr />
      <ProjectsGrid />
    </div>
  );
}

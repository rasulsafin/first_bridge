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
    <div className="p-3">
      <SuccessSnackbar />
      <Toolbar>
        <Controls.Button onClick={handleToCreatePage}>Add Project</Controls.Button>
        <Controls.Button>Add Something</Controls.Button>
      </Toolbar>
      <hr />
      <h3 className="mb-2">Projects</h3>
      <ProjectsGrid />
    </div>
  );
}

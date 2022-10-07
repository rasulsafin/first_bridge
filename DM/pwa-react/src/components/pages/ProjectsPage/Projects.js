import { useDispatch, useSelector } from "react-redux";
import { fetchProjects } from "../../../services/projectsSlice";
import { useEffect } from "react";
import "./Projects.css";
import { Toolbar, Button } from "@mui/material";
import { useNavigate } from "react-router";
import ProjectsGrid from "./components/ProjectsGrid";
import { openSnackbar } from "../../../services/snackbarSlice";
import SuccessSnackbar from "../../snackbar/SuccessSnackbar";

export function Projects() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const snackbar = useSelector((state) => state.snackbar);


  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  function handleToCreatePage() {
    navigate(`/project/create`);
  }

  function handleSnackbar() {
    dispatch(openSnackbar());
  }

  return (
    <div className="p-3">
      <div>
        <SuccessSnackbar
        />
        <Toolbar>
        <Button className="ml-o m-3" size="small" variant="outlined" onClick={handleToCreatePage}>Add Project</Button>
        <Button className="m-3" size="small" variant="outlined">Add Something</Button>
        <Button className="m-3" size="small" variant="outlined" onClick={handleSnackbar}>Something else</Button>
        <Button className="m-3" size="small" variant="outlined">WAT</Button>
      </Toolbar></div>
      <hr />
      <h3 className="mb-2">Projects</h3>
      <ProjectsGrid />
    </div>
  );
}
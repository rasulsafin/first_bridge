import { Controls } from "../../../controls/Controls";
import { Toolbar } from "@mui/material";
import { useDispatch } from "react-redux";
import { addNewProject } from "../../../../services/projectsSlice";
import { useNavigate } from "react-router-dom";
import CreateProjectForm from "./ProjectForm";

export const ProjectCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  // TODO later remove from localStorage
  const organizationId = localStorage.getItem("organizationId");

  return (
    <div>
      <Toolbar>
        <Controls.Button onClick={goBack}>
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Create project</h3>
      <CreateProjectForm
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(addNewProject({
            title: values.title,
            description: values.description,
            organizationId: organizationId
          }));
          formikHelpers.resetForm();
          navigate(`/projects`);
        }} />
    </div>
  );
};
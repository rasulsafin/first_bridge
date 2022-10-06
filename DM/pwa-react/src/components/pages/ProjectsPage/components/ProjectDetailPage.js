import { useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { selectAllProjects } from "../../../../services/projectsSlice";
import { Button, Toolbar } from "@mui/material";
import RecordsGrid from "../../RecordsPage/components/RecordsGrid";
import { BiArrowBack } from "react-icons/bi";
import { ProjectEditPage } from "./ProjectEditPage";
import { Controls } from "../../../controls/Controls";
import * as React from "react";

export const ProjectDetailPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const projects = useSelector(selectAllProjects);
  const project = projects.find(project => project.id === Number(id));
  const projectId = id;
  
  const goBack = () => {
    navigate(-1);
  };

  function handleToCreateTemplatePage() {
    navigate(`/template/create`);
  }

  function handleToCreateRecordPage() {
    navigate(`/record/create`, {state: {id: projectId}});
  }

  function handleToFilesPage() {
    navigate(`/project/${id}/files`);
  }

  function handleToProjectEditPage() {
    navigate(`/project/${id}/edit`);
  }

  return (
    <div className="p-3">
      <div>
        <Toolbar>
          <Button className="ml-o m-3" onClick={goBack} size="small" variant="outlined">
            <BiArrowBack size={24} color="#1d62ad" /></Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToProjectEditPage}>Edit
            project</Button>
          <Button className="m-3" size="small" variant="outlined" color="error">Delete project</Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToCreateRecordPage}>Add Record</Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToCreateTemplatePage}>Add
            Template</Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToFilesPage}>Files</Button>
        </Toolbar>
      </div>
      <hr />
      <div style={{
        padding: 5,
        display: "flex",
        flexDirection: "column",
        justifyContent: "flex-start",
        flexWrap: "wrap"
      }}>
        <Controls.Input
          name="title"
          label="title"
          type="text"
          value={project.title}
          inputProps={{ readOnly: true }}
        />
        <Controls.TextArea
          name="description"
          label="description"
          type="text"
          value={project.description}
          inputProps={{ readOnly: true }}
          multiline
          rows={4}
        />
      </div>
      <div style={{
        marginTop: 10,
        display: "flex",
        flexDirection: "row"
      }}>
        <p><span
          style={{
            fontSize: 24,
            paddingRight: 15
          }}>Records:</span></p>
      </div>
      <RecordsGrid projectId={id} />
    </div>
  );
};
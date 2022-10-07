import { useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { selectAllProjects } from "../../../../services/projectsSlice";
import { Toolbar } from "@mui/material";
import RecordsGrid from "../../RecordsPage/components/RecordsGrid";
import { BiArrowBack } from "react-icons/bi";
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
    navigate(`/record/create`, { state: { id: projectId } });
  }

  function handleToFilesPage() {
    navigate(`/project/${id}/files`);
  }

  function handleToProjectEditPage() {
    navigate(`/project/${id}/edit`);
  }

  return (
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
        <Controls.Button onClick={handleToProjectEditPage}>Edit project</Controls.Button>
        <Controls.Button color="error">Delete project</Controls.Button>
        <Controls.Button onClick={handleToCreateRecordPage}>Add Record</Controls.Button>
        <Controls.Button onClick={handleToCreateTemplatePage}>Add Template</Controls.Button>
        <Controls.Button onClick={handleToFilesPage}>Files</Controls.Button>
      </Toolbar>
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
      </div>
      <RecordsGrid projectId={id} />
    </div>
  );
};
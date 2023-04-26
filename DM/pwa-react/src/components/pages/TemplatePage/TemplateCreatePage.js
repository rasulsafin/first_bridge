import * as React from "react";
import { Controls } from "../../controls/Controls";
import { useSelector } from "react-redux";
import { selectAllProjects } from "../../../services/projectsSlice";
import { Toolbar } from "@mui/material";
import { useNavigate } from "react-router";
import { TemplateCreateForm } from "./TemplateCreateForm";

export function TemplateCreatePage() {
  const navigate = useNavigate();
  const projects = useSelector(selectAllProjects);

  const goBack = () => {
    navigate(-1);
  };

  return (
    <div>
      <Toolbar>
        <Controls.Button onClick={goBack}>
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Create template</h3>
      <div style={{
        display: "flex",
        flexDirection: "column"
      }}>
        <TemplateCreateForm />
      </div>
    </div>
  );
}
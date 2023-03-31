import React from "react";
import "./ProjectCard.css";
import { Controls } from "../../../controls/Controls";

export const ProjectCard = (project) => {

  return (
    <div className="project-card">
      <span className="project-title">{project.project.title}</span>
      <p><span className="project-date">12 ноября 2022</span></p>
      <div className="users-in-project">
        <span className="quantity-users-text">Участников 1234</span></div>
      <div className="btn-holder">
        <Controls.Button
          className="m-0"
          style={{
            width: "340px",
            height: "43px",
            backgroundColor: "#2D2926",
            color: "#FFF",
            border: "none"
          }}
        >Выбрать</Controls.Button>
      </div>
    </div>
  );
};

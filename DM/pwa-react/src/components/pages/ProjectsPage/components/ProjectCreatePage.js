import { Controls } from "../../../controls/Controls";
import { Button, Toolbar } from "@mui/material";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { addNewProject } from "../../../../services/projectsSlice";
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi";

export const ProjectCreatePage = () => {
  const dispatch = useDispatch();
  const [title, setTitle] = useState("");
  const [desc, setDesc] = useState("");
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  const organizationId = localStorage.getItem("organizationId");

  function createProject() {
    if (title !== "" && desc !== "") {
      dispatch(addNewProject({
        title: title,
        description: desc,
        organizationId: organizationId

      }));
      setTitle("");
      setDesc("");
    } else {
      alert("fill out a form");
    }
  }

  return (
    <div  className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
      </Toolbar>
      <hr />
      <div style={{
        display: "flex",
        flexDirection: "column"
      }}>
        <h3>Create project</h3>
        <Controls.Input
          name="title"
          label="title"
          type="text"
          onChange={(event) => setTitle(event.target.value)}
          required
        />
        <Controls.Input
          name="description"
          label="description"
          type="text"
          onChange={(event) => setDesc(event.target.value)}
          required
        />
      </div>
      <Controls.Button
        onClick={createProject}>
        Add Project
      </Controls.Button>
    </div>
  );
};
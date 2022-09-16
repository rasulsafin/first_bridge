import { Controls } from "../../../controls/Controls";
import { Button } from "@mui/material";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { addNewProject } from "../../../../services/projectsSlice";

export const ProjectCreatePage = () => {
  const dispatch = useDispatch();
  const [title, setTitle] = useState(""); 
  const [desc, setDesc] = useState("");

  const organizationId = localStorage.getItem("organizationId");

  function createProject() {
    if (title !== "" && desc !== "")
    {
      dispatch(addNewProject({
        title: title,
        description: desc,
        organizationId: organizationId,

      }))
      setTitle("");
      setDesc("");
    }
    else
    {
     alert("fill out a form") 
    }
  }
  
  return (
    <div>
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
      <Button 
        className="m-3" 
        size="small" 
        variant="outlined" 
        onClick={createProject}>
        Add Project
      </Button>
    </div>
    
  )
}
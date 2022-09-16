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
    console.log(title)
    dispatch(addNewProject({
      title: title,
      description: desc,
      organizationId: organizationId,

    }))
    setTitle("");
    setDesc("");
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
        />
        <Controls.Input
          name="description"
          label="description"
          type="text"
          onChange={(event) => setDesc(event.target.value)}
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
import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { Controls } from "../../../../controls/Controls";
import {useEffect, useState} from "react";
import { useDispatch, useSelector } from "react-redux";
import { addNewProject, fetchProjects } from "../../../../../services/projectsSlice";
import { selectAllUsers } from "../../../../../services/usersSlice";
import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";

const initialProject = {
  title: "",
};

const initialUser = {
  id: 0,
  name: ""
}

export function AddProjectModal() {
  const dispatch = useDispatch();
  const [open, setOpen] = React.useState(false);
  const [title, setTitle] = useState(initialProject);
  const [userProject, setUserProject] = useState(initialUser);
  
  function createProject() {
    console.log(title)
    dispatch(addNewProject({
      title: title,
      
    }))
    setOpen(false);
    setTitle(initialProject);
    dispatch(fetchProjects())
  }

  const users = useSelector(selectAllUsers)
  console.log(users)
  
  const usersOptions = users.map(user => (
  <MenuItem 
    key={user.id} 
    value={user.id}
  >{user.name}
  </MenuItem>
  ))

  const handleChange = event => {
    console.log(event.target.value);
    setUserProject(event.target.value);
  };
  
  useEffect(() => {
    handleClose()
  }, []);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Button variant="contained" onClick={handleClickOpen}>
        Add Project
      </Button>
      <Dialog maxWidth="sm" open={open} onClose={handleClose}>
        <DialogTitle>Create project</DialogTitle>
        <DialogContent dividers>
          <div
            className="modalContainer"
            style={{
              width: "75%",
              margin: "5px"
            }}>
            <Controls.Input
              name="name"
              label="Name"
              type="text"
              onChange={(e) => setTitle(e.target.value)}
              required
            />
            <Controls.Select
              name="User"
              label="User"
              value={userProject}
              onChange={handleChange}
              options={users}
              autoWidth={false}
              fullwidth
            />
            {/*<FormControl size="small">*/}
            {/*  <InputLabel id="demo-simple-select-label">User</InputLabel>*/}
            {/*  <Select*/}
            {/*    labelId="demo-simple-select-label"*/}
            {/*    id="demo-simple-select"*/}
            {/*    value={userProject}*/}
            {/*    label="User"*/}
            {/*    onChange={handleChange}*/}
            {/*    sx={{*/}
            {/*      maxHeight: '100px',*/}
            {/*      width: '250px'*/}
            {/*    }}*/}
            {/*  >*/}
            {/*    {usersOptions}*/}
            {/*  </Select>*/}
            {/*</FormControl>*/}
          </div>
        </DialogContent>
        <DialogActions sx={{ m: "20px" }}>
          <Button
            sx={{
              backgroundColor: "crimson",
              mr: "20px"
            }}
            variant="contained"
            onClick={handleClose}>Cancel</Button>
          <Button
            variant="contained"
            type="submit"
            form="modalForm"
            onClick={createProject}
          >Create</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

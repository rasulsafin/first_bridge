import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { Controls } from "../../../../controls/Controls";
import {useEffect, useState} from "react";
import { useDispatch, useSelector } from "react-redux";
import {addNewUser} from "../../../../../services/usersSlice";
import { fetchRecordTemplates, selectAllRecordTemplates } from "../../../../../services/recordTemplatesSlice";
import { selectAllProjects } from "../../../../../services/projectsSlice";

const initialValues = {
  name: "",
  login: "",
  email: "",
  password: ""
};

const initialProject = {
  id: 0,
  title: ""
};

export function AddRecordModal() {
  const dispatch = useDispatch();
  const [open, setOpen] = React.useState(false);
  const [values, setValues] = useState(initialValues);
  const [template, setTemplate] = useState();
  const [project, setProject] = useState(initialProject);

  const projects = useSelector(selectAllProjects);
  
  // const handleInputChange = (e) => {
  //   const { name, value } = e.target;
  //   setValues({
  //     ...values,
  //     [name]: value
  //   });
  //   console.log(values.name);
  //
  // };

  function createRecord() {
    dispatch(addNewUser({
      name: values.name,
      login: values.login,
      email: values.email,
      password: values.password
    }))
    setOpen(false);
    setValues(initialValues);
    dispatch(fetchRecordTemplates())
  }
  
  useEffect( () => {
    dispatch(fetchRecordTemplates(project))
  }, [dispatch]);
  
  const templates = useSelector(selectAllRecordTemplates)
  
  useEffect(() => {
    handleClose()
  }, []);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  // const templateForm = templates.find(t => t.id === Number(template));
  
  return (
    <div>
      <Button variant="contained" onClick={handleClickOpen}>
        Add record
      </Button>
      <Dialog maxWidth="md" open={open} onClose={handleClose}>
        <DialogTitle>Create record</DialogTitle>
        <DialogContent dividers>
          <div
            style={{
              display: "flex",
              justifyContent: "space-evenly"
            }}>
          <div
            className="modalContainer"
            style={{
              margin: "5px",
              padding: "5px",
              height: "70%",
              width: "calc(50% - 20px)"
            }}>
            <Controls.SelectProject
              name="project"
              label="project"
              value={project}
              onChange={(event) => setProject(event.target.value)}
              options={projects}
              autoWidth={false}
              style={{
                margin: "10px"
              }}
            />
            <Controls.SelectTemplate
              name="template"
              label="template"
              value={template}
              onChange={(event) => setTemplate(event.target.value)}
              options={templates}
              autoWidth={false}
              style={{
                margin: "10px"
              }}
            />
          </div>
          <div
            className="modalContainer"
            style={{
              margin: "5px",
              padding: "5px",
              height: "70%",
              width: "calc(50% - 20px)"
            }}>
            {/*{templateForm && templateForm.recordTemplate.map(item =>*/}
            {/*  <Controls.Input*/}
            {/*    name={item.title}*/}
            {/*    label={item.title}*/}
            {/*    type={item.type}*/}
            {/*  />)}*/}
          </div>
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
            onClick={createRecord}
          >Create</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

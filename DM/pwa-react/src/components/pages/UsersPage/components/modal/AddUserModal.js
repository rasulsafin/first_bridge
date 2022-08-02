import * as React from "react";
import Button from "@mui/material/Button";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import { Controls } from "../../../../controls/Controls";
import "./AddUserModal.css";
import { axiosInstance } from "../../../../../axios/axiosInstance";
import { useState } from "react";

const initialValues = {
  name: "",
  login: "",
  email: "",
  password: ""
};

export function AddUserModal() {
  const [open, setOpen] = React.useState(false);
  const [values, setValues] = useState(initialValues);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value
    });
  };

  function createUser(e) {
    e.preventDefault();
    axiosInstance.post("api/users",
      {
        name: e.target.value,
        login: e.target.value,
        email: e.target.value,
        password: e.target.value
      }).then(r => console.log(r));
  }

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Button variant="contained" onClick={handleClickOpen}>
        Add User
      </Button>
      <Dialog maxWidth="sm" open={open} onClose={handleClose}>
        <DialogTitle>Create user</DialogTitle>
        <DialogContent dividers>
          <div
            id="modalForm"
            onSubmit={createUser}
            className="modalContainer"
               style={{
                 width: "75%",
                 margin: "5px"
               }}>
            <Controls.Input
              name="name"
              label="Name"
              type="text"
              value={values.name}
              onChange={handleInputChange}
              required
            />
            <Controls.Input
              name="login"
              label="Login"
              type="text"
              value={values.login}
              onChange={handleInputChange}
              required
            />
            <Controls.Input
              name="email"
              label="Email"
              type="email"
              value={values.email}
              onChange={handleInputChange}
              required
            />
            <Controls.Input
              name="password"
              label="Password"
              type="text"
              value={values.password}
              onChange={handleInputChange}
              required
            />
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
            // onClick={createUser}
          >Create</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

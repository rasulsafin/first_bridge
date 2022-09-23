import { useNavigate, useParams } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { deleteUser, selectAllUsers } from "../../../../services/usersSlice";
import * as React from "react";
import { Controls } from "../../../controls/Controls";
import { useState } from "react";
import ProjectsPermissionsGrid from "../../AdminPage/ProjectsPermissionsGrid";
import { Button, DialogContentText, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
// import RecordsPermissionsGrid from "../../AdminPage/RecordsPermissionsGrid";

export const UserDetailPage = () => {
  const [open, setOpen] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();
  const users = useSelector(selectAllUsers);
  const [type, setType] = useState();

  const user = users.find(user => user.id === Number(id));

  const objectTypes = [
    { id: 1, name: "project" },
    { id: 2, name: "record" },
    { id: 3, name: "item" }
  ];

  const goBack = () => {
    navigate(-1);
  };

  const handleChange = (event) => {
    setType(event.target.value);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };
  
  const handleClose = () => {
    setOpen(false);
  };

  const objects = () => {
    switch (type) {
      case "project":
        return <ProjectsPermissionsGrid userId={user.id} type={1} />;
      case "record":
        return <tr>Record</tr>; //<RecordsPermissionsGrid />;
      case "item":
        return <tr>Item</tr>;
      default:
        return;
    }
  };

  function handleDeleteUser() {
    dispatch(deleteUser(id));
    navigate(`/users`);
  }

  function handleToEditPage() {
    navigate(`/users`);
  }

  return (
    <div className="p-3">
      <Toolbar>
        <Button className="m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Button>
        <Button className="m-3" size="small" variant="outlined"  onClick={handleToEditPage}>Edit user</Button>
        <Button className="m-3" size="small" variant="outlined" color="error" onClick={handleClickOpen}>Delete user</Button>
      </Toolbar>
      <hr />
      <h3>User Detail Page</h3>

      <div>
        <div style={{
          border: "black solid 2px",
          width: "40vh",
          background: "whitesmoke",
          padding: 5,
          marginBottom: 30
        }}>
          <p>Name: <span style={{ fontSize: 24 }}> {user.name}</span></p>
          <p>Login: <span style={{ fontSize: 24 }}> {user.login}</span></p>
          <p>Role: <span style={{ fontSize: 24 }}> {user.roles}</span></p>
          <p>Email: <span style={{ fontSize: 24 }}> {user.email}</span></p>
        </div>
        <Dialog
          open={open}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
        >
          <DialogTitle id="alert-dialog-title">
            {"You really delete this User?"}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              Are you sure?
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} variant="outlined" color="error">Delete</Button>
            <Button onClick={handleClose} variant="outlined" autoFocus>
              Cancel
            </Button>
          </DialogActions>
        </Dialog>
        <Dialog
          open={open}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
        >
          <DialogTitle id="alert-dialog-title">
            {"You really delete this User?"}
          </DialogTitle>
          <DialogContent>
            <DialogContentText id="alert-dialog-description">
              Are you sure?
            </DialogContentText>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleDeleteUser} variant="outlined" color="error">Delete</Button>
            <Button onClick={handleClose} variant="outlined" autoFocus>
              Cancel
            </Button>
          </DialogActions>
        </Dialog>
      </div>
      <div>
        <h3>Permissions</h3>
      </div>
      <div>
        <Controls.Select
          name="type"
          label="type"
          options={objectTypes}
          onChange={(event) => handleChange(event)}
          required
        />
      </div>
      <div>
        {objects()}
      </div>
    </div>
  );
};
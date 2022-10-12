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
import { PermissionTable } from "../../AdminPage/PermissionTable";
import { openSnackbar } from "../../../../services/snackbarSlice";
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
    dispatch(openSnackbar());
    navigate(`/users`);
  }

  function handleToEditPage() {
    navigate(`/user/${id}/edit`);
  }

  return (
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
        <Controls.Button onClick={handleToEditPage}>Edit user</Controls.Button>
        <Controls.Button color="error" onClick={handleClickOpen}>Delete user</Controls.Button>
      </Toolbar>
      <hr />
      <h3>User Detail Page</h3>
      <div>
          <div className="col-10" style={{
            display: "flex",
            justifyContent: "flex-start",
            alignItems: "center",
            flexWrap: "wrap"
          }}>
            <Controls.Input
              name="name"
              label="Name"
              type="text"
              value={user.name}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="lastName"
              label="lastName"
              type="text"
              value={user.lastName}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="fathersName"
              label="fathersName"
              type="text"
              value={user.fathersName}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="login"
              label="Login"
              type="text"
              value={user.login}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="email"
              label="Email"
              type="email"
              value={user.email}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="password"
              label="Password"
              type="text"
              value={user.password}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="roles"
              label="roles"
              type="text"
              value={user.roles}
              inputProps={{ readOnly: true }}
            />
            <Controls.DatePicker
              name="birthdate"
              label="birthdate"
              value={user.birthdate}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="snils"
              label="snils"
              type="text"
              value={user.snils}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="position"
              label="position"
              type="text"
              value={user.position}
              inputProps={{ readOnly: true }}
            />
            <Controls.Input
              name="organizationId"
              label="organizationId"
              type="text"
              value={user.organizationId}
              inputProps={{ readOnly: true }}
            />
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
      {/*<div>*/}
      {/*  <Controls.Select*/}
      {/*    name="type"*/}
      {/*    label="type"*/}
      {/*    options={objectTypes}*/}
      {/*    onChange={(event) => handleChange(event)}*/}
      {/*    required*/}
      {/*  />*/}
      {/*</div>*/}
      {/*<div>*/}
      {/*  {objects()}*/}
      {/*</div>*/}
      <PermissionTable />
    </div>
  );
};
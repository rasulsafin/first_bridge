import { useNavigate, useParams } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { deleteOrganization, selectAllOrganizations } from "../../../../services/organizationsSlice";
import { Button, DialogContentText, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import Dialog from "@mui/material/Dialog";
import { useState } from "react";
import { openSnackbar } from "../../../../services/snackbarSlice";

export const OrganizationDetailPage = () => {
  const [open, setOpen] = useState(false);
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { id } = useParams();
  const organizations = useSelector(selectAllOrganizations);

  const organization = organizations.find(org => org.id === Number(id));

  const goBack = () => {
    navigate(-1);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };
  
  function handleDeleteOrganization() {
    dispatch(deleteOrganization(id));
    dispatch(openSnackbar());
    navigate(`/organizations`);
  }
  
  return (
    <div className="p-3">
      <Toolbar>
        <Button className="m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Button>
        <Button className="m-3" size="small" variant="outlined">Edit organization</Button>
        <Button className="m-3" size="small" variant="outlined" color="error" onClick={handleClickOpen}>Delete organization</Button>
      </Toolbar>
      <hr />
      <h3>Organization Detail Page</h3>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {organization.name}</span></p>
      </div>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {"You really delete this Organization?"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDeleteOrganization} variant="outlined" color="error">Delete</Button>
          <Button onClick={handleClose} variant="outlined" autoFocus>
            Cancel
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};
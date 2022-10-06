import { useNavigate, useParams } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { deleteRecord, selectAllRecords } from "../../../../services/recordsSlice";
import { Button, DialogContentText, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";
import { Controls } from "../../../controls/Controls";
import { useState } from "react";
import Dialog from "@mui/material/Dialog";
import DialogTitle from "@mui/material/DialogTitle";
import DialogContent from "@mui/material/DialogContent";
import DialogActions from "@mui/material/DialogActions";
import { openSnackbar } from "../../../../services/snackbarSlice";
import SuccessSnackbar from "../../../snackbar/SuccessSnackbar";

export const RecordDetailPage = () => {
  const [open, setOpen] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { id } = useParams();
  const records = useSelector(selectAllRecords);

  const goBack = () => {
    navigate(-1);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const record = records.find(record => record.id === Number(id));

  const fieldsObj = record.fields;

  const fields = Object.entries(fieldsObj);

  function handleToRecordEditPage() {
    navigate(`/record/${id}/edit`);
  }

  function handleDeleteRecord() {
    dispatch(deleteRecord(id));
    dispatch(openSnackbar());
    navigate(`/projects`);
  }

  return (
    <div className="p-3">
      <SuccessSnackbar />
      <Toolbar>
        <Button className="ml-o m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
        <Button className="m-3" size="small" variant="outlined" onClick={handleToRecordEditPage}>Edit record</Button>
        <Button className="m-3" size="small" variant="outlined" color="error" onClick={handleClickOpen}>Delete
          record</Button>
      </Toolbar>
      <hr />
      <h3>Record Detail Page</h3>
      <div>
        <Controls.Input
          label="Name"
          type="text"
          value={record.name}
          inputProps={{ readOnly: true }}
        />
        <div>
          {fields.map(([key, value]) => {
            return (
              <div>
                <Controls.Input
                  label={key}
                  type="text"
                  value={value}
                  inputProps={{ readOnly: true }}
                />
              </div>
            );
          })}
        </div>
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
          {"You really delete this Record?"}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDeleteRecord} variant="outlined" color="error">Delete</Button>
          <Button onClick={handleClose} variant="outlined" autoFocus>
            Cancel
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};
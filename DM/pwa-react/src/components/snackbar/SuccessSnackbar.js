import { useDispatch, useSelector } from "react-redux";
import Snackbar from '@mui/material/Snackbar';
import { forwardRef } from "react";
import MuiAlert from "@mui/material/Alert";
import { closeSnackbar } from "../../services/snackbarSlice";

const Alert = forwardRef(function Alert(
  props,
  ref,
) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

export default function SuccessSnackbar() {
  const dispatch = useDispatch();
  const snackbar = useSelector((state) => state.snackbar);

  function handleClose() {
    dispatch(closeSnackbar());
  }

  return (
  <Snackbar
    anchorOrigin={{
      vertical: "bottom",
      horizontal: "right"
    }}
    open={snackbar.snackbar}
    autoHideDuration={2000}
    onClose={handleClose}
  >
    <Alert severity="success">
      This is a success message!
    </Alert>
  </Snackbar>
  );
}
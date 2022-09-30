import { createSlice } from "@reduxjs/toolkit";

// export enum AlertType {
//   Info = "info",
//   Success = "success",
//   Error = "error",
//   Warning = "warning"
// }

const initialState = {
  snackbar: false
}

export const snackbarSlice = createSlice(
  {
    name: "snackbar",
    initialState,
    reducers: {
     openSnackbar: state => {
       state.snackbar = !state.snackbar
     },
      closeSnackbar: state => {
        state.snackbar = false
      }
    },
    
  }
);

export const { openSnackbar, closeSnackbar } = snackbarSlice.actions;

export default snackbarSlice.reducer;
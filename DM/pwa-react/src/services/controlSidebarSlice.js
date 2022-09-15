import { createSlice } from "@reduxjs/toolkit";

const initialState = {
 open: true
};

export const controlSidebarSlice = createSlice({
  name: "controlSidebar",
  initialState,
  reducers: {
    setOpen(state) {
      state.open = !state.open;
    }
  }
});

export const { setOpen } = controlSidebarSlice.actions;

export const controlSidebarReducer = controlSidebarSlice.reducer;

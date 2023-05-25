import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isUserModalOpen: false
};

export const controlUISlice = createSlice({
  name: "controlUI",
  initialState,
  reducers: {
    toggleUserModal(state) {
      state.isUserModalOpen = !state.isUserModalOpen;
    }
  }
});

export const { toggleUserModal } = controlUISlice.actions;

export const selectUserModalOpen = (state) => state.controlUI.isUserModalOpen;

export const controlUIReducer = controlUISlice.reducer;
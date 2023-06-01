import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  isModalOpen: false,
  isDrawerOpen: false
};

export const controlUISlice = createSlice({
  name: "controlUI",
  initialState,
  reducers: {
    toggleModal(state) {
      state.isUserModalOpen = !state.isUserModalOpen;
    },
    toggleDrawer(state) {
      state.isDrawerOpen = !state.isDrawerOpen;
    },
    openDrawer(state) {
      state.isDrawerOpen = true;
    }
  }
});

export const { toggleDrawer, openDrawer } = controlUISlice.actions;

export const isModalOpen = (state) => state.controlUI.isModalOpen;
export const isDrawerOpenFromStore = (state) => state.controlUI.isDrawerOpen;

export const controlUIReducer = controlUISlice.reducer;
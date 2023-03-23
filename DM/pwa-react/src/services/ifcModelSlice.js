import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  modelStore: {},
  viewer: {},
  selectedElement: {},
};

export const ifcModelSlice = createSlice({
  name: "ifcModel",
  initialState,
  reducers: {
    setIfcModel(state, action) {
      state.modelStore = action.payload;
    },
    setViewerInstance(state, action) {
      state.viewer = action.payload;
    },
    setElement(state, action) {
      state.selectedElement = action.payload;
    }
  }
});

export const { setIfcModel } = ifcModelSlice.actions;
export const { setViewerInstance } = ifcModelSlice.actions;
export const { setElement } = ifcModelSlice.actions;

export const selectIfcModel = (state) => state.ifcModel.modelStore;
export const selectViewerInstance = (state) => state.ifcModel.viewer;
export const selectElement = (state) => state.ifcModel.selectedElement;

export default ifcModelSlice.reducer;

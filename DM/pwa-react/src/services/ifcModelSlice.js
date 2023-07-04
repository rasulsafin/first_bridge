import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  modelStore: {},
  viewer: {},
  selectedElement: {},
  rootElt: {}
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
    },
    setRootElt(state, action) {
      state.rootElt = action.payload;
    }
  }
});

export const { setIfcModel } = ifcModelSlice.actions;
export const { setViewerInstance } = ifcModelSlice.actions;
export const { setElement, setRootElt } = ifcModelSlice.actions;

export const selectIfcModel = (state) => state.ifcModel.modelStore;
export const selectViewerInstance = (state) => state.ifcModel.viewer;
export const selectElement = (state) => state.ifcModel.selectedElement;
export const selectRootElt = (state) => state.ifcModel.rootElt;

export default ifcModelSlice.reducer;

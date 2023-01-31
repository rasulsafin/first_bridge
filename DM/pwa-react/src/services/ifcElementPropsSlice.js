import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  guid: null,
  name: null,
  expressId: [],
  fileName: null,
  isElement: false,
  };

export const ifcElementPropsSlice = createSlice({
  name: "ifcElementProps",
  initialState,
  reducers: {
    setIfcElementProps(state, action) {
      state.guid = action.payload.guid;
      state.name = action.payload.name;
      state.expressId = [action.payload.expressId];
      state.fileName = action.payload.fileName;
      state.isElement = action.payload.isElement;
    }
  }
});

export const { setIfcElementProps } = ifcElementPropsSlice.actions;

export const selectGuid = (state) => state.ifcElementProps.guid;
export const selectExpressId = (state) => state.ifcElementProps.expressId;
export const selectIfcElementProps = (state) => state.ifcElementProps;

export const ifcElementPropsReducer = ifcElementPropsSlice.reducer;

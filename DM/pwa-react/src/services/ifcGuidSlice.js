import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  guid: null,
  };

export const ifcGuidSlice = createSlice({
  name: "ifcGuid",
  initialState,
  reducers: {
    setIfcGuid(state, action) {
      state.guid = action.payload;
    }
  }
});

export const { setIfcGuid } = ifcGuidSlice.actions;

export const selectGuid = (state) => state.ifcGuid.guid;

export const ifcGuidReducer = ifcGuidSlice.reducer;

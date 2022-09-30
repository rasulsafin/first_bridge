import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchRecordTemplates = createAsyncThunk(
  "recordTemplates/fetchRecordTemplates", async (projectId) => {
    const response = await axiosInstance.get("api/template", {
      params: {
        projectId: projectId
      }
    });
    console.log(response.data);
    return response.data;
  });

export const addNewTemplate = createAsyncThunk(
  "recordTemplates/addNewTemplate", async (newTemplate) => {
    const response = await axiosInstance.post("api/template", newTemplate);
    return response.data;
  });

export const recordTemplatesSlice = createSlice({
  name: "recordTemplates",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchRecordTemplates.fulfilled, (state, action) => {
      return action.payload;
    });
  }
});
 
export const selectAllRecordTemplates = (state) => state.recordTemplates;

export default recordTemplatesSlice.reducer;
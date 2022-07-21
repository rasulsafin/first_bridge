import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchProjects = createAsyncThunk("projects/fetchProjects", async () => {
  const response = await axiosInstance.get("api/project");
  console.log(response.data);
  return response.data;
});

export const projectsSlice = createSlice({
  name: "projects",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchProjects.fulfilled, (state, action) => {
      return action.payload;
    });
  }
});

export const selectAllProjects = (state) => state.projects;

export default projectsSlice.reducer;
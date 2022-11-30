import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchProjects = createAsyncThunk(
  "projects/fetchProjects", async () => {
    const response = await axiosInstance.get("api/project");
    return response.data;
  });

export const addNewProject = createAsyncThunk(
  "projects/addNewProject", async (newProject) => {
    const response = await axiosInstance.post("api/project", newProject);
    return response.data;
  });

export const deleteProject = createAsyncThunk(
  "projects/deleteProject", async (id) => {
    await axiosInstance.delete("api/project", {
      params: {
        projectId: id
      }
    }).then(() => console.log("Delete successfully"));
  }
)

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
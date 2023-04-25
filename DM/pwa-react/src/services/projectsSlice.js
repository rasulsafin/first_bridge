import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  projects: [],
  filteredProjects: [],
  isLoading: true,
  error: null
};

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
  });

export const addUserListToProject = createAsyncThunk(
  "projects/addProjectListToUser", async (data, thunkAPI) => {
    await axiosInstance.post("api/project/addUserListToProject", data);
    thunkAPI.dispatch(fetchProjects());
  });

export const deleteUserFromProject = createAsyncThunk(
  "projects/deleteUserFromProject", async ({userId, projectId}, thunkAPI) => {
    await axiosInstance.delete("api/project/deleteUserFromProject", {
      params: {
        userId: userId,
        projectId: projectId
      }
    });
    thunkAPI.dispatch(fetchProjects());
  });

export const projectsSlice = createSlice({
  name: "projects",
  initialState,
  reducers: {
    searchProjectsByTitle: (state, action) => {
      state.projects = state.filteredProjects
        .filter(project => project.title.toLowerCase().includes(action.payload.toLowerCase().trim()));
    },
    sortProjectsByDateAsc: (state) => {
      state.projects = state.projects
        .sort((a, b) => new Date(a.createdAt) < new Date(b.createdAt) ? -1 : 1);
    },
    sortProjectsByDateDesc: (state) => {
      state.projects = state.projects
        .sort((a, b) => new Date(b.createdAt) < new Date(a.createdAt) ? -1 : 1);
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchProjects.pending, (state) => {
      state.isLoading = true;
    });
    builder.addCase(fetchProjects.fulfilled, (state, action) => {
      state.isLoading = false;
      state.projects = action.payload;
      state.filteredProjects = action.payload;
    });
    builder.addCase(fetchProjects.rejected, (state, action) => {
      state.isLoading = false;
      state.error = action.error.message;
    });
  }
});

export const { searchProjectsByTitle, sortProjectsByDateAsc, sortProjectsByDateDesc } = projectsSlice.actions;

export const selectAllProjects = state => state.projects.projects;
export const filteredProjects = state => state.projects.filteredProjects;

export default projectsSlice.reducer;
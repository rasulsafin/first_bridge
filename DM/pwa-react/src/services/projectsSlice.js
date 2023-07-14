import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  projects: [],
  filteredProjects: [],
  currentProject: null,
  isLoading: true,
  error: null
};

export const fetchProjects = createAsyncThunk(
  "projects/fetchProjects", async () => {
    const response = await axiosInstance.get("api/project");
    return response.data;
  });

export const addNewProject = createAsyncThunk(
  "projects/addNewProject", async (newProject, thunkAPI) => {
    await axiosInstance.post("api/project", newProject);
    thunkAPI.dispatch(fetchProjects());
  });

export const updateProject = createAsyncThunk(
  "projects/updateProject", async (data, thunkAPI) => {
    await axiosInstance.put("api/project", data);
    thunkAPI.dispatch(fetchProjects());
  });

export const deleteProject = createAsyncThunk(
  "projects/deleteProject", async (id, thunkAPI) => {
    await axiosInstance.delete("api/project", {
      params: {
        projectId: id
      }
    });
    thunkAPI.dispatch(fetchProjects());
  });

export const addUserListToProject = createAsyncThunk(
  "projects/addProjectListToUser", async (data, thunkAPI) => {
    await axiosInstance.post("api/project/addUserListToProject", data);
    thunkAPI.dispatch(fetchProjects());
  });

export const deleteUserFromProject = createAsyncThunk(
  "projects/deleteUserFromProject", async ({ userId, projectId }, thunkAPI) => {
    await axiosInstance.delete("api/project/deleteUserFromProject", {
      params: {
        userId,
        projectId
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
    },
    setCurrentProject: (state, action) => {
      state.currentProject = action.payload;
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

export const {
  searchProjectsByTitle,
  sortProjectsByDateAsc,
  sortProjectsByDateDesc,
  setCurrentProject
} = projectsSlice.actions;

export const selectAllProjects = state => state.projects.projects;
export const selectCurrentProject = state => state.projects.currentProject;

export default projectsSlice.reducer;
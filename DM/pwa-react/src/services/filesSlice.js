import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";
import fileDownload from "js-file-download";
import { fetchProjects } from "./projectsSlice";

const initialState = {
  files: [],
  isLoading: true,
  error: null
};

export const fetchFiles = createAsyncThunk(
  "files/fetchFiles", async (id, thunkAPI) => {
    try {
      const response = await axiosInstance.get("/api/item", {
        params: {
          projectId: id
        }
      });

      return response.data;
      
    } catch (error) {
      return thunkAPI.rejectWithValue(error.message);
    }
  });

export const uploadFileService = createAsyncThunk(
  "files/uploadFile", async (formData, thunkAPI) => {
    const response = await axiosInstance.post("/api/item/file", formData, {
      params: {project: formData.get("id")}
    });
    thunkAPI.dispatch(fetchProjects());
    return response.data;
  });

export const getFile = createAsyncThunk(
  "files/getFile", async (fileName) => {
    await axiosInstance.get("/api/item/download", {
      params: {
        fileName: fileName
      },
      responseType: "blob"
    }).then((response) => {
      fileDownload(response.data, fileName);
    });
  });

export const filesSlice = createSlice({
  name: "files",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchFiles.pending, (state) => {
      state.isLoading = true;
      state.error = null;
    });
    builder.addCase(fetchFiles.fulfilled, (state, action) => {
      state.isLoading = false;
      state.files = action.payload;
    });
    builder.addCase(fetchFiles.rejected, (state, action) => {
      state.isLoading = false;
      state.error = action.payload;
    });
    builder.addCase(getFile.fulfilled, (state, action) => {
      return action.payload;
    });
    builder.addCase(uploadFileService.fulfilled, (state, action) => {
      
    })
  }
});

export const selectAllFiles = (state) => state.files.files;

export default filesSlice.reducer;
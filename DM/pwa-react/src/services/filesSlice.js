import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchFiles = createAsyncThunk(
  "files/fetchFiles", async () => {
    const response = await axiosInstance.get("/api/item");
    return response.data;
  });

export const uploadFileService = createAsyncThunk(
  'files/uploadFile', async (selectedFile) => {
    const response = await axiosInstance.post("/api/item/file", selectedFile)
    return response.data
  })

export const filesSlice = createSlice({
  name: "files",
  initialState,
  reducers: {
  },
  extraReducers(builder) {
    builder
      .addCase(fetchFiles.fulfilled, (state, action) => {
        return action.payload;
      });
  }
});

export const selectAllFiles = (state) => state.files;

export default filesSlice.reducer;
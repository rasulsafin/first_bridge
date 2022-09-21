import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchRecords = createAsyncThunk(
  "records/fetchRecords", async () => {
    const response = await axiosInstance.get("api/record");
    return response.data;
  });

export const addNewRecord = createAsyncThunk(
  "records/addNewRecord", async (newRecord) => {
    const response = await axiosInstance.post("api/record", newRecord);
    return response.data;
  });

export const recordsSlice = createSlice({
  name: "records",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchRecords.fulfilled, (state, action) => {
      return action.payload;
    });
  }
});

export const selectAllRecords = (state) => state.records;

export default recordsSlice.reducer;
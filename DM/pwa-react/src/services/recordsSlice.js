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

export const editRecord = createAsyncThunk(
  "records/editRecord", async (editableRec) => {
    await axiosInstance.put("api/record", editableRec);
  }
)

export const deleteRecord = createAsyncThunk(
  "records/deleteRecord", async (id) => {
    await axiosInstance.delete("api/record", {
      params: {
        recordId: id
      }
    }).then(() => console.log("Delete successfully"));
  }
)

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
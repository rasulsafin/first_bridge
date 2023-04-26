import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  records: [],
  filteredRecords: [],
  isLoading: true,
  error: null
};

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
  reducers: {
    searchRecordsByName: (state, action) => {
      state.records = state.filteredRecords
        .filter(record => record.name.toLowerCase().includes(action.payload.toLowerCase().trim()));
    },
    sortRecordsByNameAsc: (state) => {
      state.records = state.records
        .sort((a, b) => a.name < b.name ? -1 : 1);
    },
    sortRecordsByNameDesc: (state) => {
      state.records = state.records
        .sort((a, b) => b.name < a.name ? -1 : 1);
    },
    sortRecordsByDateAsc: (state) => {
      state.records = state.records
        .sort((a, b) => new Date(a.createdAt) < new Date(b.createdAt) ? -1 : 1);
    },
    sortRecordsByDateDesc: (state) => {
      state.records = state.records
        .sort((a, b) => new Date(b.createdAt) < new Date(a.createdAt) ? -1 : 1);
    }
  },
  extraReducers(builder) {
    builder.addCase(fetchRecords.fulfilled, (state, action) => {
      state.isLoading = false;
      state.records = action.payload;
      state.filteredRecords = action.payload;
    });
  }
});

export const { 
  searchRecordsByName, 
  sortRecordsByNameAsc, 
  sortRecordsByNameDesc, 
  sortRecordsByDateAsc, 
  sortRecordsByDateDesc,
} = recordsSlice.actions;

export const selectAllRecords = (state) => state.records.records;


export default recordsSlice.reducer;
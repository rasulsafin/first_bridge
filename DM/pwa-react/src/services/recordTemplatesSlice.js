import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  recordTemplates: [],
  filteredRecordTemplates: [],
  isLoading: true,
  error: null
};

export const fetchRecordTemplates = createAsyncThunk(
  "recordTemplates/fetchRecordTemplates", async (projectId) => {
    const response = await axiosInstance.get("api/template", {
      params: {
        projectId
      }
    });
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
  reducers: {
    searchRecordTemplatesByName: (state, action) => {
      state.recordTemplates = state.filteredRecordTemplates
        .filter(template => template.name.toLowerCase().includes(action.payload.toLowerCase().trim()));
    },
    sortRecordTemplatesByNameAsc: (state) => {
      state.recordTemplates = state.recordTemplates
        .sort((a, b) => a.name < b.name ? -1 : 1);
    },
    sortRecordTemplatesByNameDesc: (state) => {
      state.recordTemplates = state.recordTemplates
        .sort((a, b) => b.name < a.name ? -1 : 1);
    },
    sortRecordTemplatesByDateAsc: (state) => {
      state.recordTemplates = state.recordTemplates
        .sort((a, b) => new Date(a.createdAt) < new Date(b.createdAt) ? -1 : 1);
    },
    sortRecordTemplatesByDateDesc: (state) => {
      state.recordTemplates = state.recordTemplates
        .sort((a, b) => new Date(b.createdAt) < new Date(a.createdAt) ? -1 : 1);
    }
  },
  extraReducers(builder) {
    builder.addCase(fetchRecordTemplates.fulfilled, (state, action) => {
      state.isLoading = false;
      state.recordTemplates = action.payload;
      state.filteredRecordTemplates = action.payload;
    });
  }
});

export const {
  searchRecordTemplatesByName,
  sortRecordTemplatesByNameAsc,
  sortRecordTemplatesByNameDesc,
  sortRecordTemplatesByDateAsc,
  sortRecordTemplatesByDateDesc,
} = recordTemplatesSlice.actions;
 
export const selectAllRecordTemplates = (state) => state.recordTemplates.recordTemplates;

export default recordTemplatesSlice.reducer;
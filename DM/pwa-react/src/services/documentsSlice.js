import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  documents: [],
  filteredDocuments: [],
  isLoading: true,
  error: null
};

export const fetchDocuments = createAsyncThunk(
  "documents/fetchDocuments", async () => {
    const response = await axiosInstance.get("api/document");
    return response.data;
  });

export const addNewDocument = createAsyncThunk(
  "documents/addNewDocument", async (newDocument) => {
    const response = await axiosInstance.post("api/document", newDocument);
    return response.data;
  });

export const editDocument = createAsyncThunk(
  "documents/editDocument", async (editableDocument) => {
    const response = await axiosInstance.put("api/document", editableDocument);
    return response.data;
  });

export const deleteDocument = createAsyncThunk(
  "documents/deleteDocument", async (documentId) => {
    await axiosInstance.delete("api/document", {
      params: {
        documentId
      }
    }).then(() => console.log("Delete successfully"));
  });

export const documentsSlice = createSlice({
  name: "documents",
  initialState,
  reducers: {
    searchUsersByName: (state, action) => {
      state.documents = state.filteredDocuments
        .filter(document => document.name.toLowerCase().includes(action.payload.toLowerCase().trim()));
    },
    sortDocumentsByNameAsc: (state) => {
      state.documents = state.documents
        .sort((a, b) => a.name < b.name ? -1 : 1);
    },
    sortDocumentsByNameDesc: (state) => {
      state.documents = state.documents
        .sort((a, b) => b.name < a.name ? -1 : 1);
    }
  },
  extraReducers(builder) {
    builder
      .addCase(fetchDocuments.fulfilled, (state, action) => {
        state.isLoading = false;
        state.documents = action.payload;
        state.filteredDocuments = action.payload;
      });
  }
});

export const { searchDocumentsByName, sortDocumentsByNameAsc, sortDocumentsByNameDesc } = documentsSlice.actions;

export const selectAllDocuments = (state) => state.documents.documents;

export default documentsSlice.reducer;
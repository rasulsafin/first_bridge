import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";
import { fetchRecordTemplates } from "./recordTemplatesSlice";

const initialState = {
  fields: [],
  listFields: [],
  isLoading: true,
  error: null
};

export const addNewField = createAsyncThunk(
  "fields/addNewField", async ({ newField, projectId }, thunkAPI) => {
    await axiosInstance.post("api/field", newField);
    thunkAPI.dispatch(fetchRecordTemplates(projectId));
  });

export const addNewListField = createAsyncThunk(
  "fields/addNewField", async ({ newListField, projectId }, thunkAPI) => {
    await axiosInstance.post("api/listField", newListField);
    thunkAPI.dispatch(fetchRecordTemplates(projectId));
  });

export const deleteField = createAsyncThunk(
  "fields/deleteField", async ({ fieldId, projectId }, thunkAPI) => {
    await axiosInstance.delete("api/field", {
      params: { fieldId }
    });
    thunkAPI.dispatch(fetchRecordTemplates(projectId));
  });

export const deleteListField = createAsyncThunk(
  "fields/deleteField", async ({ listFieldId, projectId }, thunkAPI) => {
    await axiosInstance.delete("api/listField", {
      params: { listFieldId }
    });
    thunkAPI.dispatch(fetchRecordTemplates(projectId));
  });

export const fieldsSlice = createSlice({
  name: "fields",
  initialState,
  reducers: {}
});

export default fieldsSlice.reducer;
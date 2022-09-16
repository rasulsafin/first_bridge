import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchPermissions = createAsyncThunk(
  "permissions/fetchPermissions", async () => {
    const response = await axiosInstance.get("api/permission");
    return response.data;
  });

export const addNewPermission = createAsyncThunk(
  "permissions/addNewPermission", async (newPermission) => {
    const response = await axiosInstance.post("api/permission", newPermission);
    return response.data;
  });

export const permissionsSlice = createSlice({
  name: "permissions",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchPermissions.fulfilled, (state, action) => {
      return action.payload;
    });
  }
});

export const selectAllPermissions = (state) => state.permissions;

export default permissionsSlice.reducer;
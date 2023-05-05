import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  permissions: [],
  isLoading: true,
  error: null
};

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

export const deletePermission = createAsyncThunk(
  "permissions/deletePermission", async () => {
    await axiosInstance.delete("api/permission", {
      params: {
      }
    }).then(() => console.log("Delete successfully"));
  }
)

export const permissionsSlice = createSlice({
  name: "permissions",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchPermissions.fulfilled, (state, action) => {
      state.isLoading = false;
      state.permissions = action.payload;
    });
  }
});

export const selectAllPermissions = (state) => state.permissions.permissions;

export default permissionsSlice.reducer;
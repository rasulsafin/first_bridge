import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  roles: [],
  filteredRoles: [],
  isLoading: true,
  error: null
};

export const fetchRoles = createAsyncThunk(
  "roles/fetchRoles", async () => {
    const response = await axiosInstance.get("api/role");
    return response.data;
  });

export const addNewRole = createAsyncThunk(
  "roles/addNewRole", async (newRole) => {
    const response = await axiosInstance.post("api/role", newRole);
    return response.data;
  });

export const editRole = createAsyncThunk(
  "roles/editRole", async (editableRole) => {
    const response = await axiosInstance.put("api/role", editableRole);
    return response.data;
  });

export const deleteRole = createAsyncThunk(
  "roles/deleteRole", async (id) => {
    await axiosInstance.delete("api/role", {
      params: {
        roleId: id
      }
    }).then(() => console.log("Delete successfully"));
  }
)

export const rolesSlice = createSlice({
  name: "roles",
  initialState,
  reducers: {
    searchRolesByName: (state, action) => {
      state.roles = state.filteredRoles
        .filter(role => role.name.toLowerCase().includes(action.payload.toLowerCase().trim()));
    },
    sortRolesByNameAsc: (state) => {
      state.roles = state.roles
        .sort((a, b) => a.name < b.name ? -1 : 1);
    },
    sortRolesByNameDesc: (state) => {
      state.roles = state.roles
        .sort((a, b) => b.name < a.name ? -1 : 1);
    }
  },
  extraReducers(builder) {
    builder
      .addCase(fetchRoles.fulfilled, (state, action) => {
        state.isLoading = false;
        state.roles = action.payload;
        state.filteredRoles = action.payload;
      });
  }
});

export const { searchRolesByName, sortRolesByNameAsc, sortRolesByNameDesc } = rolesSlice.actions;

export const selectAllRoles = (state) => state.roles.roles;

export default rolesSlice.reducer;
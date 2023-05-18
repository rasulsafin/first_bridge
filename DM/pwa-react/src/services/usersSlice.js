import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  users: [],
  filteredUsers: [],
  isLoading: true,
  error: null
};

export const fetchUsers = createAsyncThunk(
  "users/fetchUsers", async () => {
    const response = await axiosInstance.get("api/users");
    return response.data;
  });

export const addNewUser = createAsyncThunk(
  "users/addNewUser", async (newUser, thunkAPI) => {
    await axiosInstance.post("api/users", newUser);
    thunkAPI.dispatch(fetchUsers());
  });

export const editUser = createAsyncThunk(
  "users/editUser", async (editableUser) => {
    const response = await axiosInstance.put("api/users", editableUser);
    return response.data;
  });

export const deleteUser = createAsyncThunk(
  "users/deleteUser", async (id) => {
    await axiosInstance.delete("api/users", {
      params: {
        userId: id
      }
    }).then(() => console.log("Delete successfully"));
  });

export const addProjectListToUser = createAsyncThunk(
  "users/addProjectListToUser", async (data) => {
    const response = await axiosInstance.post("api/users/addProjectListToUser", data);
    return response.data;
  });

export const deleteProjectFromUser = createAsyncThunk(
  "users/deleteProjectFromUser", async ({ projectId, userId }, thunkAPI) => {
    await axiosInstance.delete("api/users/deleteProjectFromUser", {
      params: {
        projectId,
        userId
      }
    });
    thunkAPI.dispatch(fetchUsers());
  });

export const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {
    searchUsersByName: (state, action) => {
      state.users = state.filteredUsers
        .filter(user => user.lastName.toLowerCase().includes(action.payload.toLowerCase().trim()));
    },
    sortUsersByNameAsc: (state) => {
      state.users = state.users
        .sort((a, b) => a.lastName < b.lastName ? -1 : 1);
    },
    sortUsersByNameDesc: (state) => {
      state.users = state.users
        .sort((a, b) => b.lastName < a.lastName ? -1 : 1);
    }
  },
  extraReducers(builder) {
    builder
      .addCase(fetchUsers.fulfilled, (state, action) => {
        state.isLoading = false;
        state.users = action.payload;
        state.filteredUsers = action.payload;
      });
  }
});

export const {
  searchUsersByName,
  sortUsersByNameAsc,
  sortUsersByNameDesc
} = usersSlice.actions;

export const selectAllUsers = (state) => state.users.users;

export default usersSlice.reducer;
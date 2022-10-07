import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchUsers = createAsyncThunk(
  "users/fetchUsers", async () => {
    const response = await axiosInstance.get("api/users");
    return response.data;
  });

export const addNewUser = createAsyncThunk(
  "users/addNewUser", async (newUser) => {
    const response = await axiosInstance.post("api/users", newUser);
    return response.data;
  });

export const EditUser = createAsyncThunk(
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
  }
)

export const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder
      .addCase(fetchUsers.fulfilled, (state, action) => {
        return action.payload;
      });
  }
});

export const selectAllUsers = (state) => state.users;

export default usersSlice.reducer;
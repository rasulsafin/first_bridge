import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = [];

export const fetchUsers = createAsyncThunk(
  "users/fetchUsers", async () => {
  const response = await axiosInstance.get("api/users");
  console.log(response.data);
  return response.data;
});

export const addNewUser = createAsyncThunk(
    'posts/addNewUser', async (newUser) => {
  const response = await axiosInstance.post("api/users", newUser)
  return response.data
})

export const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {
    // addUser: (state, action) => {
    //   state.users.push(action.payload);
    // }
  },
  extraReducers(builder) {
    builder
      .addCase(fetchUsers.fulfilled, (state, action) => {
      return action.payload;
    });
  }
});

export const selectAllUsers = (state) => state.users;

export default usersSlice.reducer;
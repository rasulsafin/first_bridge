import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import jwtDecode from "jwt-decode";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  user: null
};

export const login = createAsyncThunk(
  "api/users/authenticate", async ({ user, pwd }) => {
    const response = await axiosInstance.post("api/users/authenticate",
      { login: user, password: pwd },
      {
        headers: { "Content-Type": "application/json-patch+json" },
        withCredentials: true
      });
    return response.data;
  });

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout(state) {
      state.user = null;
      localStorage.removeItem("token");
    }
  },
  extraReducers: (builder) => {
    builder.addCase(login.fulfilled, (state, action) => {
      const token = action.payload;
      state.user = jwtDecode(token.token);
      localStorage.setItem("token", token.token);
    });
  }
});

export const { logout } = authSlice.actions;

export const selectCurrentUser = (state) => state.auth.user;

export const authReducer = authSlice.reducer;

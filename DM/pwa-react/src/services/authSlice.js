import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";
import jwtDecode from "jwt-decode";

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
      localStorage.removeItem("user");
    }
  },
  extraReducers: (builder) => {
    builder.addCase(login.fulfilled, (state, action) => {
      const token = action.payload;
      const user = jwtDecode(token.token);
      localStorage.setItem("token", token.token);
      localStorage.setItem("user", JSON.stringify(user));
      state.user = user;
    });
  }
});

export const { logout } = authSlice.actions;

export const selectUser = (state) => state.auth.user;

export const authReducer = authSlice.reducer;

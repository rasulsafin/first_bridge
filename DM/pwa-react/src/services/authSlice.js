import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
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
      localStorage.removeItem("user");
      localStorage.removeItem("role");
    }
  },
  extraReducers: (builder) => {
    builder.addCase(login.fulfilled, (state, action) => {
      const user = action.payload;
      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("token", user.token);
      localStorage.setItem("role", JSON.stringify(user.role));
      state.user = user;
    });
  }
});

export const { logout } = authSlice.actions;

export const authReducer = authSlice.reducer;

import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  id: null,
  name: null,
  login: null,
  token: null,
  email: null,
  role: null,
  organizationId: null,
};

// TODO
// export const registerUser = createAsyncThunk(
//   "api/users/authenticate", async ({ user, password }, { rejectWithValue }) => {
//     try {
//       await axiosInstance.post("api/users/authenticate",
//         JSON.stringify({ user, password }),
//         {
//           headers: { "Content-Type": "application/json-patch+json" },
//           withCredentials: true
//         })
//     } catch (error) {
//       // return custom error message from backend if present
//       if (error.response && error.response.data.message) {
//         return rejectWithValue(error.response.data.message)
//       } else {
//         return rejectWithValue(error.message)
//       }
//     }
//   })

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setAuthUser(state, action) {
      state.id = action.payload.id;
      state.name = action.payload.name;
      state.login = action.payload.login;
      state.token = action.payload.token;
      state.email = action.payload.email;
      state.role = action.payload.role;
      state.organizationId = action.payload.organizationId;
    },
    removeAuthUser(state) {
      state.id = null;
      state.name = null;
      state.login = null;
      state.token = null;
      state.email = null;
      state.role = null;
      state.organizationId = null;
      localStorage.removeItem("token");
    }
  }
});

export const { setAuthUser, removeAuthUser } = authSlice.actions;

export const authReducer = authSlice.reducer;

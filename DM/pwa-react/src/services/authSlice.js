import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  id: null,
  name: null,
  login: null,
  token: null,
  email: null,
  role: null,
  organizationId: null,
};

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
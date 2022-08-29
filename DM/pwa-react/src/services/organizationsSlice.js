import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";
import { Organizations } from "../components/pages/OrganizationsPage/Organizations";

const initialState = [];

export const fetchOrganizations = createAsyncThunk(
  "organizations/fetchOrganizations", async () => {
    const response = await axiosInstance.get("api/organization");
    return response.data;
  });

export const addNewOrganization = createAsyncThunk(
  "organizations/addNewOrganization", async (newOrganization) => {
    const response = await axiosInstance.post("api/organization", newOrganization);
    return response.data;
  });

export const organizationsSlice = createSlice({
  name: "organizations",
  initialState,
  reducers: {},
  extraReducers(builder) {
    builder.addCase(fetchOrganizations.fulfilled, (state, action) => {
      return action.payload;
    });
  }
});

export const selectAllOrganizations = (state) => state.organizations;

export default organizationsSlice.reducer;
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

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

export const editOrganization = createAsyncThunk(
  "organizations/editOrganization", async (editableOrg) => {
    const response = await axiosInstance.put("api/organization", editableOrg);
    return response.data;
  });

export const deleteOrganization = createAsyncThunk(
  "organizations/deleteOrganization", async (id) => {
    await axiosInstance.delete("api/organization", {
      params: {
        organizationId: id
      }
    }).then(() => console.log("Delete successfully"));
  }
)

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
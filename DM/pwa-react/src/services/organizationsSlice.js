import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { axiosInstance } from "../axios/axiosInstance";

const initialState = {
  organizations: [],
  isLoading: true,
  error: null
};

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
  reducers: {
    setOrganization(state, action) {
      state.push(action.payload);
    },
  },
  extraReducers(builder) {
    builder.addCase(fetchOrganizations.fulfilled, (state, action) => {
      state.isLoading = false;
      state.organizations = action.payload;
    });
  }
});

export const { setOrganization } = organizationsSlice.actions;

export const selectAllOrganizations = (state) => state.organizations.organizations;

export default organizationsSlice.reducer;
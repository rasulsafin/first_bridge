import axios from "axios";
import fetchAdapter from "@vespaiach/axios-fetch-adapter";

export const axiosInstance = axios.create({
  adapter: fetchAdapter,
  baseURL: process.env.REACT_APP_BACKEND_URL
});

axiosInstance.interceptors.request.use(request => {
  const token = localStorage.getItem("token");
  if (token) {
    request.headers.Authorization = `Bearer ${token}`;
  }
  return request;
});
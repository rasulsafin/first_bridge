import axios from "axios";

export const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_BACKEND_URL,
})

axiosInstance.interceptors.request.use(request => {

  const token = localStorage.getItem("token");
  if (token)
  {
    request.headers.Authorization = `Bearer ${token}`;
  }
    return request;
  });
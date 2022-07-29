import axios from "axios";

let headers = {};

if (localStorage.token) {
  headers.Authorization = `Bearer ${localStorage.token}`;
}

export const axiosInstance = axios.create({
  baseURL: process.env.REACT_APP_BACKEND_URL,
  headers,
})

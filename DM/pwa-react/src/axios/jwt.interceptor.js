import axios from "axios";


//TODO

// export function jwtInterceptor() {
//
//   axios.interceptors.request.use(request => {
//
//     // add auth header with jwt if account is logged in and request is to the api url
//
//     const token = localStorage.getItem("token");
//     const isApiUrl = request.url.startsWith('https://localhost:5001/');
//     request.headers.Authorization = `Bearer ${token}`;
//
//     return request;
//   });
// }
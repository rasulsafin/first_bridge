import { configureStore } from '@reduxjs/toolkit'
import thunk from "redux-thunk";
import logger from 'redux-logger'
import usersReducer from '../services/usersSlice';
import projectsReducer from '../services/projectsSlice';
import recordsReducer from '../services/recordsSlice';
import { authReducer } from "../services/authSlice";
import offlineSync from "./offlineSync";



if (localStorage.getItem("version") !== process.env.REACT_APP_VERSION) {
  localStorage.removeItem("state");
}

const preloadedState = JSON.parse(
  localStorage.getItem("state"));

const store = configureStore({
  reducer: {
    auth: authReducer,
    users: usersReducer,
    projects: projectsReducer,
    records: recordsReducer,
  },
  middleware: [thunk, offlineSync, logger],
  preloadedState,
})

export default store
import { configureStore } from '@reduxjs/toolkit'
import thunk from "redux-thunk";
import logger from 'redux-logger'
import usersReducer from '../services/usersSlice';
import projectsReducer from '../services/projectsSlice';
import recordsReducer from '../services/recordsSlice';

 const store = configureStore({
  reducer: {
    users: usersReducer,
    projects: projectsReducer,
    records: recordsReducer,
  },
  middleware: [thunk, logger],
})

export default store
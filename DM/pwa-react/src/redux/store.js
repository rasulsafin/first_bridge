import { configureStore } from '@reduxjs/toolkit'
import UsersReducer from "./usersReducer";
import thunk from "redux-thunk";
import logger from 'redux-logger'

 const store = configureStore({
  reducer: {
    usersReducer: UsersReducer,
  },
  middleware: [thunk, logger],
})

export default store
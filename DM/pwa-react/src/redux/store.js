import { configureStore, combineReducers } from '@reduxjs/toolkit'
import thunk from "redux-thunk";
import logger from 'redux-logger'
import { persistStore, persistReducer, } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import usersReducer from '../services/usersSlice';
import projectsReducer from '../services/projectsSlice';
import recordsReducer from '../services/recordsSlice';
import { authReducer } from "../services/authSlice";

const persistConfig = {
  key: 'root',
  storage,
}

const rootReducer = combineReducers({
  auth: authReducer,
  users: usersReducer,
  projects: projectsReducer,
  records: recordsReducer,
})

const persistedReducer = persistReducer(persistConfig, rootReducer);

const store = configureStore({
  reducer: persistedReducer,
  middleware: [thunk, logger],
  })

export const persistor = persistStore(store);
export default store
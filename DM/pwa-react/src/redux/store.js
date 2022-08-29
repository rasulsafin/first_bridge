import { configureStore, combineReducers } from "@reduxjs/toolkit";
import thunk from "redux-thunk";
import logger from "redux-logger";
import { persistStore, persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";
import usersReducer from "../services/usersSlice";
import projectsReducer from "../services/projectsSlice";
import recordsReducer from "../services/recordsSlice";
import filesReducer from "../services/filesSlice";
import { authReducer } from "../services/authSlice";
import recordTemplatesReducer from "../services/recordTemplatesSlice";


const persistConfig = {
  key: "root",
  storage
};

const rootReducer = combineReducers({
  auth: authReducer,
  users: usersReducer,
  projects: projectsReducer,
  records: recordsReducer,
  recordTemplates: recordTemplatesReducer,
  files: filesReducer
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

const store = configureStore({
  reducer: persistedReducer,
  middleware: [thunk, logger]
});

export const persistor = persistStore(store);
export default store;
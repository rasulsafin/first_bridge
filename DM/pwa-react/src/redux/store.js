import { configureStore, combineReducers } from "@reduxjs/toolkit";
import thunk from "redux-thunk";
import logger from "redux-logger";
import { persistStore, persistReducer } from "redux-persist";
// import storage from "redux-persist/lib/storage";
import storage from 'redux-persist-indexeddb-storage';
import usersReducer from "../services/usersSlice";
import organizationsReducer from "../services/organizationsSlice";
import projectsReducer from "../services/projectsSlice";
import recordsReducer from "../services/recordsSlice";
import filesReducer from "../services/filesSlice";
import { authReducer } from "../services/authSlice";
import recordTemplatesReducer from "../services/recordTemplatesSlice";
import permissionsReducer from "../services/permissionsSlice";
import { controlSidebarReducer } from "../services/controlSidebarSlice";
import snackbarReducer from "../services/snackbarSlice";
import { ifcElementPropsReducer } from "../services/ifcElementPropsSlice";
import ifcModelReducer from "../services/ifcModelSlice";
import autoMergeLevel2 from "redux-persist/es/stateReconciler/autoMergeLevel2";

const persistConfig = {
  key: "root",
  storage: storage('myDB'),
  stateReconciler: autoMergeLevel2,
  blacklist: ["ifcModel"]
};

const rootReducer = combineReducers({
  auth: authReducer,
  users: usersReducer,
  permissions: permissionsReducer,
  organizations: organizationsReducer,
  projects: projectsReducer,
  records: recordsReducer,
  recordTemplates: recordTemplatesReducer,
  files: filesReducer,
  controlSidebar: controlSidebarReducer,
  snackbar: snackbarReducer,
  ifcElementProps: ifcElementPropsReducer,
  ifcModel: ifcModelReducer,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

const store = configureStore({
  reducer: persistedReducer,
  middleware: [thunk, logger]
});

export const persistor = persistStore(store);
export default store;
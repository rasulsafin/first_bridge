import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Home } from "./components/pages/HomePage/Home";
import { Users } from "./components/pages/UsersPage/Users";
import { Projects } from "./components/pages/ProjectsPage/Projects";
import { Records } from "./components/pages/RecordsPage/Records";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import { ProfilePage } from "./components/pages/ProfilePage/ProfilePage";
import { RequireAuth } from "./components/RequireAuth/RequireAuth";
import { AdminPage } from "./components/pages/AdminPage/AdminPage";
import { Layout } from "./components/layout/Layout";
import { NotFoundPage } from "./components/pages/NotFoundPage/NotFoundPage";
import ViewerIfc from "./components/pages/FilesPage/ViewerIfc";
import IfcComponent from "./components/ifc/IfcComponent";
import { Models } from "./components/pages/ModelPage/Models";
import { Templates } from "./components/pages/TemplatePage/Templates";
import { Roles } from "./components/pages/RolesPage/Roles";

function App() {
  window.addEventListener("load", () => {
    if ("serviceWorker" in navigator) {
      navigator.serviceWorker.register("./service-worker.js")
        .then(registration => {
          console.log("Service worker successfully registered", registration);
        })
        .catch(error => {
          console.log("Service worker registration failed", error);
        });
    }
  });

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route element={<RequireAuth />}>
              <Route index element={<Home />} />
              <Route path="/users" element={<Users />} />
              <Route path="/roles" element={<Roles />} />
              <Route path="/projects" element={<Projects />} />
              <Route path="/records" element={<Records />} />
              <Route path="/models" element={<Models />} />
              <Route path="/templates" element={<Templates />} />
              <Route path="/profile" element={<ProfilePage />} />
              <Route path="/admin" element={<AdminPage />} />
              <Route path="/ifc" element={<ViewerIfc />} />
              <Route path="/ifcViewer" element={<IfcComponent />} />
            </Route>

            <Route path="/login" element={<LoginPage />} />

            <Route path="*" element={<NotFoundPage />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;

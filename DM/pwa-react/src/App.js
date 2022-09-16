import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Home } from "./components/pages/HomePage/Home";
import { Users } from "./components/pages/UsersPage/Users";
import { Projects } from "./components/pages/ProjectsPage/Projects";
import { ProjectDetailPage } from "./components/pages/ProjectsPage/components/ProjectDetailPage";
import { Records } from "./components/pages/RecordsPage/Records";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import { UserDetailPage } from "./components/pages/UsersPage/components/UserDetailPage";
import { ProfilePage } from "./components/pages/ProfilePage/ProfilePage";
import RegisterPage from "./components/pages/LoginPage/RegisterPage";
import { RecordDetailPage } from "./components/pages/RecordsPage/components/RecordDetailPage";
import { RequireAuth } from "./components/RequireAuth/RequireAuth";
import { GenerateFormPage } from "./components/pages/GenerateFormPage/GenerateFormPage";
import { Organizations } from "./components/pages/OrganizationsPage/Organizations";
import { OrganizationDetailPage } from "./components/pages/OrganizationsPage/components/OrganizationDetailPage";
import { AdminPage } from "./components/pages/AdminPage/AdminPage";
import { Layout } from "./components/layout/Layout";
import { ProjectCreatePage } from "./components/pages/ProjectsPage/components/ProjectCreatePage";

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
                <Route path="/user/:id" element={<UserDetailPage />} />
                <Route path="/organizations" element={<Organizations />} />
                <Route path="/organization/:id" element={<OrganizationDetailPage />} />
                <Route path="/projects" element={<Projects />} />
                <Route path="/project/:id" element={<ProjectDetailPage />} />
                <Route path="/project/create" element={<ProjectCreatePage />} />
                <Route path="/records" element={<Records />} />
                <Route path="/record/:id" element={<RecordDetailPage />} />
                <Route path="/profile" element={<ProfilePage />} />
                <Route path="/generate-form" element={<GenerateFormPage />} />
                <Route path="/admin" element={<AdminPage />} />
              </Route>

              <Route path="/login" element={<LoginPage />} />
              <Route path="/registration" element={<RegisterPage />} />
            </Route>
          </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;

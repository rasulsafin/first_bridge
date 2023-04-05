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
import { RecordDetailPage } from "./components/pages/RecordsPage/components/RecordDetailPage";
import { RequireAuth } from "./components/RequireAuth/RequireAuth";
import { Organizations } from "./components/pages/OrganizationsPage/Organizations";
import { OrganizationDetailPage } from "./components/pages/OrganizationsPage/components/OrganizationDetailPage";
import { AdminPage } from "./components/pages/AdminPage/AdminPage";
import { Layout } from "./components/layout/Layout";
import { ProjectCreatePage } from "./components/pages/ProjectsPage/components/ProjectCreatePage";
import { TemplateCreatePage } from "./components/pages/TemplatePage/TemplateCreatePage";
import { RecordCreatePage } from "./components/pages/RecordsPage/components/RecordCreatePage";
import { FilesPage } from "./components/pages/FilesPage/FilesPage";
import { ProjectEditPage } from "./components/pages/ProjectsPage/components/ProjectEditPage";
import { NotFoundPage } from "./components/pages/NotFoundPage/NotFoundPage";
import { UserCreatePage } from "./components/pages/UsersPage/components/UserCreatePage";
import { UserEditPage } from "./components/pages/UsersPage/components/UserEditPage";
import { OrganizationCreatePage } from "./components/pages/OrganizationsPage/components/OrganizationCreatePage";
import { RecordEditPage } from "./components/pages/RecordsPage/components/RecordEditPage";
import ViewerIfc from "./components/pages/FilesPage/ViewerIfc";
import { OrganizationEditPage } from "./components/pages/OrganizationsPage/components/OrganizationEditPage";
import IfcComponent from "./components/ifc/IfcComponent";
import LoginPage1 from "./components/pages/LoginPage/LoginPage1";
import { Models } from "./components/pages/ModelPage/Models";

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
              <Route path="/user/:id/edit" element={<UserEditPage />} />
              <Route path="/user/create" element={<UserCreatePage />} />

              <Route path="/organizations" element={<Organizations />} />
              <Route path="/organization/:id" element={<OrganizationDetailPage />} />
              <Route path="/organization/create" element={<OrganizationCreatePage />} />
              <Route path="/organization/:id/edit" element={<OrganizationEditPage />} />

              <Route path="/projects" element={<Projects />} />
              <Route path="/project/:id" element={<ProjectDetailPage />} />
              <Route path="/project/:id/files" element={<FilesPage />} />
              <Route path="/project/create" element={<ProjectCreatePage />} />
              <Route path="/project/:id/edit" element={<ProjectEditPage />} />

              <Route path="/records" element={<Records />} />
              <Route path="/record/:id" element={<RecordDetailPage />} />
              <Route path="/record/:id/edit" element={<RecordEditPage />} />
              <Route path="/record/create" element={<RecordCreatePage />} />

              <Route path="/models" element={<Models />} />

              <Route path="/template/create" element={<TemplateCreatePage />} />
              <Route path="/profile" element={<ProfilePage />} />
              <Route path="/admin" element={<AdminPage />} />

              <Route path="/ifc" element={<ViewerIfc />} />
              <Route path="/ifcViewer" element={<IfcComponent />} />
            </Route>

            <Route path="/login" element={<LoginPage />} />
            <Route path="/login1" element={<LoginPage1 />} />

            <Route path="*" element={<NotFoundPage />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;

import React, { useState } from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Home } from "./components/pages/HomePage/Home";
import { Users } from "./components/pages/UsersPage/Users";
import { Projects } from "./components/pages/ProjectsPage/Projects";
import { ProjectDetailPage } from "./components/pages/ProjectsPage/components/ProjectDetailPage";
import { Records } from "./components/pages/RecordsPage/Records";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import Sidebar from "./components/sidebar/Sidebar";
import { UserDetailPage } from "./components/pages/UsersPage/components/UserDetailPage";
import { ProfilePage } from "./components/pages/ProfilePage/ProfilePage";
import RegisterPage from "./components/pages/LoginPage/RegisterPage";
import { RecordDetailPage } from "./components/pages/RecordsPage/components/RecordDetailPage";
import { RequireAuth } from "./components/RequireAuth/RequireAuth";

function App() {
  const [sidebar, setSidebar] = useState(false)
  
  return (
    <>
      <BrowserRouter>
        <Sidebar onCollapse={(sidebar) => {
          console.log(sidebar);
          setSidebar(sidebar);
        }} />
        <main className={`container ${!sidebar ? "inactive" : "active"}`}>
          <Routes>
            <Route element={<RequireAuth />}>
            <Route index element={<Home />} />
            <Route path="/users" element={<Users />} />
            <Route path="/user/:id" element={<UserDetailPage />} />
            <Route path="/projects" element={<Projects />} />
            <Route path="/project/:id" element={<ProjectDetailPage />} />
            <Route path="/records" element={<Records />} />
            <Route path="/record/:id" element={<RecordDetailPage />} />
            <Route path="/profile" element={<ProfilePage />} />
            </Route>
              
            <Route path="/login" element={<LoginPage />} />
            <Route path="/registration" element={<RegisterPage />} />
          </Routes>
        </main>
      </BrowserRouter>
    </>
  );
}

export default App;

import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Route, Link, Routes } from "react-router-dom";
import { AppBar, Box, Typography, Toolbar } from "@mui/material";
import { Home } from "./components/pages/HomePage/Home";
import { Users } from "./components/pages/UsersPage/Users";
import { Projects } from "./components/pages/ProjectsPage/Projects";
import { ProjectDetailPage } from "./components/pages/ProjectsPage/components/ProjectDetailPage";
import { Records } from "./components/pages/RecordsPage/Records";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import { UsersTest } from "./components/pages/UsersPage/UsersTest";

function App() {
  return (
    <>
      <BrowserRouter>
        <Box sx={{ flexGrow: 1 }}>
          <AppBar>
            <Toolbar>
              <Link to="/"> <Typography sx={{ color: "#fff", paddingRight: "40px " }}> Home </Typography></Link>
              <Link to="/projects"> <Typography
                sx={{ color: "#fff", paddingRight: "40px" }}> Projects </Typography></Link>
              <Link to="/records"> <Typography
                sx={{ color: "#fff", paddingRight: "40px" }}> Records </Typography></Link>
              <Link to="/users"> <Typography sx={{ color: "#fff", paddingRight: "40px" }}> Users </Typography></Link>
              <Link to="/login"> <Typography
                sx={{ color: "#fff", paddingRight: "40px" }}> LoginPage </Typography></Link>
            </Toolbar>
          </AppBar>
        </Box>
        <div style={{ marginTop: "80px" }}>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/users" element={<Users />} />
            <Route path="/projects" element={<Projects />} />
            <Route path="/project/:id" element={<ProjectDetailPage />} />
            <Route path="/records" element={<Records />} />
            <Route path="/login" element={<LoginPage />} />
          </Routes>
        </div>
      </BrowserRouter>
    </>
  );
}

export default App;

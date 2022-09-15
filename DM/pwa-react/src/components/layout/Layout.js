import { Outlet } from "react-router";
import { Appbar } from "../appbar/Appbar";
import { Sidebar } from "../sidebar/Sidebar";
import React, { useState } from "react";
import "./Layout.css";

export const Layout = () => {
  const [sidebar, setSidebar] = useState(false);

  return (
    <>
      <header>
        <Appbar />
      </header>
      <Sidebar onCollapse={(sidebar) => {
        setSidebar(sidebar);
      }} />
      <main className={`container ${!sidebar ? "inactive" : "active"}`}>
        <Outlet />
      </main>
      <footer style={{
        backgroundColor: "orange",
        height: "20px",
        position: "fixed",
        bottom: 0,
        left: 0,
        right: 0
      }}>
      </footer>
    </>
  );
};
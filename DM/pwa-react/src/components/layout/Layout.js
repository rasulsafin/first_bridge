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
      {/*  <div className="sub-menu" */}
      {/*       style={{*/}
      {/*    backgroundColor: "orange",*/}
      {/*    height: "60px",*/}
      {/*    width: "100vw",*/}
      {/*  }}>*/}
      {/*  </div>*/}
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
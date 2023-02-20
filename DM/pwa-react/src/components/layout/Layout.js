import { Outlet } from "react-router";
import { Appbar } from "../appbar/Appbar";
import { Sidebar } from "../sidebar/Sidebar";
import React, { useState } from "react";
import "./Layout.css";
import { ErrorFallback } from "../ErrorFallback/ErrorFallback";
import { ErrorBoundary } from "react-error-boundary";

export const Layout = () => {
  const [sidebar, setSidebar] = useState(false);

  return (
    <>
      <header>
        {/*<Appbar />*/}
      </header>
      <Sidebar onCollapse={(sidebar) => {
        setSidebar(sidebar);
      }} />
      <main className={`container ${!sidebar ? "inactive" : "active"}`}>
        <ErrorBoundary
          FallbackComponent={ErrorFallback}
        >
        <Outlet />
        </ErrorBoundary>
      </main>
      <footer>
      </footer>
    </>
  );
};
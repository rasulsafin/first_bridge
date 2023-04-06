import { Outlet, useLocation } from "react-router";
import { Sidebar } from "../sidebar/Sidebar";
import React from "react";
import "./Layout.css";
import { ErrorFallback } from "../ErrorFallback/ErrorFallback";
import { ErrorBoundary } from "react-error-boundary";

export const Layout = () => {
  const location = useLocation();
  const { pathname } = location;
  
  return (
    <>
      <header>
      </header>
      {pathname !== "/login" ? <Sidebar /> : null}
      <main className="container">
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
import React from "react";
import { Outlet, useLocation } from "react-router";
import { ErrorBoundary } from "react-error-boundary";
import "./Layout.css";
import { Sidebar } from "../sidebar/Sidebar";
import { ErrorFallback } from "../ErrorFallback/ErrorFallback";

export const Layout = () => {
  const location = useLocation();
  const { pathname } = location;

  return (
    <>
      {pathname !== "/login" ? <Sidebar /> : null}
      <main className="container">
        <ErrorBoundary
          FallbackComponent={ErrorFallback}
        >
          <Outlet />
        </ErrorBoundary>
      </main>
    </>
  );
};
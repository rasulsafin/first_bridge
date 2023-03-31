import { Outlet } from "react-router";
import { Sidebar } from "../sidebar/Sidebar";
import React from "react";
import "./Layout.css";
import { ErrorFallback } from "../ErrorFallback/ErrorFallback";
import { ErrorBoundary } from "react-error-boundary";

export const Layout = () => {
  return (
    <>
      <header>
      </header>
      <Sidebar />
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
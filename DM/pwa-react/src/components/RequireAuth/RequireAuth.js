import React from "react";
import { Navigate, Outlet, useLocation } from "react-router";
import { useSelector } from "react-redux";
import { selectCurrentUser } from "../../services/authSlice";

export const RequireAuth = () => {
  const location = useLocation();
  const currentUser = useSelector(selectCurrentUser);
  
  return(
    currentUser
      ? <Outlet />
      : <Navigate to="/login" state={{ from: location }} replace />
  )
} 
import { useAuth } from "../../hooks/useAuth";
import { Navigate, Outlet, useLocation } from "react-router";
import { useSelector } from "react-redux";

export const RequireAuth = () => {
  const { auth } = useAuth();
  const location = useLocation();
  
  const token = localStorage.getItem("token");
  
  
  return(
    // auth?.user
    token
      ? <Outlet />
      : <Navigate to="/login" state={{ from: location }} replace />
  )
} 
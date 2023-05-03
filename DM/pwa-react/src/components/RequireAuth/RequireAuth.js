import { useAuth } from "../../hooks/useAuth";
import { Navigate, Outlet, useLocation } from "react-router";
import { useSelector } from "react-redux";
import { selectUser } from "../../services/authSlice";

export const RequireAuth = () => {
  // const { auth } = useAuth();
  const location = useLocation();
  const currentUser = useSelector(selectUser);
  
  return(
    currentUser
      ? <Outlet />
      : <Navigate to="/login" state={{ from: location }} replace />
  )
} 
import { useSelector } from "react-redux";
import { selectCurrentUser } from "../services/authSlice";

export const useAuth = () => {
  const currentUser = useSelector(selectCurrentUser);
  const fullName = currentUser !== null ? `${currentUser.name} ${currentUser.lastName}` : " ";
  
  return { fullName }
};

 
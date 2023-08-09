import * as React from "react";
import { useNavigate } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { logout, selectCurrentUser } from "../../../services/authSlice";
import { Controls } from "../../controls/Controls";
import ProfileForm from "./components/ProfileForm";
import "../../layout/Layout.css";

const buttonStyle = {
  width: "200px",
  backgroundColor: "#C32A2A",
  color: "#FFF",
  border: "none"
};

export const ProfilePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const currentUser = useSelector(selectCurrentUser);

  const handleLogout = () => {
    dispatch(logout());
    navigate(`/login`);
  };

  return (
    <div className="component-container">
      <div className="header-toolbar">
        <div className="header-title">Профиль</div>
      </div>
      <ProfileForm user={currentUser} />
      <Controls.Button
        className="ml-0 mt-3"
        size="large"
        style={buttonStyle}
        onClick={handleLogout}
      >Выйти</Controls.Button>
    </div>
  );
};

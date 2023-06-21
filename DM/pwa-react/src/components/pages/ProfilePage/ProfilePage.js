import * as React from "react";
import { useNavigate } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { logout, selectCurrentUser } from "../../../services/authSlice";
import { Controls } from "../../controls/Controls";
import ProfileForm from "./components/ProfileForm";
import "../../layout/Layout.css";

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
      <div
        style={{
          width: "200px"
        }}
      >
        <Controls.Button
          className="ml-0 mt-3"
          size="large"
          fullWidth
          color="warning"
          style={{
            backgroundColor: "#C32A2A",
            color: "#FFF",
            border: "none"
          }}
          onClick={handleLogout}
        >Выйти</Controls.Button>
      </div>
    </div>
  );
};

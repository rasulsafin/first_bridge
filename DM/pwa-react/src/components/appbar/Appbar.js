import * as FaIcons from "react-icons/fa";
import { DropdownButton } from "react-bootstrap";
import { MenuItem } from "@mui/material";
import { removeAuthUser } from "../../services/authSlice";
import { React } from "react";
import "./Appbar.css";
import { useNavigate } from "react-router";
import { useDispatch, useSelector } from "react-redux";

export const Appbar = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const userAuth = useSelector((state) => state.auth.name);


  const toSignIn = () => {
    navigate(`/login`);
  };

  const toProfile = () => {
    navigate(`/profile`);
  };

  const toSettings = () => {
    navigate(`/settings`);
  };

  return (
    <>
      <div className="navbar">
        <div className="profile">
          <FaIcons.FaUserCircle size={40} className="avatar" />
          <DropdownButton className="dropdown" title={"Profile"}>
            {!userAuth &&
              <MenuItem onClick={toSignIn}>Sign in</MenuItem>}
            <MenuItem onClick={toProfile}>Profile</MenuItem>
            <MenuItem onClick={toSettings}>Settings</MenuItem>
            {userAuth &&
              <>
                <MenuItem divider />
                <MenuItem onClick={() => dispatch(removeAuthUser())}>Log out</MenuItem>
              </>}
          </DropdownButton>
        </div>
      </div>
    </>
  );
};
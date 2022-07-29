import { Link } from "react-router-dom";
import * as FaIcons from "react-icons/fa";
import { React, useEffect, useState } from "react";
import { SidebarData } from "./SidebarData";
import { IconContext } from "react-icons";
import "./Sidebar.css";
import { DropdownButton } from "react-bootstrap";
import { MenuItem } from "@mui/material";
import { useNavigate } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { removeAuthUser } from "../../services/authSlice";

function SideBar(props) {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [sidebar, setSidebar] = useState(false);
  const showSidebar = () => setSidebar(!sidebar);
  const userAuth = useSelector((state) => state.auth.name);

  useEffect(() => {
    props.onCollapse(sidebar);
  }, [sidebar]);

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
      <IconContext.Provider value={{ color: "aliceblue" }}>
        <div className="navbar">
          <Link to="#" className="menu-bars">
            <FaIcons.FaBars onClick={showSidebar} />
          </Link>

          <div className="profile">
            <FaIcons.FaUserCircle size={40} className="avatar" />
            <DropdownButton className="dropdown" title={"Profile"}>
              {!userAuth ?
                <MenuItem onClick={toSignIn}>Sign in</MenuItem>
                : null}
              <MenuItem onClick={toProfile}>Profile</MenuItem>
              <MenuItem onClick={toSettings}>Settings</MenuItem>
              {userAuth ?
                <>
                  <MenuItem divider />
                  <MenuItem onClick={() => dispatch(removeAuthUser())}>Log out</MenuItem>
                </>
                : null}
            </DropdownButton>
          </div>
        </div>
        <nav className={sidebar ? "nav-menu active" : "nav-menu"}>
          <ul className="nav-menu-items">
            {SidebarData.map((item, index) => {
              return (
                <li key={index} className={item.cName}>
                  <Link to={item.path} onClick={showSidebar}>
                    {item.icon}
                    <span>{item.title}</span>
                  </Link>
                </li>
              );
            })}
          </ul>
        </nav>
      </IconContext.Provider>
    </>
  );
}

export default SideBar;
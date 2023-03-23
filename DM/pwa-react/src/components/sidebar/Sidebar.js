import { Link } from "react-router-dom";
import { useState } from "react";
import { SidebarData } from "./SidebarData";
import { Avatar, Tooltip } from "@mui/material";
import "./Sidebar.css";
import { Controls } from "../controls/Controls";

import KeyboardDoubleArrowRightIcon from "@mui/icons-material/KeyboardDoubleArrowRight";
import KeyboardDoubleArrowLeftIcon from "@mui/icons-material/KeyboardDoubleArrowLeft";
import { useLocation } from "react-router";

export function Sidebar(props) {
  const [open, setOpen] = useState(false);
  const location = useLocation();
  const { pathname } = location;
  
  const toggleOpen = () => {
    setOpen(!open);
    props.onCollapse(!open);
  };

  return (
    <>
      <div className={open ? "sidenav" : "sidenavClosed"}>
        <div className="box-items">
          {SidebarData.map(item => {
            return <Tooltip key={item.title} title={item.title} placement="right">
              <Link key={item.id} className={`sideItem ${pathname === item.path ? "active" : ""}`} to={item.path}>
                {pathname === item.path ? item.iconActive : item.icon}
                {open &&
                  <span className="linkText">{item.title}</span>
                }
              </Link>
            </Tooltip>;
          })}
          {/*<button className="menuBtn" onClick={toggleOpen}>*/}
          {/*  {open ? <KeyboardDoubleArrowLeftIcon /> : <KeyboardDoubleArrowRightIcon />}*/}
          {/*</button>*/}
        </div>
        <Controls.Avatar
        />
        {/*<Avatar className="avatar-sidebar"/>*/}
      </div>
    </>
  );
}

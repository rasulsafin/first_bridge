import React, { useMemo } from "react";
import { Link } from "react-router-dom";
import { Tooltip } from "@mui/material";
import "./Sidebar.css";
import { useLocation } from "react-router";
import { SidebarData } from "./SidebarData";
import { Controls } from "../controls/Controls";
import { useAuth } from "../../hooks/useAuth";

export function Sidebar() {
  const location = useLocation();
  const { pathname } = location;
  const { fullName } = useAuth();

  const sidebarItems = useMemo(() => {
    return SidebarData.map(item => (
      <Tooltip key={item.title} title={item.title} placement="right">
        <Link
          key={item.id}
          className={`sideItem ${pathname === item.path || pathname === item.innerPath ? "active" : ""}`}
          to={item.path}
        >
          {pathname === item.path || pathname === item.innerPath ? item.iconActive : item.icon}
        </Link>
      </Tooltip>
    ));
  }, [pathname]);

  return (
    <div className="sidenav">
      <div className="box-items">{sidebarItems}</div>
      <Link className={`sideAvatar ${pathname === "/profile" ? "active" : ""}`} to="/profile">
        <Controls.Avatar fullName={fullName} />
      </Link>
    </div>
  );
}

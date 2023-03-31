import { Link } from "react-router-dom";
import { SidebarData } from "./SidebarData";
import { Avatar, Tooltip } from "@mui/material";
import "./Sidebar.css";
import { Controls } from "../controls/Controls";
import { useLocation } from "react-router";

export function Sidebar() {
  const location = useLocation();
  const { pathname } = location;
  
  return (
    <>
      <div className="sidenav">
        <div className="box-items">
          {SidebarData.map(item => {
            return <Tooltip key={item.title} title={item.title} placement="right">
              <Link key={item.id} className={`sideItem ${pathname === item.path ? "active" : ""}`} to={item.path}>
                {pathname === item.path ? item.iconActive : item.icon}
              </Link>
            </Tooltip>;
          })}
        </div>
        <Controls.Avatar
        />
        {/*<Avatar className="avatar-sidebar"/>*/}
      </div>
    </>
  );
}

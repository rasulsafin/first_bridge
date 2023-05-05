import { Link } from "react-router-dom";
import { Tooltip } from "@mui/material";
import "./Sidebar.css";
import { useLocation } from "react-router";
import { useSelector } from "react-redux";
import { selectUser } from "../../services/authSlice";
import { SidebarData } from "./SidebarData";
import { Controls } from "../controls/Controls";

export function Sidebar() {
  const location = useLocation();
  const { pathname } = location;
  const user = useSelector(selectUser);

  const fullName = user !== null ? `${user.name} ${user.lastName}` : " ";

  return (
    <div className="sidenav">
      <div className="box-items">
        {SidebarData.map(item => {
          return <Tooltip key={item.title} title={item.title} placement="right">
            <Link
              key={item.id}
              className={`sideItem ${pathname === item.path || pathname === item.innerPath ? "active" : ""}`}
              to={item.path}
            >
              {pathname === item.path || pathname === item.innerPath ? item.iconActive : item.icon}
            </Link>
          </Tooltip>;
        })}
      </div>
      <Link className={`sideAvatar ${pathname === "/profile" ? "active" : ""}`} to="/profile">
        <Controls.Avatar
          fullName={fullName}
        />
      </Link>
    </div>
  );
}

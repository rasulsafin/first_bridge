import { Link } from "react-router-dom";
import { SidebarData } from "./SidebarData";
import { Tooltip } from "@mui/material";
import "./Sidebar.css";
import { Controls } from "../controls/Controls";
import { useLocation } from "react-router";
import { useSelector } from "react-redux";
import { selectUser } from "../../services/authSlice";

export function Sidebar() {
  const location = useLocation();
  const { pathname } = location;
  const user = useSelector(selectUser);
  
// TODO Add LastName from user
    const fullName = user !== null ? (user.name + " " + "LastName") : " ";
  
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
        <Link className={`sideAvatar ${pathname === "/profile" ? "active" : ""}`} to={"/profile"}>
          <Controls.Avatar
            fullName={fullName}
          />
        </Link>
      </div>
    </>
  );
}

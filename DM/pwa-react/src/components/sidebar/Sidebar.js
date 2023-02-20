import { Link } from "react-router-dom";
import { useState } from "react";
import { SidebarData } from "./SidebarData";
import { IconContext } from "react-icons";
import { Avatar, Tooltip } from "@mui/material";
import "./Sidebar.css";
import { Controls } from "../controls/Controls";

import KeyboardDoubleArrowRightIcon from "@mui/icons-material/KeyboardDoubleArrowRight";
import KeyboardDoubleArrowLeftIcon from "@mui/icons-material/KeyboardDoubleArrowLeft";

export function Sidebar(props) {
  const [open, setOpen] = useState(false);

  const toggleOpen = () => {
    setOpen(!open);
    props.onCollapse(!open);
  };

  const name = "Stttue Wertqqy";
  
  return (
    <>
      <IconContext.Provider value={{ size: 32 }}>
        <div className={open ? "sidenav" : "sidenavClosed"}>
          <div className="box-items">
          {SidebarData.map(item => {
            return <Tooltip key={item.title} title={item.title} placement="right">
              <Link key={item.id} className="sideItem" to={item.path}>
                {item.icon}
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
          name={name}
          />
          {/*<Avatar className="avatar-sidebar"/>*/}
        </div>
      </IconContext.Provider>
    </>
  );
}

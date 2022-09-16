import { Link } from "react-router-dom";
import { React, useEffect, useState } from "react";
import { SidebarData } from "./SidebarData";
import { IconContext } from "react-icons";
import "./Sidebar.css";
import KeyboardDoubleArrowRightIcon from "@mui/icons-material/KeyboardDoubleArrowRight";
import KeyboardDoubleArrowLeftIcon from "@mui/icons-material/KeyboardDoubleArrowLeft";

export function Sidebar(props) {
  const [open, setOpen] = useState(false);

  const toggleOpen = () => {
    setOpen(!open);
  };

  useEffect(() => {
    props.onCollapse(open);
  }, [open]);

  return (
    <>
      <IconContext.Provider value={{ color: "aliceblue", size: 28 }}>
        <div className={open ? "sidenav" : "sidenavClosed"}>

          {SidebarData.map(item => {
            return <Link key={item.id} className="sideItem" to={item.path}>
              {item.icon}
              {open &&
                <span className="linkText">{item.title}</span>
              }
            </Link>;
          })}

          <button className="menuBtn" onClick={toggleOpen}>
            {open ? <KeyboardDoubleArrowLeftIcon /> : <KeyboardDoubleArrowRightIcon />}
          </button>
        </div>
      </IconContext.Provider>
    </>
  );
}

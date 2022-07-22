import {Link} from "react-router-dom";
import * as FaIcons from "react-icons/fa";
import { React, useEffect, useState } from "react";
import {SidebarData} from "./SidebarData";
import { IconContext } from "react-icons";
import "./Sidebar.css"
import { DropdownButton } from "react-bootstrap";
import { MenuItem } from "@mui/material";

function SideBar(props) {
  const [sidebar, setSidebar] = useState(false)
  const showSidebar = () => setSidebar(!sidebar)

  useEffect(() => {
    props.onCollapse(sidebar);
  }, [sidebar]);
  
  return (
    <>
      <IconContext.Provider value={{color: 'aliceblue'}}>
        <div className="navbar">
          <Link to="#" className='menu-bars'>
            <FaIcons.FaBars onClick={showSidebar}/>
          </Link>

          <div className="profile" >
            <FaIcons.FaUserCircle size={40} className="avatar" />
            <DropdownButton className="dropdown" title={"Profile"}>
              <MenuItem eventKey="1" href="/profile">Profile</MenuItem>
              <MenuItem eventKey="2">Settings</MenuItem>
              <MenuItem divider />
              <MenuItem eventKey="3">Log out</MenuItem>
            </DropdownButton>
          </div>
          
        </div>
        <nav className={sidebar ? "nav-menu active" : "nav-menu"}>
          <ul className='nav-menu-items'>
            {SidebarData.map((item, index) => {
              return (
                <li key={index} className={item.cName}>
                  <Link to={item.path} onClick={showSidebar}>
                    {item.icon}
                    <span>{item.title}</span>
                  </Link>
                </li>
              )
            })}
          </ul>
        </nav>
      </IconContext.Provider>
    </>
  )
}

export default SideBar
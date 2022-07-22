import * as FaIcons from "react-icons/fa";
import * as AaIcons from "react-icons/ai";
import * as IoIcons from "react-icons/io";

export const SidebarData = [
  {
    id: 1,
    title: 'Home',
    path: '/',
    icon: <AaIcons.AiFillHome />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
  {
    id: 2,
    title: 'Users',
    path: '/users',
    icon: <IoIcons.IoIosPaper />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
  {
    id: 3,
    title: 'Projects',
    path: '/projects',
    icon: <FaIcons.FaCartPlus />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
  {
    id: 4,
    title: 'Records',
    path: '/records',
    icon: <IoIcons.IoMdPeople />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
  {
    id: 5,
    title: 'Items',
    path: '/items',
    icon: <FaIcons.FaEnvelopeOpenText />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
  {
    id: 6,
    title: 'Support',
    path: '/support',
    icon: <IoIcons.IoMdHelpCircle />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
  {
    id: 6,
    title: 'Login',
    path: '/login',
    icon: <IoIcons.IoMdHelpCircle />,
    cName: 'nav-text',
    sName: 'sidebar-item',
    nName: 'nav-item',
  },
]
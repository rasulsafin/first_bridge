import * as FaIcons from "react-icons/fa";
import * as AaIcons from "react-icons/ai";
import * as IoIcons from "react-icons/io";

export const SidebarData = [
  {
    id: 1,
    title: 'Home',
    path: '/',
    icon: <AaIcons.AiFillHome />,
  },
  {
    id: 2,
    title: 'Users',
    path: '/users',
    icon: <IoIcons.IoMdPeople />,
  },
  {
    id: 3,
    title: 'Organizations',
    path: '/organizations',
    icon: <IoIcons.IoIosPaper />,
  },
  {
    id: 4,
    title: 'Projects',
    path: '/projects',
    icon: <FaIcons.FaEnvelopeOpenText />,
  },
  {
    id: 5,
    title: 'Admin',
    path: '/admin',
    icon: <IoIcons.IoMdHelpCircle />,
  },
]
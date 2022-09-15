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
    icon: <IoIcons.IoIosPaper />,
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
    icon: <FaIcons.FaCartPlus />,
  },
  {
    id: 5,
    title: 'Records',
    path: '/records',
    icon: <IoIcons.IoMdPeople />,
  },
  {
    id: 6,
    title: 'Items',
    path: '/items',
    icon: <FaIcons.FaEnvelopeOpenText />,
  },
  {
    id: 7,
    title: 'Support',
    path: '/support',
    icon: <IoIcons.IoMdHelpCircle />,
  },
  {
    id: 8,
    title: 'Login',
    path: '/login',
    icon: <IoIcons.IoMdHelpCircle />,
  },
  {
    id: 9,
    title: 'Generate Form',
    path: '/generate-form',
    icon: <IoIcons.IoMdHelpCircle />,
  },
  {
    id: 10,
    title: 'Admin',
    path: '/admin',
    icon: <IoIcons.IoMdHelpCircle />,
  },
]
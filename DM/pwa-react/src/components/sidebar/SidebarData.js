import { ReactComponent as ProjectIcon } from "../../assets/icons/projects.svg";
import { ReactComponent as UserIcon } from "../../assets/icons/users.svg";
import { ReactComponent as RecordIcon } from "../../assets/icons/records.svg";
import { ReactComponent as ModelIcon } from "../../assets/icons/models.svg";
import { ReactComponent as TemplateIcon } from "../../assets/icons/templates.svg";
import { ReactComponent as DocIcon } from "../../assets/icons/docs.svg";

export const SidebarData = [
  
  {
    id: 1,
    title: 'Projects',
    path: '/projects',
    icon: <ProjectIcon className="icon" />,
    iconActive: <ProjectIcon className="icon active" />,
  },
  {
    id: 2,
    title: 'Records',
    path: '/records',
    icon: <RecordIcon className="icon" />,
    iconActive: <RecordIcon className="icon active" />,
  },
  {
    id: 3,
    title: 'Models',
    path: '/organizations',
    icon: <ModelIcon className="icon" />,
    iconActive: <ModelIcon className="icon active" />,
  },
  {
    id: 4,
    title: 'Users',
    path: '/users',
    icon: <UserIcon className="icon" />,
    iconActive: <UserIcon className="icon active" />,
  },
  {
    id: 5,
    title: 'Documents',
    path: '/',
    icon: <DocIcon className="icon" />,
    iconActive: <DocIcon className="icon active" />,
  },
  {
    id: 6,
    title: 'Templates',
    path: '/admin',
    icon: <TemplateIcon className="icon" />,
    iconActive: <TemplateIcon className="icon active" />,
  },
]
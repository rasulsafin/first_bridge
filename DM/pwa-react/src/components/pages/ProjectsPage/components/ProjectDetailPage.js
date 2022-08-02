import { useParams } from "react-router";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { selectAllProjects } from "../../../../services/projectsSlice";
import { BsArrowLeftSquareFill } from "react-icons/bs";

export const ProjectDetailPage = () => {
  const { id } = useParams();
  const projects = useSelector(selectAllProjects);

  const project = projects.find(project => project.id === Number(id));

  return (
    <div className="p-4">
      <Link to="/projects">
        <span style={{ color: "black", textDecoration: "none" }}>
         <BsArrowLeftSquareFill size={30} color="#1d62ad" />
        </span>
      </Link>
      <h1>Project Detail Page</h1>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {project.title}</span></p>
      </div>
    </div>
  );
};
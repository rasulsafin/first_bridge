import { useParams } from "react-router";
import { useSelector } from "react-redux";
import {Link} from "react-router-dom";
import { selectAllProjects } from "../../../../services/projectsSlice";
import { selectAllUsers } from "../../../../services/usersSlice";

export const ProjectDetailPage = () => {
  const { id } = useParams();
  const projects = useSelector(selectAllProjects)
  const users = useSelector(selectAllUsers)

  const project = projects.find(project => project.id === Number(id));

  const usersOptions = users.map(user => (
    <option key={user.id} value={user.id}>
      {user.name}
    </option>
  ))
  
  return (
    <div className="p-4">
      <Link to="/projects">
        <span style={{ color: "black", textDecoration: "none" }}>
          back
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

      {/*<label htmlFor="UserProject">Author:</label>*/}
      {/*<select id="UserProject">*/}
      {/*  <option value=""></option>*/}
      {/*  {usersOptions}*/}
      {/*</select>*/}
    </div>
  );
};
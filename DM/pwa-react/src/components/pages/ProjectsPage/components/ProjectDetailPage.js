import { useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { selectAllProjects } from "../../../../services/projectsSlice";
import { BsArrowLeftSquareFill } from "react-icons/bs";
import { Button, Toolbar } from "@mui/material";
import RecordsGrid from "../../RecordsPage/components/RecordsGrid";

export const ProjectDetailPage = () => {
  const { id } = useParams();
  const projects = useSelector(selectAllProjects);
  const project = projects.find(project => project.id === Number(id));

  return (
    <div className="p-3">
      <div>
        <Toolbar>
          <Link to="/projects">
        <span style={{ color: "black", textDecoration: "none" }}>
         <BsArrowLeftSquareFill size={30} color="#1d62ad" />
        </span>
          </Link>
          <Button className="ml-o m-3" size="small" variant="outlined">Add Record</Button>
          <Button className="m-3" size="small" variant="outlined">Add Template</Button>
          <Button className="m-3" size="small" variant="outlined">Add User</Button>
          <Button className="m-3" size="small" variant="outlined">Add Item</Button>
        </Toolbar></div>
     
      <div style={{
        padding: 5
      }}>
        <p><span style={{ fontSize: 24 }}> {project.title}</span></p>
      </div>
      <div style={{
        marginTop: 10,
        display: "flex",
        flexDirection: "row"
      }}>
        <p><span 
          style={{
          fontSize: 24,
          paddingRight: 15,
        }}>Records:</span></p>
        
      </div>
      <RecordsGrid projectId={id} />
    </div>
  );
};
import { useLocation, useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { selectAllProjects } from "../../../../services/projectsSlice";
import { BsArrowLeftSquareFill } from "react-icons/bs";
import { Button, Toolbar } from "@mui/material";
import RecordsGrid from "../../RecordsPage/components/RecordsGrid";
import { BiArrowBack } from "react-icons/bi";
import { FilesPage } from "../../FilesPage/FilesPage";

export const ProjectDetailPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const projects = useSelector(selectAllProjects);
  const project = projects.find(project => project.id === Number(id));
  localStorage.setItem("projectId", id);

  const goBack = () => {
    navigate(-1);
  }
   
  function handleToCreateTemplatePage() {
    navigate(`/template/create`);
  }

  function handleToCreateRecordPage() {
    navigate(`/record/create`);
  }

  function handleToFilesPage() {
    navigate(`/project/${id}/files`);
  }

  return (
    <div className="p-3">
      <div>
        <Toolbar>
            <Button className="ml-o m-3" onClick={goBack} size="small" variant="outlined">
              <BiArrowBack size={24} color="#1d62ad" /></Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToCreateRecordPage}>Add Record</Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToCreateTemplatePage}>Add Template</Button>
          <Button className="m-3" size="small" variant="outlined"  onClick={handleToFilesPage}>Files</Button>
          <Button className="m-3" size="small" variant="outlined">Add Item</Button>
        </Toolbar>
      </div>
     <hr />
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
import { Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Controls } from "../../../controls/Controls";


export const ProjectEditPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  const goBack = () => {
    navigate(-1);
  };

  return (
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Edit project</h3>
    </div>
  );
};
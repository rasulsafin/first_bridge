import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router";

export const UserEditPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();

  console.log(id);
  
  const goBack = () => {
    navigate(-1);
  };

  return (
    <div  className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Button>
      </Toolbar>
      <hr />
      <h3>Edit User</h3>
    </div>
  )
}
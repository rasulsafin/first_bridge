import { Button, Toolbar } from "@mui/material";
import { useNavigate } from "react-router";

export const AdminPage = () => {
  const navigate = useNavigate();
  
  function handleToUsersPage() {
    navigate(`/users`);
  }

  function handleToOrgPage() {
    navigate(`/organizations`);
  }

  return (
    <div className="component-container">
      <div>
        <Toolbar>
          <Button className="ml-o m-3" size="small" variant="outlined" onClick={handleToUsersPage}>Users</Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToOrgPage}>Organizations</Button>
        </Toolbar>
      </div>
      <hr />
      <h3>Admin Page</h3>
      <h3> {localStorage.getItem("user")}</h3>
    </div>
  );
};

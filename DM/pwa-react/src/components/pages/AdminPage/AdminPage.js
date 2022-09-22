import UsersGrid from "../UsersPage/components/UsersGrid";
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
    <div className="p-3">
      <div>
        <Toolbar>
          <Button className="ml-o m-3" size="small" variant="outlined" onClick={handleToUsersPage}>Users</Button>
          <Button className="m-3" size="small" variant="outlined" onClick={handleToOrgPage}>Organizations</Button>
          <Button className="m-3" size="small" variant="outlined">Something else</Button>
          <Button className="m-3" size="small" variant="outlined">WAT</Button>
        </Toolbar>
      </div>
      <hr />
      <h1>Admin Page</h1>
      <h2> {localStorage.getItem("user")}</h2>
    </div>
  );
};

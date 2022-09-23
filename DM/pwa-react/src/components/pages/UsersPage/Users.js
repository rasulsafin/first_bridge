import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";
import { useNavigate } from "react-router";
import UsersGrid from "./components/UsersGrid";

export const Users = () => {
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  function handleToCreateUserPage() {
    navigate(`/user/create`);
  }

  return (
    <div className="p-3">
      <Toolbar>
        <Button className="m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
        <Button className="m-3" size="small" variant="outlined" onClick={handleToCreateUserPage}>Add user</Button>
      </Toolbar>
      <hr />
      <h3 className="mb-4">Users</h3>
      <UsersGrid />
    </div>
  )
}

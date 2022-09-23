import OrganizationsGrid from "./components/OrganizationsGrid";
import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";
import { useNavigate } from "react-router";

export const Organizations = () => {
  const navigate = useNavigate();
  
  const goBack = () => {
    navigate(-1);
  };
  
  return (
    <div className="p-3">
      <Toolbar>
        <Button className="m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Button>
        <Button className="m-3" size="small" variant="outlined">Add organizations</Button>
      </Toolbar>
      <hr />
      <h3 className="mb-4">Organizations</h3>
      <OrganizationsGrid />
    </div>
  )
}

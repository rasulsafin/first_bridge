import { useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { BsArrowLeftSquareFill } from "react-icons/bs";
import { selectAllOrganizations } from "../../../../services/organizationsSlice";
import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import * as React from "react";

export const OrganizationDetailPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const organizations = useSelector(selectAllOrganizations);

  const organization = organizations.find(org => org.id === Number(id));

  const goBack = () => {
    navigate(-1);
  };
  
  return (
    <div className="p-3">
      <Toolbar>
        <Button className="m-3" onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Button>
        <Button className="m-3" size="small" variant="outlined">Edit organization</Button>
        <Button className="m-3" size="small" variant="outlined" color="error">Delete organization</Button>
      </Toolbar>
      <hr />
      <h3>Project Detail Page</h3>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {organization.name}</span></p>
      </div>
    </div>
  );
};
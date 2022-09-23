import { Controls } from "../../../controls/Controls";
import { Button, Toolbar } from "@mui/material";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { addNewProject } from "../../../../services/projectsSlice";
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi";
import { addNewOrganization } from "../../../../services/organizationsSlice";

export const OrganizationCreatePage = () => {
  const dispatch = useDispatch();
  const [title, setTitle] = useState("");
  const [desc, setDesc] = useState("");
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  const organizationId = localStorage.getItem("organizationId");

  function createOrg() {
      dispatch(addNewOrganization({
        name: title,
        address: desc,
        inn: organizationId,
        ogrn: organizationId,
        kpp: organizationId,
        phone: organizationId,
        email: organizationId,
      }))
  }

  return (
    <div  className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
      </Toolbar>
      <hr />
      <div style={{
        display: "flex",
        flexDirection: "column"
      }}>
        <h3>Create organization</h3>
        <Controls.Input
          name="title"
          label="title"
          type="text"
          onChange={(event) => setTitle(event.target.value)}
          required
        />
        <Controls.Input
          name="description"
          label="description"
          type="text"
          onChange={(event) => setDesc(event.target.value)}
          required
        />
      </div>
      <Button
        className="m-3"
        size="small"
        variant="outlined"
        onClick={createOrg}>
        Add organization
      </Button>
    </div>
  );
};
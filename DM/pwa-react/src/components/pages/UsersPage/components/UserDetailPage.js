import { useNavigate, useParams } from "react-router";
import { useSelector } from "react-redux";
import { selectAllUsers } from "../../../../services/usersSlice";
import * as React from "react";
import { Controls } from "../../../controls/Controls";
import { useState } from "react";
import ProjectsPermissionsGrid from "../../AdminPage/ProjectsPermissionsGrid";
import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
// import RecordsPermissionsGrid from "../../AdminPage/RecordsPermissionsGrid";

export const UserDetailPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const users = useSelector(selectAllUsers);
  const [type, setType] = useState();

  const user = users.find(user => user.id === Number(id));

  const objectTypes = [
    { id: 1, name: "project" },
    { id: 2, name: "record" },
    { id: 3, name: "item" }
  ];

  const goBack = () => {
    navigate(-1);
  };

  const handleChange = (event) => {
    setType(event.target.value);
  };

  const objects = () => {
    switch (type) {
      case "project":
        return <ProjectsPermissionsGrid userId={user.id} type={1} />;
      case "record":
        return <tr>Record</tr>; //<RecordsPermissionsGrid />;
      case "item":
        return <tr>Item</tr>;
      default:
        return;
    }
  };

  return (
    <div className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
      </Toolbar>
      <hr />
      <h1>User Detail Page</h1>

      <div>
        <div style={{
          border: "black solid 2px",
          width: "40vh",
          background: "whitesmoke",
          padding: 5,
          marginBottom: 30
        }}>
          <p>Name: <span style={{ fontSize: 24 }}> {user.name}</span></p>
          <p>Login: <span style={{ fontSize: 24 }}> {user.login}</span></p>
          <p>Role: <span style={{ fontSize: 24 }}> {user.roles}</span></p>
          <p>Email: <span style={{ fontSize: 24 }}> {user.email}</span></p>
        </div>
      </div>
      <div>
        <h3>Permissions</h3>
      </div>
      <div>
        <Controls.Select
          name="type"
          label="type"
          options={objectTypes}
          onChange={(event) => handleChange(event)}
          required
        />
      </div>
      <div>
        {objects()}
      </div>
    </div>
  );
};
import { useParams } from "react-router";
import { useSelector } from "react-redux";
import { selectAllUsers } from "../../../../services/usersSlice";
import { Link } from "react-router-dom";
import { BsArrowLeftSquareFill } from "react-icons/bs";
import * as React from "react";
import { Controls } from "../../../controls/Controls";
import { useState } from "react";
import ProjectsPermissionsGrid from "../../AdminPage/ProjectsPermissionsGrid";
// import RecordsPermissionsGrid from "../../AdminPage/RecordsPermissionsGrid";

export const UserDetailPage = () => {
  const { id } = useParams();
  const users = useSelector(selectAllUsers);
  const [type, setType] = useState();

  const user = users.find(user => user.id === Number(id));

  const objectTypes = [
    { id: 1, name: "project" },
    { id: 2, name: "record" },
    { id: 3, name: "item" }
  ];

  const handleChange = (event) => {
    setType(event.target.value);
  };

  const objects = () => {
    switch (type) {
      case "project":
        return <ProjectsPermissionsGrid userId={user.id} type={1} />;
      case "record":
        return <tr>Records</tr>; //<RecordsPermissionsGrid />
      case "item":
        return <tr>Item</tr>;
      default:
        return;
    }
  };

  return (
    <div className="p-4">
      <Link to="/users">
        <span style={{ color: "black", textDecoration: "none" }}>
          <BsArrowLeftSquareFill size={30} color="#1d62ad" />
        </span>
      </Link>
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
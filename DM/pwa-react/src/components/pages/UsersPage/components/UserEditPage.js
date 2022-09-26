import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Controls } from "../../../controls/Controls";
import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { EditUser, selectAllUsers } from "../../../../services/usersSlice";
import { useState } from "react";

export const UserEditPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { id } = useParams();
  const users = useSelector(selectAllUsers);

  const user = users.find(user => user.id === Number(id));

  const initialValues = {
    name: user.name,
    lastName: user.lastName,
    fathersName: user.fathersName,
    login: user.login,
    email: user.email,
    password: user.password,
    roles: user.roles,
    birthdate: user.birthdate,
    snils: user.snils,
    position: user.position,
    organizationId: user.organizationId
  };

  const [values, setValues] = useState(initialValues);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value
    });
  };

  function createUser() {
    dispatch(EditUser({
      userId: id,
      name: values.name,
      // lastName: values.lastName,
      // fathersName: values.fathersName,
      login: values.login,
      email: values.email
      // password: values.password,
      // roles: values.roles,
      // birthdate: values.birthdate,
      // snils: values.snils,
      // position: values.position,
      // organizationId: values.organizationId
    }));
    navigate(`/users`);
  }

  const goBack = () => {
    navigate(-1);
  };

  return (
    <div className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" />
        </Button>
      </Toolbar>
      <hr />
      <h3>Edit User</h3>
      <div className="col-10" style={{
        display: "flex",
        justifyContent: "flex-start",
        alignItems: "center",
        flexWrap: "wrap"
      }}>
        <Controls.Input
          name="name"
          label="Name"
          type="text"
          value={values.name}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="lastName"
          label="lastName"
          type="text"
          value={values.lastName}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="fathersName"
          label="fathersName"
          type="text"
          value={values.fathersName}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="login"
          label="Login"
          type="text"
          value={values.login}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="email"
          label="Email"
          type="email"
          value={values.email}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="password"
          label="Password"
          type="text"
          value={values.password}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="roles"
          label="roles"
          type="text"
          value={values.roles}
          onChange={handleInputChange}
        />
        <Controls.DatePicker
          name="birthdate"
          label="birthdate"
          value={values.birthdate}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="snils"
          label="snils"
          type="text"
          value={values.snils}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="position"
          label="position"
          type="text"
          value={values.position}
          onChange={handleInputChange}
        />
        <Controls.Input
          name="organizationId"
          label="organizationId"
          type="text"
          value={values.organizationId}
          onChange={handleInputChange}
        />
      </div>
      <div>
        <Button
          className="m-3"
          size="small"
          variant="outlined"
          onClick={createUser}>
          Save changes
        </Button>
      </div>
    </div>
  );
};
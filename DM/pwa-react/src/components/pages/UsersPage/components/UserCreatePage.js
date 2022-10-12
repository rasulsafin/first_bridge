import { Controls } from "../../../controls/Controls";
import { Button, Toolbar } from "@mui/material";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi";
import { addNewUser } from "../../../../services/usersSlice";
import * as React from "react";
import { openSnackbar } from "../../../../services/snackbarSlice";
import CreateUserForm from "./UserForm";
import { getInitialValues } from "../utils/getInitialValues";

export const UserCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const initialValues = getInitialValues();
  
  const goBack = () => {
    navigate(-1);
  };

  return (
    <div className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
      </Toolbar>
      <hr />
      <h3>Create user</h3>
      <CreateUserForm
        initialValues={initialValues}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(addNewUser({
            name: values.name,
            lastName: values.lastName,
            fathersName: values.fathersName,
            login: values.login,
            email: values.email,
            password: values.password,
            roles: values.roles,
            birthdate: values.birthdate,
            snils: values.snils,
            position: values.position,

            //TODO to get orgId
            organizationId: "1"
          }));
          dispatch(openSnackbar());
          formikHelpers.resetForm();
          navigate(`/users`);
        }}
      />
    </div>
  );
};
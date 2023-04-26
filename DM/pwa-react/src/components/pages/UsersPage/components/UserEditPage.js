import { Toolbar } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Controls } from "../../../controls/Controls";
import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { editUser, selectAllUsers } from "../../../../services/usersSlice";
import { getUpdateInputDataFromValues } from "../utils/getUpdateInputDataFromValues";
import UserForm from "./UserForm";

export const UserEditPage = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const { id } = useParams();
  const users = useSelector(selectAllUsers);
  const textButton = "Save changes";
  const user = users.find(user => user.id === Number(id));
  const initialValues = getUpdateInputDataFromValues(user);

  const goBack = () => {
    navigate(-1);
  };

  return (
    <div>
      <Toolbar>
        <Controls.Button onClick={goBack}>
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Edit User</h3>
      <UserForm
        textButton={textButton}
        initialValues={initialValues}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(editUser(values));
          formikHelpers.resetForm();
          navigate(`/users`);
        }}
      />
    </div>
  );
};
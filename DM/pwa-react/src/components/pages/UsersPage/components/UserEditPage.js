import { Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Controls } from "../../../controls/Controls";
import * as React from "react";
import { useDispatch, useSelector } from "react-redux";
import { EditUser, selectAllUsers } from "../../../../services/usersSlice";
import { openSnackbar } from "../../../../services/snackbarSlice";
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
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack}>
          <BiArrowBack size={24} color="#1d62ad" />
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Edit User</h3>
      <UserForm
        textButton={textButton}
        initialValues={initialValues}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(EditUser(values));
          dispatch(openSnackbar());
          formikHelpers.resetForm();
          navigate(`/users`);
        }}
      />
    </div>
  );
};
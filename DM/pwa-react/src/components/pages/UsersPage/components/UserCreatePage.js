import { Controls } from "../../../controls/Controls";
import { Toolbar } from "@mui/material";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi";
import { addNewUser } from "../../../../services/usersSlice";
import * as React from "react";
import { openSnackbar } from "../../../../services/snackbarSlice";
import UserForm from "./UserForm";
import { getInitialValues } from "../utils/getInitialValues";

export const UserCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const initialValues = getInitialValues();
  const textButton = "Add User";
  
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
      <h3>Create user</h3>
      <UserForm
        textButton={textButton}
        initialValues={initialValues}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(addNewUser(values));
          dispatch(openSnackbar());
          formikHelpers.resetForm();
          navigate(`/users`);
        }}
      />
    </div>
  );
};
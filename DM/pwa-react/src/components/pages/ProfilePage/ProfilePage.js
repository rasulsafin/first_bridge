import * as React from "react";
import UserForm from "../UsersPage/components/UserForm";
import { getUpdateInputDataFromValues } from "../UsersPage/utils/getUpdateInputDataFromValues";
import { useDispatch, useSelector } from "react-redux";
import { selectAllUsers } from "../../../services/usersSlice";
import { logout, selectUser } from "../../../services/authSlice";
import { Controls } from "../../controls/Controls";
import { useNavigate } from "react-router";

export const ProfilePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const textButton = "Save changes";
  const currentUser = useSelector(selectUser);
  const users = useSelector(selectAllUsers);
  const user = users.find(user => user.id === Number(currentUser.id));
  const initialValues = getUpdateInputDataFromValues(user);
  
  const handleLogout = () => {
    dispatch(logout());
    navigate(`/login`);
  }
  
  return (
    <div className="component-container">
      <h3 className="mb-2">Профиль</h3>
      <UserForm
        textButton={textButton}
        initialValues={initialValues}
        onSubmit={(values, formikHelpers) => {
          formikHelpers.resetForm();
        }}
      />
      <div
      style={{
        width: "200px"
      }}
      >
        <Controls.Button
          className="ml-0 mt-3"
          size="large"
          fullWidth
          color="warning"
          style={{
            backgroundColor: "#C32A2A",
            color: "#FFF",
            border: "none"
          }}
          onClick={handleLogout}
        >Выйти</Controls.Button>
      </div>
    </div>
  );
};

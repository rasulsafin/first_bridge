import * as React from "react";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import {
  addNewUser,
  fetchUsers,
  selectAllUsers,
} from "../../../services/usersSlice";
import { useEffect, useState } from "react";
import { UserCard } from "./components/UserCard";
import "./Users.css";
import { Box, Grid } from "@mui/material";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import UserForm from "./components/UserForm";
import { getInitialValues } from "./utils/getInitialValues";
import { useNavigate } from "react-router";
import { SearchAndSortUserToolbar } from "./components/SearchAndSortUserToolbar";

export const Users = () => {
  const navigate = useNavigate();
  const [openModal, setModal] = useState(false);
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const initialValues = getInitialValues();
  const title = "Добавление участника";

  useEffect(() => {
    dispatch(fetchUsers());
  }, [dispatch]);

  const handleModalOpen = () => {
    setModal(true);
  };

  const handleModalClose = () => {
    setModal(false);
  };

  const handleNavigateToRolesPage = () => {
    navigate(`/roles`)
  }

  const iconBurger = (
    <BurgerIcon className="icon-role active" />
  );

  return (
    <>
      <div className="component-container">
        <Grid direction="row" container>
          <Grid item xs={9} sm={9} lg={9}>
            <h3 className="mb-2">Участники</h3>
          </Grid>
          <Grid container item xs={3} sm={3} lg={3} justifyContent="flex-end">
            <Grid item>
              <Controls.Button
                startIcon={iconBurger}
                className="m-0"
                style={{
                  backgroundColor: "#2D2926",
                  color: "#FFF",
                  border: "none"
                }}
              >Участники</Controls.Button>
              <Controls.Button
                startIcon={<StarIcon />}
                className="m-0"
                style={{
                  backgroundColor: "#FFF",
                  color: "#2D2926",
                  border: "none"
                }}
                onClick={handleNavigateToRolesPage}
              >Роли</Controls.Button>
            </Grid>
          </Grid>
        </Grid>
        <Box>
          <SearchAndSortUserToolbar />
        </Box>
        <div className="user-cards-container">
            {users.map(user => <UserCard key={user.id} user={user} />)}
        </div>
        <Controls.Modal
          titleModal={title}
          open={openModal}
          onClose={handleModalClose}
        >
          <UserForm
            initialValues={initialValues}
            onSubmit={(values, formikHelpers) => {
              console.log(values);
              dispatch(addNewUser(values));
              formikHelpers.resetForm();
            }}
          />
        </Controls.Modal>
        <Controls.RoundButton
          onClick={handleModalOpen}
        >
        </Controls.RoundButton>
      </div>
    </>
  );
};

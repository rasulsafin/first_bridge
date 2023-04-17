import * as React from "react";
import { SearchBar } from "../../searchBar/SearchBar";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import {
  addNewUser,
  fetchUsers,
  searchUsersByName,
  selectAllUsers,
  sortUsersByNameAsc,
  sortUsersByNameDesc
} from "../../../services/usersSlice";
import { useEffect, useState } from "react";
import { UserCard } from "./components/UserCard";
import "./Users.css";
import { Grid } from "@mui/material";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import UserForm from "./components/UserForm";
import { getInitialValues } from "./utils/getInitialValues";

export const Users = () => {
  const [openModal, setModal] = useState(false);
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const initialValues = getInitialValues();
  const title = "Добавление участника";

  useEffect(() => {
    dispatch(fetchUsers());
  }, [dispatch]);

  function filterByInput(e) {
    dispatch(searchUsersByName(e.target.value));
  }

  const handleSortByAsc = () => {
    dispatch(sortUsersByNameAsc());
  };

  const handleSortByDesc = () => {
    dispatch(sortUsersByNameDesc());
  };

  const handleModalOpen = () => {
    setModal(true);
  };

  const handleModalClose = () => {
    setModal(false);
  };

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
                startIcon={<BurgerIcon />}
                className="m-0"
                style={{
                  backgroundColor: "#2D2926",
                  color: "#FFF",
                  border: "none"
                }}
                onClick={handleSortByAsc}
              >Участники</Controls.Button>
              <Controls.Button
                startIcon={<StarIcon />}
                className="m-0"
                style={{
                  backgroundColor: "#FFF",
                  color: "#2D2926",
                  border: "none"
                }}
                onClick={handleSortByDesc}
              >Роли</Controls.Button>
            </Grid>
          </Grid>
        </Grid>
        <div className="toolbar-project">
          <SearchBar
            onChange={e => filterByInput(e)}
          />
          <div>
            <Controls.Button
              className="ml-0"
              style={{
                backgroundColor: "#2D2926",
                color: "#FFF",
                border: "none"
              }}
              onClick={handleSortByAsc}
            >От А до Я</Controls.Button>
            <Controls.Button
              style={{
                backgroundColor: "#FFF",
                color: "#2D2926",
                border: "none"
              }}
              onClick={handleSortByDesc}
            >От Я до А</Controls.Button>
          </div>
        </div>
        <div className="user-card-container">
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

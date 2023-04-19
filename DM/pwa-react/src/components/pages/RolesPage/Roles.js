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
import { Grid } from "@mui/material";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import { useNavigate } from "react-router";
import "./Roles.css";

export const Roles = () => {
  const navigate = useNavigate();
  const [openModal, setModal] = useState(false);
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const title = "Редактирование роли";

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

  const handleNavigateToUsersPage = () => {
    navigate(`/users`)
  }

  const iconStar = (
    <StarIcon className="icon-role active" />
  );

  return (
    <>
      <div className="component-container">
        <Grid direction="row" container>
          <Grid item xs={9} sm={9} lg={9}>
            <h3 className="mb-2">Роли</h3>
          </Grid>
          <Grid container item xs={3} sm={3} lg={3} justifyContent="flex-end">
            <Grid item>
              <Controls.Button
                startIcon={<BurgerIcon />}
                className="m-0"
                style={{
                  backgroundColor: "#FFF",
                  color: "#2D2926",
                  border: "none"
                }}
                onClick={handleNavigateToUsersPage}
              >Участники</Controls.Button>
              <Controls.Button
                startIcon={iconStar}
                className="m-0"
                style={{
                  backgroundColor: "#2D2926",
                  color: "#FFF",
                  border: "none"
                }}
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
          {/*{users.map(user => <UserCard key={user.id} user={user} />)}*/}
        </div>
        <Controls.Modal
          titleModal={title}
          open={openModal}
          onClose={handleModalClose}
        >
        </Controls.Modal>
        <Controls.RoundButton
          onClick={handleModalOpen}
        >
        </Controls.RoundButton>
      </div>
    </>
  );
};

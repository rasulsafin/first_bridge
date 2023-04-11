import * as React from "react";
import { SearchBar } from "../../searchBar/SearchBar";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchUsers,
  searchUsersByName,
  selectAllUsers,
  sortUsersByNameAsc,
  sortUsersByNameDesc
} from "../../../services/usersSlice";
import { useEffect } from "react";
import { UserCard } from "./components/UserCard";
import "./Users.css";
import { Grid } from "@mui/material";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import { ReactComponent as PlusIcon } from "../../../assets/icons/plus.svg";

export const Users = () => {
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);

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
  
  return (
    <div className="component-container">
      <Grid container>
        <Grid item lg={9} md={9} xs={9}>
          <h3 className="mb-2">Участники</h3>

        </Grid>
        <Grid item lg={3} md={3} xs={3}>
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
      <button
        className="btn-add-user"
      >
        <PlusIcon />
      </button>
    </div>
  );
};

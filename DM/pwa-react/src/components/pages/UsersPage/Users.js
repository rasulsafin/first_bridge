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
import { searchProjectsByTitle, sortProjectsByDateAsc, sortProjectsByDateDesc } from "../../../services/projectsSlice";

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
      <h3 className="mb-2">Участники</h3>
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
    </div>
  );
};

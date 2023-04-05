import * as React from "react";
import { SearchBar } from "../../searchBar/SearchBar";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import { fetchUsers, selectAllUsers } from "../../../services/usersSlice";
import { useEffect } from "react";
import { UserCard } from "./components/UserCard";
import "./Users.css";

export const Users = () => {
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);

  useEffect(() => {
    dispatch(fetchUsers());
  }, [dispatch]);

  return (
    <div className="component-container">
      <h3 className="mb-2">Участники</h3>
      <div className="toolbar-project">
        <SearchBar />
        <div>
          <Controls.Button
            className="ml-0"
            style={{
              backgroundColor: "#2D2926",
              color: "#FFF",
              border: "none"
            }}
          >От А до Я</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
          >От Я до А</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
          >Сначала старые</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
          >Сначала старые</Controls.Button>
        </div>
      </div>
      <div className="user-card-container">
        {users.map(user => <UserCard key={user.id} user={user} />)}
      </div>
    </div>
  );
};

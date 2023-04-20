import { useDispatch } from "react-redux";
import { searchUsersByName, sortUsersByNameAsc, sortUsersByNameDesc } from "../../../../services/usersSlice";
import { SearchBar } from "../../../searchBar/SearchBar";
import { Controls } from "../../../controls/Controls";
import React from "react";

export function SearchAndSortUserToolbar() {
  const dispatch = useDispatch();

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
    <>
      <SearchBar
        onChange={e => filterByInput(e)}
      />
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
          backgroundColor: "#b4b3b2",
          color: "#2D2926",
          border: "none"
        }}
        onClick={handleSortByDesc}
      >От Я до А</Controls.Button>
    </>
  )
}
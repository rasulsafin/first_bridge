import React from "react";
import { useDispatch } from "react-redux";
import {
  searchRolesByName,
  sortRolesByNameAsc,
  sortRolesByNameDesc
} from "../../../../services/rolesSlice";
import { SearchBar } from "../../../searchBar/SearchBar";
import { Controls } from "../../../controls/Controls";

export function SearchAndSortRoleToolbar() {
  const dispatch = useDispatch();

  function filterByInput(e) {
    dispatch(searchRolesByName(e.target.value));
  }

  const handleSortByAsc = () => {
    dispatch(sortRolesByNameAsc());
  };

  const handleSortByDesc = () => {
    dispatch(sortRolesByNameDesc());
  };

  return (
    <>
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
    </>
  );
}

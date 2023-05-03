import React from "react";
import { useDispatch } from "react-redux";
import {
  searchProjectsByTitle,
  sortProjectsByDateAsc,
  sortProjectsByDateDesc
} from "../../../../services/projectsSlice";
import { SearchBar } from "../../../searchBar/SearchBar";
import { Controls } from "../../../controls/Controls";

export function SearchAndSortProjectToolbar() {
  const dispatch = useDispatch();

  function filterByInput(e) {
    dispatch(searchProjectsByTitle(e.target.value));
  }

  const handleSortByAsc = () => {
    dispatch(sortProjectsByDateDesc());
  };

  const handleSortByDesc = () => {
    dispatch(sortProjectsByDateAsc());
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
        >Сначала новые</Controls.Button>
        <Controls.Button
          style={{
            backgroundColor: "#FFF",
            color: "#2D2926",
            border: "none"
          }}
          onClick={handleSortByDesc}
        >Сначала старые</Controls.Button>
      </div>
    </>
  );
}

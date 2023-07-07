import React, { useState } from "react";
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
  const [activeButton, setActiveButton] = useState("");

  function filterByInput(e) {
    dispatch(searchProjectsByTitle(e.target.value));
  }

  const handleSortByAsc = (event) => {
    dispatch(sortProjectsByDateDesc());
    setActiveButton(event.target.id);
  };

  const handleSortByDesc = (event) => {
    dispatch(sortProjectsByDateAsc());
    setActiveButton(event.target.id);
  };

  return (
    <>
      <SearchBar
        onChange={e => filterByInput(e)}
      />
      <div>
        <Controls.Button
          id="1"
          className="ml-0"
          style={{
            backgroundColor: activeButton === "1" ? "#2D2926" : "#FFF",
            color: activeButton === "1" ? "#FFF" : "#2D2926"
          }}
          onClick={(event) => handleSortByAsc(event)}
        >Сначала новые</Controls.Button>
        <Controls.Button
          id="2"
          style={{
            backgroundColor: activeButton === "2" ? "#2D2926" : "#FFF",
            color: activeButton === "2" ? "#FFF" : "#2D2926"
          }}
          onClick={(event) => handleSortByDesc(event)}
        >Сначала старые</Controls.Button>
      </div>
    </>
  );
}

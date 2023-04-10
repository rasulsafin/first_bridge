import React from "react";
import { SearchBar } from "../../searchBar/SearchBar";
import { Controls } from "../../controls/Controls";
import RecordsGrid from "../RecordsPage/components/RecordsGrid";

export const Templates = () => {
  return (
    <div>
      <h3 className="mb-2">Шаблоны</h3>
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
          >Сначала новые</Controls.Button>
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
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
          >Сначала старые</Controls.Button>
        </div>
      </div>
      <div>
      </div>
      <RecordsGrid />
    </div>
  );
};

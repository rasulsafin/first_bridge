import React from "react";
import { SearchBar } from "../../searchBar/SearchBar";

export const Documents = () => {
  return (
    <div>
      <h3 className="mb-2">Документы</h3>
      <div className="toolbar-project">
        <SearchBar />
      </div>
    </div>
  );
};

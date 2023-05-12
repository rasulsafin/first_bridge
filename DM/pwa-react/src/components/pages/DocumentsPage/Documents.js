import React from "react";
import { Box } from "@mui/material";
import { SearchBar } from "../../searchBar/SearchBar";

export const Documents = () => {
  return (
    <div>
      <h3 className="mb-2">Документы</h3>
      <Box>
        <SearchBar />
      </Box>
    </div>
  );
};

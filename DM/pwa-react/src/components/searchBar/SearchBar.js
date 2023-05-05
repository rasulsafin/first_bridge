import React from "react";
import { InputAdornment, TextField } from "@mui/material";
import { ReactComponent as SearchIcon } from "../../assets/icons/search.svg";
import "./SearchBar.css";

export const SearchBar = (props) => {
  const { onChange } = props;
  
  return (
    <div style={{ width: "100%" }}>
      <TextField
        className="search-input"
        placeholder="Поиск"
        type="text"
        variant="outlined"
        fullWidth
        size="small"
        onChange={onChange}
        InputProps={{
          startAdornment: (
            <InputAdornment
              position="start"
              style={{
                marginLeft: "5px"
              }}
            >
              <SearchIcon />
            </InputAdornment>
          )
          // endAdornment: value && (
          //   <Button 
          //     onClick={() => setValue("")}
          //   >
          //     <CancelIcon />
          //   </Button>
          // )
        }}
      />
    </div>
  );
};

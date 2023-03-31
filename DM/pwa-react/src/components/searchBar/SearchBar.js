import React from "react";
import { Button, InputAdornment } from "@mui/material";
import { ReactComponent as SearchIcon } from "../../assets/icons/search.svg";
import { ReactComponent as CancelIcon } from "../../assets/icons/cancel.svg";
import TextField from "@mui/material/TextField";
import "./SearchBar.css";

export const SearchBar = ({onChange}) => {
  
  return (
    <div style={{ width: "100%"}}>
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
          ),
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

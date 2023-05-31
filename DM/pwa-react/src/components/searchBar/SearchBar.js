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
        variant="standard"
        fullWidth
        autoComplete="off"
        onChange={onChange}
        sx={{
          display: "flex",
          alignItems: "center",
          height: "36px",
          borderRadius: "5px",
          alignContent: "center",
          input: {
            paddingTop: "7px"
          }
        }}
        InputProps={{
          disableUnderline: true,
          startAdornment: (
            <InputAdornment
              position="start"
              style={{
                paddingTop: "2px",
                display: "flex",
                alignItems: "center",
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

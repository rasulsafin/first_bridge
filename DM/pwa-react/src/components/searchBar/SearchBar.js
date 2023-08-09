import React, { memo } from "react";
import { useDispatch } from "react-redux";
import { Badge, IconButton, InputAdornment, TextField, Typography } from "@mui/material";
import { ReactComponent as SearchIcon } from "../../assets/icons/search.svg";
import { ReactComponent as FilterIcon } from "../../assets/icons/filter.svg";
import "./SearchBar.css";
import { toggleDrawer } from "../../services/controlUISlice";

export const SearchBar = memo(({ onChange, filter, numberOfActiveFilters, darkMode }) => {
  const dispatch = useDispatch();

  const handleClick = () => {
    dispatch(toggleDrawer());
  };

  return (
    <div className="search-bar">
      <TextField
        className="search-input"
        placeholder="Поиск"
        type="text"
        variant="standard"
        fullWidth
        autoComplete="off"
        onChange={onChange}
        sx={{
          backgroundColor: darkMode ? "#F4F4F4" : "#FFFFFF",
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
              className="search-icon"
            >
              <SearchIcon />
            </InputAdornment>
          ),
          endAdornment:
            <InputAdornment
              position="end"
              className="search-bar-end"
            >
            </InputAdornment>
        }}
      />
      {filter &&
        <IconButton
          sx={{
            backgroundColor: "#FFF",
            height: "36px",
            borderTopRightRadius: "5px",
            borderBottomRightRadius: "5px",
            borderTopLeftRadius: 0,
            borderBottomLeftRadius: 0,
            paddingRight: "18px"
          }}
          onClick={handleClick}
        >
          <FilterIcon className="filter-icon" />
          <Badge
            badgeContent={numberOfActiveFilters}
            color="error"
            sx={{
              "& .MuiBadge-badge":
                {
                  right: -9,
                  top: 10,
                  fontSize: 12,
                  padding: 0,
                  width: 15,
                  border: "3px solid #FFF"
                }
            }}
          >
            <Typography
              sx={{
                fontWeight: "bold",
                fontFamily: "Myriad Pro"
              }}
            >Фильтр
            </Typography>
          </Badge>
        </IconButton>
      }
    </div>
  );
});

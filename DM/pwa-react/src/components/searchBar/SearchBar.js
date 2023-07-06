import React from "react";
import { useDispatch } from "react-redux";
import { Badge, IconButton, InputAdornment, TextField, Typography } from "@mui/material";
import { ReactComponent as SearchIcon } from "../../assets/icons/search.svg";
import { ReactComponent as FilterIcon } from "../../assets/icons/filter.svg";
import "./SearchBar.css";
import { toggleDrawer } from "../../services/controlUISlice";

export const SearchBar = (props) => {
  const { onChange, filter, numberOfActiveFilters, darkMode } = props;
  const dispatch = useDispatch();
  
  return (
    <div style={{ width: "100%", display: "flex", flexDirection: "row" }}>
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
              style={{
                paddingTop: "2px",
                display: "flex",
                alignItems: "center",
                marginLeft: "5px"
              }}
            >
              <SearchIcon />
            </InputAdornment>
          ),
          endAdornment:
            <InputAdornment
              position="end"
              style={{
                marginRight: "20px"
              }}
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
          onClick={() => dispatch(toggleDrawer())}
        >
          <FilterIcon
            style={{
              marginRight: "5px"
            }}
          />
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
};

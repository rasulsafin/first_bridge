import React from "react";
import "../Users.css";
import { Avatar, Grid } from "@mui/material";

export const UserCard = (user) => {

  let fullName = user.user.name + " " + user.user.lastName;
  console.log(fullName);

  function stringToColor(string: string) {
    let hash = 0;
    let i;

    /* eslint-disable no-bitwise */
    for (i = 0; i < string.length; i += 1) {
      hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = "#";

    for (i = 0; i < 3; i += 1) {
      const value = (hash >> (i * 8)) & 0xff;
      color += `00${value.toString(16)}`.slice(-2);
    }
    /* eslint-enable no-bitwise */

    return color;
  }

  function stringAvatar(name: string) {
    return {
      sx: {
        bgcolor: stringToColor(name)
      },
      children: `${name.split(" ")[0][0]}${name.split(" ")[1][0]}`
    };
  }

  return (
    <div className="user-card">
      <Grid container>
        <Grid item xs={2}>
          <Avatar {...stringAvatar(fullName)} />
        </Grid>
        <Grid item xs={10}>
          <Grid rowSpacing="0" direction="column" container>
            <span style={{ fontSize: "16px" }}>{user.user.name}</span>
            <span style={{ fontSize: "12px" }}>{user.user.roles}</span>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};

import React from "react";
import "../Users.css";
import { Grid } from "@mui/material";
import { Controls } from "../../../controls/Controls";

export const UserCard = (user) => {

  let fullName = user.user.name + " " + user.user.lastName;

  return (
    <div className="user-card">
      <Grid container>
        <Grid item xs={2}>
          <Controls.Avatar fullName={fullName} />
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

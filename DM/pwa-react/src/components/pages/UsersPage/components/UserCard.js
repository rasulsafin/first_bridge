import React from "react";
import "../Users.css";
import { Grid } from "@mui/material";
import { Controls } from "../../../controls/Controls";

export const UserCard = (props) => {
  const { user } = props;
  let fullName = user.name + " " + user.lastName;
  
  return (
    <div className="user-card">
      <Grid container>
        <Grid item xs={2}>
          <Controls.Avatar fullName={fullName} />
        </Grid>
        <Grid item xs={10}>
          <Grid rowSpacing="0" direction="column" container>
            <span style={{ fontSize: "16px" }}>{fullName}</span>
            <span style={{ fontSize: "12px" }}>{user.position}</span>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};

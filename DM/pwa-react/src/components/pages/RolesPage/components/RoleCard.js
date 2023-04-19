import React from "react";
import "../Roles.css";
import { Grid } from "@mui/material";

export const RoleCard = (role) => {

  return (
    <div className="role-card">
      <Grid container>
        <Grid item xs={6}>
          {role.role.name}
        </Grid>
        <Grid item xs={6}>
         Role description
        </Grid>
      </Grid>
    </div>
  );
};

import React, { useState } from "react";
import { Grid, TextField } from "@mui/material";

export const RoleForm = (props) => {
  const { role } = props;
  const [titleRole, setTitleRole] = useState(role.name);

  return (
    <Grid direction="column" container>
      <Grid direction="row" item container>
        <Grid item xs={5}>
          <span style={{ color: "#B3B3B3" }}>
                 Название роли
               </span>
          <TextField
            value={titleRole}
            onChange={(e) => setTitleRole(e.target.value)}
            variant="outlined"
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
      </Grid>
      <Grid item container>
        <Grid item>
          {role.permissions.map(p => <>
            {p.id}
          </>)}
        </Grid>
      </Grid>
    </Grid>
  );
};

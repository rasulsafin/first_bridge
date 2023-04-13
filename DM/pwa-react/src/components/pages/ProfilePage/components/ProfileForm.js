import React from "react";
import { Grid, InputLabel } from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { profile } from "../../../../locale/ru/profile";

const ProfileForm = (props) => {
  const { user } = props;

  return (
    <>
      <form
      >
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <InputLabel>{profile.name}</InputLabel>
            <Controls.FormTextfield
              value={user?.name ?? ""}
              variant="outlined"
              type="text"
              fullWidth
              size="small"
            />
          </Grid>
          <Grid item xs={12}>
            <InputLabel>{profile.lastName}</InputLabel>
            <Controls.FormTextfield
              value={user?.lastName ?? ""}
              type="text"
              fullWidth
              size="small"
            />
          </Grid>
          <Grid item xs={12}>
            <InputLabel>{profile.fathersName}</InputLabel>
            <Controls.FormTextfield
              value={user?.fathersName ?? ""}
              type="text"
              fullWidth
              size="small"
            />
          </Grid>
          <Grid item xs={12}>
            <InputLabel>{profile.position}</InputLabel>
            <Controls.FormTextfield
              value={user?.position ?? ""}
              name="position"
              type="text"
              fullWidth
              size="small"
            />
          </Grid>
          <Grid item xs={12}>
            <InputLabel>{profile.role}</InputLabel>
            <Controls.FormTextfield
              value={user?.role ?? ""}
              name="roles"
              type="text"
              fullWidth
              size="small"
            />
          </Grid>
          <Grid item xs={12}>
            <InputLabel>{profile.email}</InputLabel>
            <Controls.FormTextfield
              value={user?.email ?? ""}
              name="email"
              type="email"
              fullWidth
              size="small"
            />
          </Grid>
          <Grid item xs={12}>
            <InputLabel>{profile.password}</InputLabel>
            <Controls.FormTextfield
              value={user?.password ?? ""}
              type="password"
              fullWidth
              size="small"
            />
          </Grid>
        </Grid>
      </form>
    </>
  );
};

export default ProfileForm;
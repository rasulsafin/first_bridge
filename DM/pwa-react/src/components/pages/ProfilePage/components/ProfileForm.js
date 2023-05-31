import React from "react";
import { Grid, InputLabel, Typography } from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { profile } from "../../../../locale/ru/profile";

const ProfileForm = (props) => {
  const { user } = props;
  
  const styleInput = {
    backgroundColor: "#FFF",
    margin: "5px",
    padding: "12px",
    borderRadius: "5px"
  }
  
  return (
    <form
    >
      <Grid container spacing={2} style={{ width: "542px"}}>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.name}</InputLabel>
          <Controls.FormTextfield
            value={user?.userName ?? ""}
            variant="outlined"
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.lastName}</InputLabel>
          <Controls.FormTextfield
            value={user?.lastName ?? ""}
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.fathersName}</InputLabel>
          <Controls.FormTextfield
            value={user?.fathersName ?? ""}
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.position}</InputLabel>
          <Controls.FormTextfield
            value={user?.position ?? ""}
            name="position"
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.role}</InputLabel>
          <Controls.FormTextfield
            value={user?.roleName ?? ""}
            name="roles"
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.email}</InputLabel>
          <Controls.FormTextfield
            value={user?.email ?? ""}
            name="email"
            type="email"
            fullWidth
            size="small"
          />
          <Typography variant="body2">Сменить E-mail</Typography>
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.password}</InputLabel>
          <Controls.FormTextfield
            value={user?.password ?? ""}
            type="password"
            fullWidth
            size="small"
          />
          <Typography variant="body2">Сменить пароль</Typography>
        </Grid>
      </Grid>
    </form>
  );
};

export default ProfileForm;
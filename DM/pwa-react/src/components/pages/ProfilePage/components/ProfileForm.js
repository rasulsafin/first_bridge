import React from "react";
import { Grid, InputLabel, Typography } from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { profile } from "../../../../locale/ru/profile";

const styleInput = {
  backgroundColor: "#FFF",
  margin: "5px",
  padding: "12px",
  borderRadius: "5px"
};

const TextfieldWithCommonProps = (props) => (
  <Controls.FormTextfield
    {...props}
    fullWidth
    size="small"
  />
);

const ProfileForm = (props) => {
  const { user } = props;

  return (
    <form>
      <Grid container spacing={2} style={{ maxWidth: "542px" }}>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.name}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.userName ?? ""}
            name="name"
            type="text"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.lastName}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.lastName ?? ""}
            name="lastName"
            type="text"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.fathersName}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.fathersName ?? ""}
            name="fathersName"
            type="text"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.position}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.position ?? ""}
            name="position"
            type="text"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.role}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.roleName ?? ""}
            name="roles"
            type="text"
          />
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.email}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.email ?? ""}
            name="email"
            type="email"
          />
          <Typography variant="body2">Сменить E-mail</Typography>
        </Grid>
        <Grid item xs={12} sx={styleInput}>
          <InputLabel>{profile.password}</InputLabel>
          <TextfieldWithCommonProps
            value={user?.password ?? ""}
            type="password"
          />
          <Typography variant="body2">Сменить пароль</Typography>
        </Grid>
      </Grid>
    </form>
  );
};

export default ProfileForm;

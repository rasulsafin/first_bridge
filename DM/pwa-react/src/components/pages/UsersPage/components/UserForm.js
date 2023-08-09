import React, { useEffect, useMemo, useState } from "react";
import { Field, useFormikContext } from "formik";
import { Box, Grid, InputLabel, List, MenuItem, Typography } from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { user } from "../../../../locale/ru/user";
import { UserCreateChildModal } from "./UserCreateChildModal";

export const UserForm = ({ roles, projects }) => {
  const { setFieldValue } = useFormikContext();
  const [addedProjects, setAddedProjects] = useState([]);
  console.log(addedProjects);

  const filterProjects = useMemo(() => {
    return projects.filter(project => addedProjects.some(id => id === project.id));
  }, [projects, addedProjects]);

  useEffect(() => {
    setFieldValue("projectsIds", addedProjects);
  }, [addedProjects]);

  const ProjectItem = React.memo(({ project }) => (
    <Box
      key={project.id}
      sx={{
        height: "73px",
        backgroundColor: "#F4F4F4",
        margin: "4px",
        padding: "16px",
        borderRadius: "5px"
      }}
    >
      <Grid direction="column" container>
        <Grid item xs={2}>
          <span style={{ fontSize: "12px" }}>Участники: {project.users.length}</span>
        </Grid>
        <Grid item xs={10}>
          <span style={{ fontWeight: "bold" }}>{project.title}</span>
        </Grid>
      </Grid>
    </Box>
  ));

  return (
    <>
      <Grid container spacing={2} sx={{ width: "50%" }}>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.lastName}</InputLabel>
          <Field name="lastName" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.name}</InputLabel>
          <Field name="name" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.fathersName}</InputLabel>
          <Field name="fathersName" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.login}</InputLabel>
          <Field name="login" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.email}</InputLabel>
          <Field name="email" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.role}</InputLabel>
          <Field name="roleId" as={Controls.Select}>
            {roles.map(role => (
              <MenuItem
                key={role.id}
                value={role.id}
              >{role.name}
              </MenuItem>)
            )}
          </Field>
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.position}</InputLabel>
          <Field name="position" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{user.password}</InputLabel>
          <Field name="hashedPassword" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <Field name="organizationId" as={Controls.ValidationFormTextfield} hidden />
        </Grid>
      </Grid>
      <Box sx={{ width: "480px" }}>
        <Typography variant="h5">Доступ к проектам</Typography>
        <List style={{ height: "200px", overflowY: "auto", overflowX: "hidden" }}>
          {filterProjects ?
            filterProjects.map(project =>
              <ProjectItem key={project.id} project={project} />
            )
            : null
          }
        </List>
        <UserCreateChildModal projects={projects} setAddedProjects={setAddedProjects} />
      </Box>
    </>
  );
};

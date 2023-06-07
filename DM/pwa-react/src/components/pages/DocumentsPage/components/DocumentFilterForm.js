import React from "react";
import { Grid, InputLabel, MenuItem } from "@mui/material";
import { Field } from "formik";
import { Controls } from "../../../controls/Controls";
import { document } from "../../../../locale/ru/document";

const DocumentFilterForm = (props) => {
  const { projects, users } = props;

  return (
    <div>
      <Grid container spacing={2}>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{document.project}</InputLabel>
          <Field name="project" as={Controls.Select}>
            {projects.map(project => (
              <MenuItem
                key={project.id}
                value={project.id}
              >{project.title}
              </MenuItem>)
            )}
          </Field>
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{document.model}</InputLabel>
          <Field name="model" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{document.status}</InputLabel>
          <Field name="status" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{document.executor}</InputLabel>
          <Field name="executor" as={Controls.Select}>
            {users.map(user => (
              <MenuItem
                key={user.id}
                value={user.id}
              >{user.name}
              </MenuItem>)
            )}
          </Field>
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{document.createdBy}</InputLabel>
          <Field name="createdBy" as={Controls.Select}>
            {users.map(user => (
              <MenuItem
                key={user.id}
                value={user.id}
              >{user.name}
              </MenuItem>)
            )}
          </Field>
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{document.createdAt}</InputLabel>
          <Field name="createdAt" as={Controls.DatePicker} />
        </Grid>
      </Grid>
    </div>
  );
};
export default DocumentFilterForm;
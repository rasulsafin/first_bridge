import React from "react";
import { Field, Form, Formik } from "formik";
import { Grid, InputLabel, MenuItem } from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { userValidationSchema } from "../utils/validationSchema";
import { user } from "../../../../locale/ru/user";

const UserForm = (props) => {
  const { initialValues, onSubmit, roles } = props;

  return (
    <Formik
      initialValues={initialValues}
      validationSchema={userValidationSchema}
      onSubmit={onSubmit}
    >
      {({ isValid, dirty }) => (
        <Form>
          <Grid container spacing={2}>
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
          <Controls.Button
            type="submit"
            disabled={!isValid || !dirty}
          >Сохранить
          </Controls.Button>
        </Form>
      )}
    </Formik>
  );
};

export default UserForm;
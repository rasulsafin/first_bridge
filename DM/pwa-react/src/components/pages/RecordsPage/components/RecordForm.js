import React from "react";
import { Grid, InputLabel } from "@mui/material";
import { record } from "../../../../locale/ru/record";
import { Field } from "formik";
import { Controls } from "../../../controls/Controls";

export const RecordForm = (props) => {
  
  return (
    <>
      <Grid container spacing={2} sx={{ width: "50%" }}>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.title}</InputLabel>
          <Field name="title" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.id}</InputLabel>
          <Field name="id" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.status}</InputLabel>
          <Field name="status" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.priority}</InputLabel>
          <Field name="priority" as={Controls.ValidationFormTextfield} />
        </Grid>

        {/*<Grid item xs={12} md={12} lg={12}>*/}
        {/*  <InputLabel>{record.role}</InputLabel>*/}
        {/*  <Field name="roleId" as={Controls.Select}>*/}
        {/*    {roles.map(role => (*/}
        {/*      <MenuItem*/}
        {/*        key={role.id}*/}
        {/*        value={role.id}*/}
        {/*      >{role.name}*/}
        {/*      </MenuItem>)*/}
        {/*    )}*/}
        {/*  </Field>*/}
        {/*</Grid>*/}

        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.executor}</InputLabel>
          <Field name="executor" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.createdAt}</InputLabel>
          <Field name="createdAt" as={Controls.ValidationFormTextfield} />
        </Grid>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{record.createdBy}</InputLabel>
          <Field name="createdBy" as={Controls.ValidationFormTextfield} />
        </Grid>
      </Grid>
    </>
  );
};

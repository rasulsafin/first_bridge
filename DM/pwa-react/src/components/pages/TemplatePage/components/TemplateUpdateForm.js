import React from "react";
import { Box, Grid, InputLabel, List, MenuItem, Typography } from "@mui/material";
import { Field, FieldArray, useFormikContext } from "formik";
import { Controls } from "../../../controls/Controls";
import { TemplateUpdateChildModal } from "./TemplateUpdateChildModal";

export const TemplateUpdateForm = () => {
  const { values } = useFormikContext();

  return (
    <div>
      <Grid container spacing={2}>
        <Grid item xs={7} md={7} lg={7}>
          <InputLabel>Название шаблона</InputLabel>
          <Field name="name" as={Controls.ValidationFormTextfield} />
          <Box sx={{
            marginTop: "40px"
          }}>
            <Typography variant="h5">Поля</Typography>
            <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>

              {/*{values.fields.map(field => <>*/}
              {/*    <InputLabel>{field.name}</InputLabel>*/}
              {/*  <Field name="fields.data" as={Controls.ValidationFormTextfield} />*/}
              {/*  </>*/}
              {/*  )}*/}

              <FieldArray
                name="fields"
                render={() => (
                  <div>
                    {values.fields.map((field, index) => (
                      <div key={index}>
                        <InputLabel>{field.name}</InputLabel>
                        <Field name={`fields[${index}].data`} as={Controls.ValidationFormTextfield}/>
                      </div>
                    ))}
                  </div>)
                }
              />
                            
              {values.listFields.map(listField => (
                <>
                <InputLabel>{listField.name}</InputLabel>
              <Field name="listFields" as={Controls.Select}>
                {listField.lists.map(list => (
                  <MenuItem
                    key={list.id}
                    value={list.data}
                  >{list.data}
                  </MenuItem>)
                )}
              </Field>
                </>))}
              
            </List>
            {/*<ProjectCreateChildModal*/}
            {/*  users={users}*/}
            {/*  setAddedUsers={setAddedUsers}*/}
            {/*/>*/}
            <TemplateUpdateChildModal />
            <pre>{JSON.stringify(values, null, 2)}</pre>
          </Box>
        </Grid>
      </Grid>
    </div>
  );
};

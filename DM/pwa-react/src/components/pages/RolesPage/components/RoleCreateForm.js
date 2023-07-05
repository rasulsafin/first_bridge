import React from "react";
import { Grid, InputLabel } from "@mui/material";
import { Field, FieldArray, useFormikContext } from "formik";
import { Controls } from "../../../controls/Controls";
import { role } from "../../../../locale/ru/role";
import { CheckBoxRow } from "./RoleForm";

const permissionTitles = [
  "Project",
  "Item",
  "Record",
  "Template",
  "Role",
  "User",
  "Organization",
  "Document"
]

const permissions = [
  {
    type: 1,
    name: "Project",
    create: false,
    read: false,
    update: false,
    delete: false
  },
  {
    type: 2,
    name: "Item",
    create: false,
    read: false,
    update: false,
    delete: false
  },
  {
    type: 3,
    name: "Record",
    create: false,
    read: false,
    update: false,
    delete: false
  },
  {
    type: 4,
    name: "Template",
    create: false,
    read: false,
    update: false,
    delete: false
  }
]


export const RoleCreateForm = () => {
  const { values } = useFormikContext();
  
  return (
    <>
      <Grid container spacing={2}>
        <Grid item xs={12} md={12} lg={12}>
          <InputLabel>{role.name}</InputLabel>
          <Field name="name" as={Controls.ValidationFormTextfield} />
        </Grid>
      </Grid>
      <Grid item container>
        <Grid item container direction="row">
          <Grid item lg={2}>
            <h6>Модули</h6>
          </Grid>
          <Grid item lg={1}>
            <span>Создание</span>
          </Grid>
          <Grid item lg={1}>
            <span>Просмотр</span>
          </Grid>
          <Grid item lg={1}>
            <span>Изменение</span>
          </Grid>
          <Grid item lg={1}>
            <span>Удаление</span>
          </Grid>
        </Grid>
        <Grid item container>

          {/*{permissionTitles.map(permission => */}
          {/*  <Grid item container direction="row"> */}
          {/*    <Grid item lg={2}>*/}
          {/*      <h6>{permission}</h6>*/}
          
          {/*    </Grid>*/}
          {/*    <Grid item lg={10}>*/}
          {/*      */}
          {/*    </Grid>*/}
          {/*  </Grid>)}*/}

          <FieldArray
            name="permissions"
            render={(arrayHelpers) => (
              <div>
                {permissionTitles.map((permission) => (
                  <div key={permission.id}>
                    <label>
                      <input
                        name="permissions"
                        type="checkbox"
                        value={permission}
                        checked={values.permissions
                          .map((e) => e)
                          .includes(permission)}
                        onChange={(e) => {
                          if (e.target.checked)
                            arrayHelpers.push({ id: permission, text: "txt" });
                          else {
                            const index = values.permissions
                              .map(function (e) {
                                return e.id;
                              })
                              .indexOf(permission);
                            arrayHelpers.remove(index);
                          }
                        }}
                      />
                      {permission}
                    </label>
                    
                  </div>
                ))}
                <pre>{JSON.stringify(values, null, 2)}</pre>
              </div>
            )}
          />
        </Grid>
      </Grid>
    </>
  );
};

import React, { useEffect, useState } from "react";
import { Box, Grid, IconButton, InputLabel, List, MenuItem, Typography } from "@mui/material";
import { Field, FieldArray, useFormikContext } from "formik";
import { useDispatch, useSelector } from "react-redux";
import { Controls } from "../../../controls/Controls";
import { TemplateUpdateChildModal } from "./TemplateUpdateChildModal";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { addNewField, addNewListField, deleteField, deleteListField } from "../../../../services/fieldsSlice";
import { selectCurrentProject } from "../../../../services/projectsSlice";

export const TemplateUpdateForm = () => {
  const [addingFields, setAddingFields] = useState();
  const dispatch = useDispatch();
  const { values } = useFormikContext();
  const currentProject = useSelector(selectCurrentProject);

  useEffect(() => {
  }, [dispatch]);

  useEffect(() => {
    if (addingFields !== undefined && addingFields?.fields.length !== 0) {
      const fillFields = addingFields?.fields.filter(item => item.name !== null && item.name !== undefined);
      if (fillFields.length !== 0) {
        fillFields.forEach(fillField => fillField.templateId = values.id);
        const newField = { ...fillFields[0] };
        dispatch(addNewField({ newField, projectId: currentProject }));
      }
    }
    if (addingFields !== undefined && addingFields?.listFields.length !== 0) {
      const fillListFields = addingFields?.listFields.filter(item => item.name !== null && item.name !== undefined);
      if (fillListFields.length !== 0) {
        fillListFields.forEach(fillField => fillField.templateId = values.id);
        const newListField = { ...fillListFields[0] };
        dispatch(addNewListField({ newListField, projectId: currentProject }));
      }
    }
  }, [addingFields]);

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
              <FieldArray
                name="fields"
                render={() => (
                  <div>
                    {values.fields.map((field, index) => (
                      <div key={field.id}>
                        <InputLabel>{field.name}</InputLabel>
                        <div
                          style={{
                            display: "flex"
                          }}
                        >
                          <Field
                            placeholder={field.name}
                            name={`fields[${index}].data`}
                            as={Controls.ValidationFormTextfield} />
                          <IconButton
                            onClick={() => dispatch(deleteField({ fieldId: field.id, projectId: currentProject }))}
                          >
                            <CancelIcon />
                          </IconButton>
                        </div>
                      </div>
                    ))}
                  </div>)
                }
              />
              {values.listFields.map(listField => (
                <>
                  <InputLabel>{listField.name}</InputLabel>
                  <div
                    style={{
                      display: "flex"
                    }}
                  >
                    <Field name="listFields" as={Controls.Select}>
                      {listField.lists.map(list => (
                        <MenuItem
                          key={list.id}
                          value={list.data}
                          disabled
                        >{list.data}
                        </MenuItem>)
                      )}
                    </Field>
                    <IconButton
                      onClick={() => dispatch(deleteListField({
                        listFieldId: listField.id,
                        projectId: currentProject
                      }))}
                    >
                      <CancelIcon />
                    </IconButton>
                  </div>
                </>
              ))}
            </List>
            <TemplateUpdateChildModal setAddingFields={setAddingFields} />
          </Box>
        </Grid>
      </Grid>
    </div>
  );
};

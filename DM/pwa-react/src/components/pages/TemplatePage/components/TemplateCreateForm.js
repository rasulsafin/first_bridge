import React, { useEffect, useRef, useState } from "react";
import {
  Box,
  FormControl,
  Grid,
  IconButton,
  InputLabel,
  List,
  MenuItem,
  Typography
} from "@mui/material";
import { Field, FieldArray, useFormikContext } from "formik";
import { useSelector } from "react-redux";
import { Controls } from "../../../controls/Controls";
import { TemplateUpdateChildModal } from "./TemplateUpdateChildModal";
import { selectCurrentProject } from "../../../../services/projectsSlice";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";

export const TemplateCreateForm = () => {
  const [addingFields, setAddingFields] = useState();
  const { values, setFieldValue } = useFormikContext();
  const FieldHelperRef = useRef();
  const ListFieldHelperRef = useRef();
  const currentProject = useSelector(selectCurrentProject);

  useEffect(() => {
    setFieldValue("projectId", currentProject);
  }, [currentProject]);

  useEffect(() => {
    if (addingFields !== undefined && addingFields?.fields.length !== 0) {
      const fillFields = addingFields?.fields.filter(item => item.name !== null && item.name !== undefined);
      if (fillFields.length !== 0) {
        FieldHelperRef.current.push(...fillFields);
      }
    }
    if (addingFields !== undefined && addingFields?.listFields.length !== 0) {
      const fillListFields = addingFields?.listFields.filter(item => item.name !== null && item.name !== undefined);
      if (fillListFields.length !== 0) {
        ListFieldHelperRef.current.push(...fillListFields);
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
                render={(arrayHelpers) => {
                  FieldHelperRef.current = arrayHelpers;
                  return (
                    <div>
                      {values?.fields?.map((field, index) => (
                        <div key={index}>
                          <InputLabel>{field.name}</InputLabel>
                          <div style={{ display: "flex" }}>
                            <Field
                              placeholder={field.name}
                              name={`fields[${index}].data`}
                              as={Controls.ValidationFormTextfield}
                            />
                            <IconButton onClick={() => arrayHelpers.remove(index)}>
                              <CancelIcon />
                            </IconButton>
                          </div>
                        </div>
                      ))}
                    </div>);
                }}
              />
              <FieldArray
                name="listFields"
                render={(arrayHelpers) => {
                  ListFieldHelperRef.current = arrayHelpers;
                  return (
                    <div>
                      {values?.listFields?.map((field, index) => (
                        <div key={index}>
                          <InputLabel>{field?.name}</InputLabel>
                          <div style={{ display: "flex" }}>
                            <FormControl
                              size="small"
                              variant="outlined"
                              fullWidth={true}
                            >
                              <InputLabel>Выберите вариант</InputLabel>
                              <Field autoWidth={false} name={`listFields[${index}].name`} as={Controls.Select}>
                                {field?.lists.map((list, listIndex) => (
                                  <MenuItem
                                    key={listIndex}
                                    value={list}
                                    disabled
                                  >{list?.data}
                                  </MenuItem>)
                                )}
                              </Field>
                            </FormControl>
                            <IconButton onClick={() => arrayHelpers.remove(index)}>
                              <CancelIcon />
                            </IconButton>
                          </div>
                        </div>
                      ))}
                    </div>
                  );
                }}
              />
            </List>
            <TemplateUpdateChildModal setAddingFields={setAddingFields} />
          </Box>
        </Grid>
      </Grid>
    </div>
  );
};

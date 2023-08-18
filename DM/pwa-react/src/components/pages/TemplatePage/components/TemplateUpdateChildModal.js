import React, { useCallback, useRef, useState } from "react";
import { Box, IconButton, InputLabel, List, MenuItem, Modal } from "@mui/material";
import { FieldArray, Form, Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { inputTypes } from "../../../../constants/inputTypes";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { ReactComponent as AddIcon } from "../../../../assets/icons/add.svg";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "none",
  borderRadius: 3,
  boxShadow: 24,
  pt: 2,
  px: 4,
  pb: 3
};

const listStyle = {
  marginTop: "20px",
  height: "300px",
  gap: "2px",
  overflowY: "auto",
  overflowX: "hidden"
  // backgroundColor: "#B3B3B3"
};

const templateData = {
  name: "",
  createdById: null,
  updatedById: null,
  fields: [
    {
      name: null,
      isMandatory: false,
      data: "",
      type: 1,
      createdById: null,
      updatedById: null
    }
  ],
  listFields: [
    {
      name: null,
      isMandatory: false,
      type: 2,
      createdById: null,
      updatedById: null,
      lists: [
        {
          data: "",
          createdById: null,
          updatedById: null
        },
        {
          data: "",
          createdById: null,
          updatedById: null
        }
      ]
    }
  ]
};

export const TemplateUpdateChildModal = ({ setAddingFields }) => {
  const [open, setOpen] = useState(false);
  const [textfieldType, setTextfieldType] = useState("text");
  const formRef = useRef();
  const isEnum = textfieldType === "enum";

  const handleOpen = useCallback(() => {
    setOpen(true);
  }, []);

  const handleClose = useCallback(() => {
    setOpen(false);
  }, []);

  const handleSubmit = () => {
    formRef.current?.handleSubmit();
    setOpen(false);
  };

  return (
    <div>
      <Controls.Button
        onClick={handleOpen}
        className="m-0"
        sx={{ width: "100%" }}
        variant="outlined"
      >Добавить</Controls.Button>
      <Modal
        hideBackdrop
        open={open}
        onClose={handleClose}
        aria-labelledby="child-modal-title"
        aria-describedby="child-modal-description"
      >
        <Box sx={{ ...style, width: 500 }}>
          <h2 id="child-modal-title">Добавление поля</h2>
          <Box sx={{
            marginTop: "40px",
            display: "flex",
            flexDirection: "column"
          }}>

            <InputLabel>Тип поля</InputLabel>
            <Controls.Select
              name="type"
              label="type"
              value={textfieldType}
              onChange={(e) => setTextfieldType(e.target.value)}
            >
              {inputTypes.map(inputType =>
                <MenuItem key={inputType.id} value={inputType.value}>{inputType.name}</MenuItem>
              )}
            </Controls.Select>
            <List style={listStyle}>
              <Formik
                innerRef={formRef}
                initialValues={{
                  fields: templateData.fields,
                  listFields: templateData.listFields
                }}
                onSubmit={(values) => {
                  setAddingFields(values);
                }}
              >
                {({ values, setFieldValue }) => (
                  <Form>
                    {!isEnum ?
                      <FieldArray name="fields">
                        {() => (
                          <div>
                            {values?.fields.map((field, index) => (
                              <div key={index}>
                                <InputLabel>Название поля</InputLabel>
                                <Controls.ValidationFormTextfield
                                  placeholder="Field"
                                  name={`fields.${index}.name`}
                                />
                                <Controls.Checkbox
                                  label="Обязательное поле"
                                  onChange={(e) => {
                                    setFieldValue(`fields.${index}.isMandatory`, e.target.checked);
                                  }}
                                />
                              </div>
                            ))}
                          </div>
                        )}
                      </FieldArray>
                      :
                      <FieldArray name="listFields">
                        {() => (
                          <div>
                            {values?.listFields?.map((listField, index) => (
                              <div key={index}>
                                <InputLabel>Название поля</InputLabel>
                                <Controls.ValidationFormTextfield
                                  placeholder="Field"
                                  name={`listFields.${index}.name`}
                                />
                                <Controls.Checkbox
                                  label="Обязательное поле"
                                  onChange={(e) => {
                                    setFieldValue(`listFields.${index}.isMandatory`, e.target.checked);
                                  }}
                                />
                                <FieldArray name={`listFields.${index}.lists`}>
                                  {({ push: pushList, remove }) => (
                                    <div>
                                      <InputLabel>Содержание списка</InputLabel>
                                      {listField?.lists.map((list, listIndex) => (
                                        <div
                                          key={listIndex}
                                          style={{
                                            display: "flex",
                                            marginBottom: "5px"
                                          }}
                                        >
                                          <Controls.ValidationFormTextfield
                                            name={`listFields.${index}.lists.${listIndex}.data`}
                                            placeholder="List Data"
                                          />
                                          <IconButton onClick={() => remove(listIndex)}>
                                            <CancelIcon />
                                          </IconButton>
                                        </div>
                                      ))}
                                      <IconButton onClick={() =>
                                        pushList(
                                          {
                                            data: "",
                                            createdById: null,
                                            updatedById: null
                                          })}>
                                        <AddIcon />
                                      </IconButton>
                                    </div>
                                  )}
                                </FieldArray>
                              </div>
                            ))}
                          </div>
                        )}
                      </FieldArray>
                    }
                  </Form>
                )}
              </Formik>
            </List>
          </Box>
          <Controls.Button
            variant="outlined"
            onClick={handleSubmit}
          >Добавить</Controls.Button>
          <Controls.Button
            variant="outlined"
            onClick={handleClose}
          >Отменить</Controls.Button>
        </Box>
      </Modal>
    </div>
  );
};

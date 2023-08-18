import React, { useState } from "react";
import { Formik } from "formik";
import { useDispatch } from "react-redux";
import { Controls } from "../../../controls/Controls";
import { TemplateUpdateForm } from "./TemplateUpdateForm";
import { getInitialValuesFromData } from "../utils/getInitialValuesFromData";

export const TemplateUpdateModal = ({ toggleModal, template }) => {
  const dispatch = useDispatch();
  const initialValues = getInitialValuesFromData(template);
  const [isOpen, setIsOpen] = useState(true);
  const title = "Редактирование шаблона";

  const toggleIt = () => {
    setIsOpen(false);
    toggleModal();
  };
  return (
    <Controls.Modal
      open={isOpen}
      onClose={toggleIt}
    >
      <Formik
        initialValues={initialValues}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          // dispatch(addNewRole(values));
          formikHelpers.resetForm();
          toggleIt();
        }}
      >
        {({ isValid, dirty }) => (
          <Controls.ModalForm>
            <Controls.ModalContent
              title={title}
              isWithActions="true"
              confirmButtonProps={{
                children: "Сохранить",
                disabled: !(isValid && dirty)
              }}
              cancelButtonProps={{ onClick: toggleIt }}
            >
              <TemplateUpdateForm />
            </Controls.ModalContent>
          </Controls.ModalForm>
        )}
      </Formik>
    </Controls.Modal>
  );
};

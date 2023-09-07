import React, { useState } from "react";
import { Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { TemplateUpdateForm } from "./TemplateUpdateForm";
import { getInitialValuesFromData } from "../utils/getInitialValuesFromData";

export const TemplateUpdateModal = ({ toggleModal, template }) => {
  const initialValues = getInitialValuesFromData(template);
  const [isOpen, setIsOpen] = useState(true);
  const title = "Просмотр шаблона";

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
        enableReinitialize="true"
        onSubmit={(values, formikHelpers) => {
          console.log(values);
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
                children: "Сохранить"
                // disabled: !(isValid && dirty)
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

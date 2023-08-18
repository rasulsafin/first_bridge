import React, { useState } from "react";
import { Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { useDispatch } from "react-redux";
import { getInitialValues } from "../utils/getInitialValues";
import { TemplateCreateForm } from "./TemplateCreateForm";
import { addNewTemplate } from "../../../../services/recordTemplatesSlice";

export const TemplateCreateModal = (props) => {
  const { toggleModal } = props;
  const dispatch = useDispatch();
  const initialValues = getInitialValues();
  const [isOpen, setIsOpen] = useState(true);
  const title = "Создание шаблона";

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
          dispatch(addNewTemplate(values));
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
                children: "Создать",
                disabled: !(isValid && dirty)
              }}
              cancelButtonProps={{ onClick: toggleIt }}
            >
              <TemplateCreateForm />
            </Controls.ModalContent>
          </Controls.ModalForm>
        )}
      </Formik>
    </Controls.Modal>
  );
};

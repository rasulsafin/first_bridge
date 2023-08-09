import { useDispatch } from "react-redux";
import React, { useState } from "react";
import { Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { getInitialValues } from "../utils/getInitialValues";
import { ProjectForm } from "./ProjectForm";
import { addNewProject } from "../../../../services/projectsSlice";
import { projectValidationSchema } from "../utils/validationSchema";

export const ProjectCreateModal = ({ toggleModal, users }) => {
  const dispatch = useDispatch();
  const initialValues = getInitialValues();
  const [isOpen, setIsOpen] = useState(true);
  const title = "Создание проекта";

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
        validationSchema={projectValidationSchema}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(addNewProject(values));
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
              <ProjectForm users={users} />
            </Controls.ModalContent>
          </Controls.ModalForm>
        )}
      </Formik>
    </Controls.Modal>
  );
};
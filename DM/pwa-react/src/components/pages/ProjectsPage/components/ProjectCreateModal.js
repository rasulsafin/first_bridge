import { Controls } from "../../../controls/Controls";
import { useDispatch } from "react-redux";
import { getInitialValues } from "../utils/getInitialValues";
import React, { useState } from "react";
import { userValidationSchema } from "../../UsersPage/utils/validationSchema";
import { addNewUserWithProjects } from "../../../../services/usersSlice";
import UserForm from "../../UsersPage/components/UserForm";
import { Formik } from "formik";
import { ProjectForm } from "./ProjectForm";

export const ProjectCreateModal = (props) => {
  const { toggleModal } = props;
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
        validationSchema={userValidationSchema}
        onSubmit={(values, formikHelpers) => {
          console.log(values);
          dispatch(addNewUserWithProjects(values));
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
              <ProjectForm />
            </Controls.ModalContent>
          </Controls.ModalForm>
        )}
      </Formik>
    </Controls.Modal>
  );
};
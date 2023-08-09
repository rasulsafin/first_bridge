import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { UserForm } from "./UserForm";
import { addNewUserWithProjects } from "../../../../services/usersSlice";
import { userValidationSchema } from "../utils/validationSchema";
import { getInitialValues } from "../utils/getInitialValues";

export const UserCreateModal = (props) => {
  const { toggleModal, roles, projects, title } = props;
  const dispatch = useDispatch();
  const initialValues = getInitialValues();
  const [isOpen, setIsOpen] = useState(true);

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
              <UserForm
                roles={roles}
                projects={projects}
              />
            </Controls.ModalContent>
          </Controls.ModalForm>
        )}
      </Formik>
    </Controls.Modal>
  );
};

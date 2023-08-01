import React, { useState } from "react";
import { Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { useDispatch } from "react-redux";
import { addNewRole } from "../../../../services/rolesSlice";
import { getInitialValues } from "../utils/getInitialValues";
import { RoleForm } from "./RoleForm";
import { RoleCreateForm } from "./RoleCreateForm";

export const RoleCreateModal = (props) => {
  const { toggleModal, roles } = props;
  const dispatch = useDispatch();
  const initialValues = getInitialValues();
  const [isOpen, setIsOpen] = useState(true);
  const title = "Создание роли";

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
          dispatch(addNewRole(values));
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
              <RoleCreateForm />
              
              {/*<RoleForm />*/}
              
            </Controls.ModalContent>
          </Controls.ModalForm>
        )}
      </Formik>
    </Controls.Modal>
  );
};

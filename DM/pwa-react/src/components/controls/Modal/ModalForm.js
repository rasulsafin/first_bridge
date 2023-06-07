import React from "react";
import { Form } from "formik";

export const ModalForm = (props) => (
  <Form
    style={{ width: "100%", height: "100%", display: "flex", flexDirection: "column" }}
    {...props}
  />
);

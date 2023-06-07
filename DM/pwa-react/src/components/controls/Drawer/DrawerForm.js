import React from "react";
import { Form } from "formik";

export const DrawerForm = (props) => (
  <Form
    style={{ height: "100%", width: "415px", display: "flex", flexDirection: "column" }}
    {...props}
  />
);
import { object, string } from "yup";
import { RegExpress } from "./regularExpression";

export const userValidationSchema = object({
  name: string()
    .required("Please enter title")
    .min(3, "title too short"),
  lastName: string()
    .required("Please enter description")
    .min(3, "description too short"),
  fathersName: string()
    .required("Please enter description")
    .min(3, "description too short"),
  login: string()
    .required("Please enter login")
    .min(3, "description too short"),
  email: string()
    .required("Please enter email")
    .email("a@a.aa"),
  password: string()
    .required("Please enter password")
    .min(3, "description too short"),
  roles: string()
    .required("Please enter role"),
  snils: string()
    .required("Please enter snils"),
    // .matches(RegExpress.snils, "format 000-000-000-00"),
  position: string()
    .required("Please enter position")
});
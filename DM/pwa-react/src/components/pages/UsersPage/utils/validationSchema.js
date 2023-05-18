import { object, string } from "yup";

export const userValidationSchema = object({
  name: string()
    .required("Please enter name")
    .min(2, "title too short")
    .max(25, "name must be shorter"),
  lastName: string()
    .required("Please enter lastName")
    .min(2, "lastName too short")
    .max(25, "lastName must be shorter"),
  fathersName: string()
    .required("Please enter fathersName")
    .min(2, "fathersName too short")
    .max(25, "fathersName must be shorter"),
  login: string()
    .required("Please enter login")
    .min(5, "login too short")
    .max(25, "login must be shorter"),
  email: string()
    .required("Please enter email")
    .email("a@a.aa"),
  hashedPassword: string()
    .required("Please enter password")
    .min(3, "password too short")
    .max(25, "password must be shorter"),
  position: string()
    .required("Please enter position")
    .min(3, "position too short")
    .max(25, "position must be shorter")
});
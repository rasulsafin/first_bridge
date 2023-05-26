import { object, string } from "yup";

export const projectValidationSchema = object({
  title: string()
    .required("Please enter title")
    .min(3, "title too short")
});
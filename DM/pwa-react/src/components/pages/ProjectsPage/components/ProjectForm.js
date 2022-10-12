import { Form, Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { projectValidationSchema } from "../utils/validationSchema";
import { Button } from "@mui/material";

const initialValues = {
  title: "",
  description: ""
};

const CreateProjectForm = (props) => {
  const { onSubmit } = props;

  return (
    <div>
      <Formik
        initialValues={initialValues}
        validationSchema={projectValidationSchema}
        onSubmit={onSubmit}
      >
        {({ isValid, dirty }) => (
          <Form>
            <Controls.ValidationFormTextfield
              name="title"
              type="title"
              label="Title"
              required
            />
            <Controls.ValidationFormTextfield
              name="description"
              type="description"
              label="Description"
              required
            />
            <Button
              sx={{
                width: { sm: 100, md: 150 },
                "& .MuiInputBase-root": {
                  height: 45,
                  marginRight: 3
                }
              }}
              type="submit"
              variant="contained"
              size="small"
              margin="normal"
              disabled={!isValid || !dirty}
            >
              Submit
            </Button>
          </Form>
        )}
      </Formik>
    </div>
  );
};

export default CreateProjectForm;
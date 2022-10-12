import { Form, Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { userValidationSchema } from "../utils/validationSchema";
import { Button } from "@mui/material";

const CreateUserForm = (props) => {
  const { onSubmit, initialValues } = props;

  return (
    <>
      <Formik
        initialValues={initialValues}
        validationSchema={userValidationSchema}
        onSubmit={onSubmit}
      >
        {({ isValid, dirty }) => (
          <Form>
            <div
              className="col-10 flex"
              style={{
                display: "flex",
                justifyContent: "flex-start",
                alignItems: "center",
                flexWrap: "wrap"
              }}
            >
              <Controls.ValidationFormTextfield
                className="col-4"
                name="name"
                type="name"
                label="Name"
                required
              />
              <Controls.ValidationFormTextfield
                name="lastName"
                type="lastName"
                label="LastName"
                required
              />
              <Controls.ValidationFormTextfield
                name="fathersName"
                type="fathersName"
                label="FathersName"
                required
              />
              <Controls.ValidationFormTextfield
                InputLabelProps={{ shrink: true }}
                name="birthdate"
                type="date"
                label="Birthdate"
                required
              />
              <Controls.ValidationFormTextfield
                name="login"
                type="login"
                label="Login"
                required
              />
              <Controls.ValidationFormTextfield
                name="email"
                type="email"
                label="Email"
                required
              />
              <Controls.ValidationFormTextfield
                name="password"
                type="password"
                label="Password"
                required
              />
              <Controls.ValidationFormTextfield
                name="roles"
                type="roles"
                label="Role"
                required
              />
              <Controls.ValidationFormTextfield
                name="snils"
                type="snils"
                label="Snils"
                required
              />
              <Controls.ValidationFormTextfield
                name="position"
                type="position"
                label="Position"
                required
              />
            </div>
            <Button
              sx={{
                width: { sm: 100, md: 150 },
                marginTop: 3
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
    </>
  );
};

export default CreateUserForm;
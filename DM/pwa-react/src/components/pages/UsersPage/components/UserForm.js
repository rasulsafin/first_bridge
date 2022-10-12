import { Form, Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { projectValidationSchema } from "../utils/validationSchema";
import { Button } from "@mui/material";

const initialValues = {
  name: "",
  lastName: "",
  fathersName: "",
  login: "",
  email: "",
  password: "",
  roles: "",
  birthdate: "",
  snils: "",
  position: "",
  organizationId: ""
};

const CreateUserForm = (props) => {
  const { onSubmit } = props;

  return (
    <div className="col-12"
         style={{
           display: "flex",
           flexDirection: "row",
           // justifyContent: "flex-start",
           // alignItems: "center",
           // flexWrap: "wrap"
         }}>
      <Formik
        initialValues={initialValues}
        validationSchema={projectValidationSchema}
        onSubmit={onSubmit}
      >
        {({isValid, dirty }) => (
          <div>
          <Form>
            <Controls.ValidationFormTextfield
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
            <Button
              sx={{
                width: { sm: 100, md: 150 },
                "& .MuiInputBase-root": {
                  height: 45,
                  marginRight: 3,
                },
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
          </div>
        )}
      </Formik>
    </div>
  );
};

export default CreateUserForm;
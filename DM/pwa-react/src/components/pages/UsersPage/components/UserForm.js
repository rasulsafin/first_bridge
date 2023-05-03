import { Form, Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { userValidationSchema } from "../utils/validationSchema";
import { Button, Grid, InputLabel } from "@mui/material";
import { users } from "../../../../locale/ru/users";

const UserForm = (props) => {
  const { onSubmit, initialValues, textButton } = props;

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
                flexDirection: "column",
                justifyContent: "flex-start",
                alignItems: "flex-start",
                flexWrap: "wrap"
              }}
            >
              <Grid container spacing={3}>
                <Grid item xs={12}>
                  <InputLabel >{users.name}
                    </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="name"
                    // type="name"
                    variant="outlined"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}>
                  <InputLabel >{users.lastName}
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="lastName"
                    // type="lastName"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}>
                  <InputLabel >{users.fathersName}
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="fathersName"
                    // type="fathersName"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}>
                  <InputLabel >Login
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="login"
                    // type="login"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}>
                  <InputLabel >{users.email}
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="email"
                    // type="email"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                
                <Grid item xs={12}>
                  <InputLabel >{users.role}
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="roles"
                    // type="roles"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}>
                  <InputLabel >{users.position}
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="position"
                    // type="position"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}>
                  <InputLabel >{users.password}
                  </InputLabel>
                  <Controls.ValidationFormTextfield
                    name="password"
                    // type="password"
                    type="text"
                    fullWidth
                    size="small"
                    required
                  />
                </Grid>
                <Grid item xs={12}></Grid>
              </Grid>
            </div>
            {textButton ? (<Button
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
              {textButton}
            </Button>) 
              : null}
          </Form>
        )}
      </Formik>
    </>
  );
};

export default UserForm;
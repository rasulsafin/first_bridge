import { Component } from "react";
import {
  Button,
  Col,
  Form,
  FormControl,
  FormGroup,
  FormLabel,
  Modal,
  ModalBody,
  ModalHeader,
  Row
} from "react-bootstrap";
import { axiosInstance } from "../../../../../axios/axiosInstance";

export class AddUserModal extends Component {
  constructor() {
    super();
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();
    axiosInstance.post("api/users",
      {
        name: event.target.name.value,
        login: event.target.login.value,
        email: event.target.email.value,
        password: event.target.password.value,
      }).then(r => console.log(r));
    
    
  }

  render() {
    return (
      <div className="container">
        <Modal {...this.props} aria-labelledby="contained-modal-title-vcenter" centered>
          <ModalHeader closeButton>
            <Modal.Title id="contained-modal-title-vcenter">
              Add User
            </Modal.Title>
          </ModalHeader>
          <ModalBody>
            <Row>
              <Col sm={6}>
                <Form
                  onSubmit={this.handleSubmit}
                >
                  <FormGroup>
                    <FormLabel>
                      User Name
                    </FormLabel>
                    <FormControl
                      type="text"
                      name="name"
                      required
                      placeholder="Name"
                    />
                  </FormGroup>
                  <FormGroup>
                    <FormLabel>
                      Login
                    </FormLabel>
                    <FormControl
                      type="text"
                      name="login"
                      required
                      placeholder="Login"
                    />
                  </FormGroup>
                  <FormGroup>
                    <FormLabel>
                      Email
                    </FormLabel>
                    <FormControl
                      type="text"
                      name="email"
                      required
                      placeholder="Email"
                    />
                  </FormGroup>
                  <FormGroup>
                    <FormLabel>
                      pass
                    </FormLabel>
                    <FormControl
                      type="text"
                      name="password"
                      required
                      placeholder="pass"
                    />
                  </FormGroup>
                  <FormGroup>
                    <Button
                      variant="primary"
                      type="submit"
                      className="mt-3"
                    >
                      Add User
                    </Button>
                  </FormGroup>
                </Form>
              </Col>
            </Row>
          </ModalBody>
          <Modal.Footer>
            <Button
              variant="danger"
              onClick={this.props.onHide}
            >
              Close
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}
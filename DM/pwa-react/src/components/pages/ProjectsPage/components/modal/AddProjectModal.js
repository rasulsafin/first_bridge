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


export class AddProjectModal extends Component {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit(event) {
    event.preventDefault();
    axiosInstance.post("api/project", {
      ProjectId: null,
      title: event.target.title.value
    }).then(r => console.log(r));
  }
    
  render() {
    return (
      <div className="container">
        <Modal {...this.props} aria-labelledby="contained-modal-title-vcenter" centered>
          <ModalHeader closeButton>
            <Modal.Title id="contained-modal-title-vcenter">
              Add Project
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
                      Project Title
                    </FormLabel>
                    <FormControl
                      type="text"
                      name="title"
                      required
                      placeholder="ProjectTitle"
                    />
                  </FormGroup>
                  <FormGroup>
                    <Button
                      variant="primary"
                      type="submit"
                      className="mt-3"
                    >
                      Add Project
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
import {Component} from "react";
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

export class AddProjectModal extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this)
    }

    handleSubmit(event) {
        event.preventDefault();
        fetch('https://localhost:5001/api/project', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ProjectId: null,
                title: event.target.title.value
            })
        })
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
        )
    }
}
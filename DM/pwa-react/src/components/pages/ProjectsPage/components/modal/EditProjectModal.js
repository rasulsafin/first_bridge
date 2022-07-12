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

export class EditModal extends Component {
    constructor(props) {
        super(props);
        this.handleSubmit = this.handleSubmit.bind(this)
    }

    handleSubmit(event) {
        event.preventDefault();
        fetch(process.env.REACT_APP_BACKEND_URL + 'project', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                ProjectId: event.target.ProjectId.value,
                title: event.target.ProjectTitle.value
            })
        })
            .then(res => res.json())
            .then((result) => {
                    alert(result);
                },
                (error) => {
                    alert('Failed');
                })
    }

    render() {
        return (
            <div className="container">
                <Modal {...this.props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
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
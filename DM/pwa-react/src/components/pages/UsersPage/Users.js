import { Component } from "react";
import { AddUserModal } from "./components/modal/AddUserModal";
import { Button, ButtonToolbar } from "react-bootstrap";
import UsersGrid from "./components/UsersGrid";

export class Users extends Component {
  constructor(props) {
    super(props);
    this.state = {
      users: [],
      addModalShow: false
    };
  }

  render() {
    let addModalClose = () => this.setState({ addModalShow: false });
    return (
      <div className="p-4">
        <h1 className="mb-4">User</h1>
        <div>
        </div>
        <ButtonToolbar>
          <Button
            variant="primary"
            onClick={() => this.setState({ addModalShow: true })}
          >
            Add user
          </Button>
          <AddUserModal
            show={this.state.addModalShow}
            onHide={addModalClose}
          >
          </AddUserModal>
        </ButtonToolbar>
        <UsersGrid />
      </div>
    );
  }
}

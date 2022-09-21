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
    return (
      <div className="p-3">
        <h1 className="mb-4">User</h1>
        <div>
        </div>
        <ButtonToolbar>
          <AddUserModal />
        </ButtonToolbar>
        <UsersGrid />
      </div>
    );
  }
}

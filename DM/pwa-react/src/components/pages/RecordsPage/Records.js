import { Component } from "react";
import { Button, ButtonToolbar } from "react-bootstrap";
import RecordsGrid from "./components/RecordsGrid";

export class Records extends Component {
  constructor(props) {
    super(props);
    this.state = {
      records: [],
      addModalShow: false
    };
  }

  render() {
    // let addModalClose = () => this.setState({addModalShow: false});
    return (
      <div className="p-4">
        <h1 className="mb-4">Records</h1>
        <div>
        </div>
        <ButtonToolbar>
          <Button
            variant="primary"
            onClick={() => this.setState({ addModalShow: true })}
          >
            Add Record
          </Button>
          {/*<AddUserModal*/}
          {/*    show={this.state.addModalShow}*/}
          {/*    onHide={addModalClose}*/}
          {/*>*/}
          {/*</AddUserModal>*/}
        </ButtonToolbar>
        <RecordsGrid />
      </div>
    );
  }
}

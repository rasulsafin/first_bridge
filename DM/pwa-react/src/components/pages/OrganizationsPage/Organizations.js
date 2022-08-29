import { Component } from "react";
import OrganizationsGrid from "./components/OrganizationsGrid";

export class Organizations extends Component {
  constructor(props) {
    super(props);
    this.state = {
      organizations: [],
      addModalShow: false,
    };
  }

  render() {
    return (
      <div className="p-4">
        <h1 className="mb-4">Projects</h1>
        {/*<AddProjectModal />*/}
        <OrganizationsGrid />
      </div>
    );
  }
}

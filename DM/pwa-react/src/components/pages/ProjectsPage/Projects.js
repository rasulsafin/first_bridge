import { Component } from "react";
import { Button, ButtonToolbar } from "react-bootstrap";
import { AddProjectModal } from "./components/modal/AddProjectModal";
import ProjectsGrid from "./components/ProjectsGrid";

export class Projects extends Component {
  constructor(props) {
    super(props);
    this.state = {
      projects: [],
      addModalShow: false,
    };
  }

  render() {
    return (
      <div className="p-4">
        <h1 className="mb-4">Projects</h1>
        <AddProjectModal />
        <ProjectsGrid />
      </div>
    );
  }
}

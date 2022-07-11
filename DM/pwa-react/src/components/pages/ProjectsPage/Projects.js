import {Component} from "react";
import Button from "@mui/material/Button";
import CreateUser from "../UsersPage/components/CreateUser";
import CollapsibleTable from "../UsersPage/components/UsersGrid";

export class Projects extends Component {
    constructor() {
        super();
    }


    getProjects = async () => {
        const response = await fetch('https://localhost:5001/api/project');

        console.log(response);
        
        const projectsData = await response.json();
        this.setState({
            projects: projectsData
        })

    }

    componentDidMount() {
        this.getProjects();
    }

    render() {
        return (
            <div className="mt-5 d-flex justify-content-left">
                <h1>Projects</h1>
                <p></p>
                <CollapsibleTable users={this.state.projects} />
            </div>
        )
    }
}

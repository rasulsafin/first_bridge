import {Component} from "react";
import CreateUser from "./components/CreateUser";
import CollapsibleTable from "./components/UsersGrid";

export class Users extends Component {
    constructor() {
        super();
        this.state = {
            users: []
        }
    }

    getUsers = async () => {
        const response = await fetch('https://localhost:5001/api/users');

        const usersData = await response.json();
        this.setState({
            users: usersData
        })
    }
    
    componentDidMount() {
        this.getUsers();
    }

    render() {
        return (
            <div className="mt-5 d-flex justify-content-left">
                <h1>User</h1>
                <p></p>
                <CreateUser />
                <CollapsibleTable users={this.state.users} />
            </div>
        )
    }
}

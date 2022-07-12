import {Component} from "react";
import {AddUserModal} from "./components/modal/AddUserModal";
import {Button, ButtonToolbar} from "react-bootstrap";
import UsersGrid from "./components/UsersGrid";

export class Users extends Component {
    constructor(props) {
        super(props);
        this.state = {
            users: [],
            addModalShow: false
        }
    }
    
    

    getUsers = async () => {
        const response = await fetch('https://localhost:5001/api/users');

        const usersData = await response.json();
        
        console.log(usersData)
        this.setState({
            users: usersData
        })
    }
    
    componentDidMount() {
        this.getUsers();
    }

    render() {
        let addModalClose = () => this.setState({addModalShow: false});
        return (
            <div className="m-5">
                <h1 className="mb-4">User</h1>
                <div>
                </div>
                <ButtonToolbar>
                    <Button
                        variant="primary"
                        onClick={() => this.setState({addModalShow: true})}
                    >
                        Add user
                    </Button>
                    <AddUserModal
                        show={this.state.addModalShow}
                        onHide={addModalClose}
                    >
                    </AddUserModal>
                </ButtonToolbar>
                
                {/*<CreateUser />*/}
                <UsersGrid users={this.state.users} />
            </div>
        )
    }
}

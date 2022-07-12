import {Table} from "react-bootstrap";

export default function UsersGrid(props) {

    return (
        <div>
            <Table
                className="mt-5"
                striped
                bordered
                hover
                size="sm"
                data-pagination="true"
            >
                <thead>
                <tr>
                    <td>Name</td>
                    <td>Login</td>
                    <td>Email</td>
                    <td>Edit</td>
                </tr>
                </thead>
                <tbody>
                {props.users.map(user =>
                    <tr key={user.id}>
                        <td data-sortable="true">{user.name}</td>
                        <td>{user.login}</td>
                        <td>{user.email}</td>
                        <td>Edit / Delete</td>
                    </tr>)}
                </tbody>
            </Table>
        </div>
    )
}
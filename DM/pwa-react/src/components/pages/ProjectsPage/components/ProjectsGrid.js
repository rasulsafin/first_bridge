import {Table} from "react-bootstrap";

export default function ProjectsGrid(props) {

    return (
        <div>
            <Table
                className="mt-5"
                striped bordered hover size="sm"
            >
                <thead>
                <tr>
                    <td>Project title</td>
                    <td>User</td>
                    <td>Items</td>
                    <td>Edit</td>
                </tr>
                </thead>
                <tbody>
                {props.projects.map(project =>
                    <tr key={project.id}>
                        <td>{project.title}</td>
                        <td> {project.users.map(user => <tr key={user.id}>
                            <td>{user.name}</td>
                        </tr>)}</td>
                        <td> {project.items.map(item => <tr key={item.id}>
                            <td>{item.name}</td>
                        </tr>)}</td>
                        <td>Edit / Delete</td>
                    </tr>)}
                </tbody>
            </Table>
        </div>
    )
}
export const UserBadge = props => {

    console.log(props.users);

    return (
        <div>
            {props.users.map(user => <div key={user.name}
                style={{
                    border: "2px solid black",
                    width: "400px",
                    height: "250",
                    background: "lightblue",
                    padding: "10px",
                    margin: "10px"
                }}
            >
                Имя: <strong>{user.name}</strong>
                <p>
                    Логин: <strong>{user.login}</strong>
                </p>
                E-mail: <strong>{user.email}</strong>
            </div>)}
        </div>
    )
}
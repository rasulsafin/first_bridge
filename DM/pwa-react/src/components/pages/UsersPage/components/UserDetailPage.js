import { useParams } from "react-router";
import { useSelector } from "react-redux";
import { selectAllUsers } from "../../../../services/usersSlice";
import { Link } from "react-router-dom";
import { BsArrowLeftSquareFill } from "react-icons/bs";

export const UserDetailPage = () => {
  const { id } = useParams();
  const users = useSelector(selectAllUsers);

  const user = users.find(user => user.id === Number(id));

  return (
    <div className="p-4">
      <Link to="/users">
        <span style={{ color: "black", textDecoration: "none" }}>
          <BsArrowLeftSquareFill size={30} color="#1d62ad" />
        </span>
      </Link>
      <h1>User Detail Page</h1>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {user.name}</span></p>
        <p>Login: <span style={{ fontSize: 32 }}> {user.login}</span></p>
      </div>
    </div>
  );
};
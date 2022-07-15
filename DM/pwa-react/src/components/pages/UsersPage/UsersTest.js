import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { GetUsers } from "../../../services/users";

export const UsersTest = () => {
  const users = useSelector(state => state.usersReducer.users);
  const dispatch = useDispatch();
  
  useEffect(() => {
    GetUsers(dispatch)
  }, []);
  
  console.log('UserTest', users);
  
  return (
    <div>
  <table>
    <tbody>
    {users.map(n => <tr key={n.id}>{n.name}</tr>)}
    </tbody>
  </table>
    </div>
  )
}
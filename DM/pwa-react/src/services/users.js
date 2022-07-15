import { axiosInstance } from "../axios/axiosInstance";
import { ActionCreators } from "../redux/usersReducer";

export const GetUsers = async (dispatch) => {
  try {

    const data = await axiosInstance.get("api/users");

    // const data = [
    //   {id: 1, name: 'qwerty'},
    //   {id: 2, name: 'zxcvbn'},
    //   {id: 3, name: 'asdfgg'},
    // ]
    
    console.log("GetUsers", data);

    await dispatch(ActionCreators.setUsers(data));

  } catch {
    console.log("Error");
  }
};
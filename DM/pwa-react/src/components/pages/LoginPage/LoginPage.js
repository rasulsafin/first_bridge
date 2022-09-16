import { useState, useEffect } from "react";
import { useAuth } from "../../../hooks/useAuth";
import { axiosInstance } from "../../../axios/axiosInstance";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { setAuthUser } from "../../../services/authSlice";

export const LoginPage = () => {
  const { setAuth } = useAuth();

  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  const dispatch = useDispatch();
  const userName = useSelector((state) => state.auth.name);
  const userRole = useSelector((state) => state.auth.role);
  
  useEffect(() => {
    setErrMsg("");
  }, [user, pwd]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axiosInstance.post("api/users/authenticate",
        JSON.stringify({ login: user, password: pwd }),
        {
          headers: { "Content-Type": "application/json-patch+json" },
          withCredentials: true
        }
      );
      
      dispatch(setAuthUser({
        id: response.data.id,
        name: response.data.name,
        email: response.data.email,
        token: response.data.token,
        login: response.data.login,
        role: response.data.role,
        organizationId: response.data.organizationId,
      }));

      console.log(JSON.stringify(response?.data));

      const token = response?.data?.token;

      // const roles = response?.data?.roles;

      setUser("");
      setPwd("");
      setAuth({ user, token });
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("user",  response.data.name);
      localStorage.setItem("role",  response.data.role);
      localStorage.setItem("organizationId",  response.data.organizationId);
      setSuccess(true);

    } catch (err) {
      if (!err?.response) {
        setErrMsg("No Server Response");
      } else if (err.response?.status === 400) {
        setErrMsg("Missing Username or Password");
      } else if (err.response?.status === 401) {
        setErrMsg("Unauthorized");
      } else {
        setErrMsg("Login Failed");
      }
    }
  };

  return (
    <>
      {success ? (
        <section>
          <h1>You are logged in, {userName}!</h1>
          <h1>Your role is {userRole}!</h1>
          <br />
        </section>
      ) : (
        <section>
          <h1>Sign In</h1>
          <form onSubmit={handleSubmit}>
            <label htmlFor="username">Username:</label>
            <input
              type="text"
              id="username"
              autoComplete="off"
              onChange={(e) => setUser(e.target.value)}
              value={user}
              required
            />

            <label htmlFor="password">Password:</label>
            <input
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              value={pwd}
              required
            />
            <button>Sign In</button>
          </form>
          <p>
            Need an Account?<br />
            <span className="line">
              <Link to="/registration">
                Sign Up 
              </Link>
            </span>
          </p>
        </section>
      )}
    </>
  );
};

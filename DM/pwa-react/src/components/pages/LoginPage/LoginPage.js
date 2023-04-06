import React, { useEffect, useState } from "react";
import { Controls } from "../../controls/Controls";
import "./LoginPage.css";
import { useAuth } from "../../../hooks/useAuth";
import { useDispatch } from "react-redux";
import { axiosInstance } from "../../../axios/axiosInstance";
import { setAuthUser } from "../../../services/authSlice";
import { useNavigate } from "react-router";

export const LoginPage = () => {
  const { setAuth } = useAuth();
  const navigate = useNavigate();
  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);
  const dispatch = useDispatch();

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
        organizationId: response.data.organizationId
      }));

      console.log(JSON.stringify(response?.data));

      const token = response?.data?.token;

      // const roles = response?.data?.roles;

      setUser("");
      setPwd("");
      setAuth({ user, token });
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("user", response.data.name);
      localStorage.setItem("role", response.data.role);
      localStorage.setItem("organizationId", response.data.organizationId);
      setSuccess(true);
      navigate(`/`);
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
    <div className="Auth-form-container">
      <form 
        className="Auth-form"
        onSubmit={handleSubmit}
      >
        <div className="Auth-form-content">
          <h3 className="Auth-form-title">Вход</h3>
          <label>Login/Email</label>
          <Controls.Input
            placeholder="Login/Email"
            onChange={(e) => setUser(e.target.value)}
            value={user}
            required
          />
          <label>Пароль</label>
          <Controls.Input
            type="password"
            placeholder="Пароль"
            onChange={(e) => setPwd(e.target.value)}
            value={pwd}
            required
          />
          <div className="d-grid gap-2 mt-3">
            <Controls.Button
              className="mt-1"
              variant="contained"
              type="submit">
              Войти
            </Controls.Button>
          </div>
          <p className="forgot-password text-right mt-2">
            Забыли <a href="#">пароль?</a>
          </p>
        </div>
      </form>
    </div>
  );
};

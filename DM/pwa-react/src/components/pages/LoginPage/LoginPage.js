import React, { useState } from "react";
import { Controls } from "../../controls/Controls";
import "./LoginPage.css";
import { useDispatch } from "react-redux";
import { login } from "../../../services/authSlice";
import { useNavigate } from "react-router";

export const LoginPage = () => {
  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();

    dispatch(login({ user, pwd })).then(() => {
      navigate("/");
    });
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

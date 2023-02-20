import React from "react";
import { Controls } from "../../controls/Controls";
import "./LoginPage1.css";

const LoginPage1 = () => {
  

  return (
    <div className="Auth-form-container">
      <form className="Auth-form">
        <div className="Auth-form-content">
          <h3 className="Auth-form-title">Вход</h3>
          <label>Email</label>
          <Controls.Input
            type="email"
            placeholder="Email"
          />
          <label>Пароль</label>
          <Controls.Input
            type="password"
            placeholder="Пароль"
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

export default LoginPage1;
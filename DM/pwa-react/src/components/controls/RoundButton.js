import React from "react";
import { ReactComponent as PlusIcon } from "../../assets/icons/plus.svg";
import "./Controls.css";

const RoundButton = (props) => {
  const { ...other } = props;

  return (
      <button
        className="add-round-button"
        type="button"
        {...other}
      >
        <PlusIcon />
      </button>
  );
};

export default RoundButton;
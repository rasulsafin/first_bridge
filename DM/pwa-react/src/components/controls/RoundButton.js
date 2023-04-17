import React from "react";
import { ReactComponent as PlusIcon } from "../../assets/icons/plus.svg";
import "./Controls.css";

const RoundButton = (props) => {
  const { ...other } = props;

  return (
    <div>
      <button
        className="add-round-button"
        {...props}
      >
        <PlusIcon />
      </button>
    </div>
  );
};

export default RoundButton;
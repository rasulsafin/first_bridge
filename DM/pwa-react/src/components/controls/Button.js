import React from "react";
import { Button as MuiButton } from "@mui/material";

export function Button(props) {
  const { size, color, variant, ...other } = props;

  return (
    <MuiButton
      className="m-3"
      variant={variant || "outlined"}
      size={size || "small"}
      color={color || "primary"}
      {...other}
      >{props.children}
    </MuiButton>
  );
}

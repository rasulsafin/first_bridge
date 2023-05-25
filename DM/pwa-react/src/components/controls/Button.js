import React from "react";
import { Button as MuiButton } from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";

const { palette } = createTheme();
const { augmentColor } = palette;
const createColor = (mainColor) => augmentColor({ color: { main: mainColor } });
const theme = createTheme({
  palette: {
    primary: createColor("#2D2926"),
    secondary: createColor("#FFF")
  },
  typography: {
    button: {
      textTransform: "none"
    }
  }
});

export function Button(props) {
  const { size, color, variant, className, ...other } = props;

  return (
    <ThemeProvider theme={theme}>
      <MuiButton
        className={className || "m-3"}
        variant={variant || "outlined"}
        size={size || "small"}
        color={color || "primary"}
        {...other}
      >{props.children}
      </MuiButton>
    </ThemeProvider>
  );
}

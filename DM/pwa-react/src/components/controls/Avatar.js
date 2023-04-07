import React from "react";
import { Avatar as MuiAvatar } from "@mui/material";

const Avatar = (fullName) => {

  function stringToColor(string: string) {
    let hash = 0;
    let i;

    /* eslint-disable no-bitwise */
    for (i = 0; i < string.length; i += 1) {
      hash = string.charCodeAt(i) + ((hash << 5) - hash);
    }

    let color = "#";

    for (i = 0; i < 3; i += 1) {
      const value = (hash >> (i * 8)) & 0xff;
      color += `00${value.toString(16)}`.slice(-2);
    }
    /* eslint-enable no-bitwise */

    return color;
  }

  function stringAvatar(fullName) {
    return {
      sx: {
        bgcolor: stringToColor(fullName.toString())
      },
      children: `${String(fullName).split(" ")[0][0]}${String(fullName).split(" ")[1][0]}`
    };
  }

  return (
    <>
      <MuiAvatar {...stringAvatar(fullName.fullName)}
      />
    </>
  );
};

export default Avatar;
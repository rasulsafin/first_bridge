import React, { useEffect } from "react";
import { IconButton } from "@mui/material";
import IfcComponent from "../../ifc/IfcComponent";
import { ReactComponent as TaskIcon } from "../../../assets/icons/task.svg";
import { ReactComponent as ModelIcon } from "../../../assets/icons/models.svg";
import { ReactComponent as LayerIcon } from "../../../assets/icons/layers.svg";
import "./Models.css";

export const Models = () => {
  const [showElement, setShowElement] = React.useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShowElement(false);
    }, 3000);
    return () => clearTimeout(timer);
  }, []);

  const handleToShowAppbar = () => {
    setShowElement(true);
  };

  const handleToCloseAppbar = () => {
    const timer = setTimeout(() => {
      setShowElement(false);
    }, 3000);
    return () => clearTimeout(timer);
  };

  const styleIconButton = {
    ml: 1,
    "&.MuiButtonBase-root:hover": {
      background: "transparent"
    }
  };

  return (
    <div>
      <div
        className="container-for-appbar"
        onMouseEnter={() => handleToShowAppbar()}
        onMouseLeave={() => handleToCloseAppbar()}
      >
        {showElement &&
          <div
            className="appbar-for-models"
          >
            <IconButton
              sx={styleIconButton}
              onClick={() => console.log()}
            >
              <TaskIcon
                className="icon-appbar"
              />
            </IconButton>
            <IconButton
              sx={styleIconButton}
              onClick={() => console.log()}
            >
              <ModelIcon
                className="icon-appbar"
              />
            </IconButton>
            <IconButton
              sx={styleIconButton}
              onClick={() => console.log()}
            >
              <LayerIcon
                className="icon-appbar"
              />
            </IconButton>
          </div>
        }
      </div>
      <IfcComponent />
    </div>
  );
};

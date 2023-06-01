import React from "react";
import { Divider, IconButton, Menu, MenuItem, Typography } from "@mui/material";
import { ReactComponent as LayerIcon } from "../../assets/icons/layers.svg";
import { ReactComponent as CreateTaskIcon } from "../../assets/icons/createTask.svg";
import { ReactComponent as InfoIcon } from "../../assets/icons/info.svg";
import "./IfcComponent.css";
import { useNavigate } from "react-router";
import { useDispatch } from "react-redux";
import { openDrawer } from "../../services/controlUISlice";

const styleIconButton = {
  paddingY: "4px",
  paddingRight: 2,
  marginRight: "5px",
  margin: 0,
  "&.MuiButtonBase-root:hover": {
    background: "transparent"
  }
};

const MenuOfElementModel = (props) => {
  const { anchorEl, open, onClose } = props;
  const navigate = useNavigate();
  const dispatch = useDispatch();
  
  const handleToCreateRecord = () => {
    navigate(`/records`, { state: { open: true } });
  };

  return (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: "center",
        horizontal: "center"
      }}
      disableAutoFocusItem
      open={open}
      onClose={onClose}
      PaperProps={{
        style: {
          width: 248,
          height: 186,
          borderRadius: 12
        }
      }}
    >
      <MenuItem>
        <IconButton
          sx={styleIconButton}
          onClick={() => console.log()}
        >
          <LayerIcon className="icon-dialog-model" />
          <Typography>В слои</Typography>
        </IconButton>
      </MenuItem>
      <Divider />
      <MenuItem>
        <IconButton
          sx={styleIconButton}
          onClick={handleToCreateRecord}
        >
          <CreateTaskIcon className="icon-dialog-model" />
          <Typography>Создать задачу</Typography>
        </IconButton>
      </MenuItem>
      <Divider />
      <MenuItem>
        <IconButton
          sx={styleIconButton}
          onClick={() => dispatch(openDrawer())}
        >
          <InfoIcon className="icon-dialog-model" />
          <Typography>Информация</Typography>
        </IconButton>
      </MenuItem>
    </Menu>
  );
};

export default MenuOfElementModel;
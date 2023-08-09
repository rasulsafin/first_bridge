import React from "react";
import { IconButton, Menu, MenuItem } from "@mui/material";
import { ReactComponent as EditIcon } from "../../../../assets/icons/edit.svg";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";

export const MenuProjectCard = ({ anchorEl, handleOpenDialog, handleOpenModal, open, onClose }) => {
  return (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: "top",
        horizontal: "right"
      }}
      keepMounted
      transformOrigin={{
        vertical: "top",
        horizontal: "right"
      }}
      open={open}
      onClose={onClose}
    >
      <MenuItem
        onClick={handleOpenModal}>
        <IconButton>
          <EditIcon />
        </IconButton>
        Редактировать
      </MenuItem>
      <MenuItem
        onClick={handleOpenDialog}>
        <IconButton>
          <TrashIcon />
        </IconButton>
        В архив
      </MenuItem>
    </Menu>
  );
};

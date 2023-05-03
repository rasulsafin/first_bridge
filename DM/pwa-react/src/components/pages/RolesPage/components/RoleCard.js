import React, { useState } from "react";
import "../Roles.css";
import {
  Grid,
  ListItem,
  ListItemButton,
  ListItemText
} from "@mui/material";
import { Controls } from "../../../controls/Controls";

export const RoleCard = (props) => {
  const { role } = props;
  const [openModal, setOpenModal] = useState(false);

  const handleOpenModal = () => {
    setOpenModal(true);
  };

  const handleCloseModal = () => {
    setOpenModal(false);
  };

  return (
    <>
      <ListItem
        sx={{
          height: "51px",
          backgroundColor: "#FFF",
          marginY: "10px",
          padding: "12px",
          borderRadius: "10px"
        }}
        dense
        key={role.id}
      >
        <ListItemButton
          onClick={handleOpenModal}
        >
          <Grid container>
            <Grid item lg={6}>
              <ListItemText
                primary={role.name}
              />
            </Grid>
            <Grid item lg={6}>
              <ListItemText
                primary={`Description`}
              />
            </Grid>
          </Grid>
        </ListItemButton>
      </ListItem>
      <Controls.Modal
        titleModal="Редактирование роли"
        open={openModal}
        onClose={handleCloseModal}
      >

      </Controls.Modal>
    </>
  );
};

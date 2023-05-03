import * as React from "react";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchRoles,
  selectAllRoles
} from "../../../services/rolesSlice";
import { useEffect, useState } from "react";
import { Grid, List } from "@mui/material";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import { useNavigate } from "react-router";
import "./Roles.css";
import { RoleCard } from "./components/RoleCard";
import { SearchAndSortRoleToolbar } from "./components/SearchAndSortRoleToolbar";

export const Roles = () => {
  const navigate = useNavigate();
  const [openModal, setModal] = useState(false);
  const dispatch = useDispatch();
  const roles = useSelector(selectAllRoles);
  const title = "Редактирование роли";

  useEffect(() => {
    dispatch(fetchRoles());
  }, [dispatch]);

  const handleModalOpen = () => {
    setModal(true);
  };

  const handleModalClose = () => {
    setModal(false);
  };

  const handleNavigateToUsersPage = () => {
    navigate(`/users`);
  };

  const iconStar = (
    <StarIcon className="icon-role active" />
  );

  return (
    <div>
      <Grid direction="row" container>
        <Grid item xs={9} sm={9} lg={9}>
          <h3 className="mb-2">Роли</h3>
        </Grid>
        <Grid container item xs={3} sm={3} lg={3} justifyContent="flex-end">
          <Grid item>
            <Controls.Button
              startIcon={<BurgerIcon />}
              className="m-0"
              style={{
                backgroundColor: "#FFF",
                color: "#2D2926",
                border: "none"
              }}
              onClick={handleNavigateToUsersPage}
            >Участники</Controls.Button>
            <Controls.Button
              startIcon={iconStar}
              className="m-0"
              style={{
                backgroundColor: "#2D2926",
                color: "#FFF",
                border: "none"
              }}
            >Роли</Controls.Button>
          </Grid>
        </Grid>
      </Grid>
      <div className="toolbar-project">
        <SearchAndSortRoleToolbar />
      </div>
      <List>
        {roles.map(role =>
          <RoleCard
            key={role.id}
            role={role}
            // openModal={handleModalOpen}
            // onClose={() => setModal(false)}
            // open={openModal}
          />)}
      </List>
      <Controls.Modal
        titleModal={title}
        open={openModal}
        onClose={handleModalClose}
      >
        {"test"}
      </Controls.Modal>
      <Controls.RoundButton
        onClick={handleModalOpen}
      >
      </Controls.RoundButton>
    </div>
  );
};

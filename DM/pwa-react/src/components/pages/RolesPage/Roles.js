import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";
import { Grid, List } from "@mui/material";
import {
  fetchRoles,
  selectAllRoles
} from "../../../services/rolesSlice";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import "./Roles.css";
import { Controls } from "../../controls/Controls";
import { RoleCard } from "./components/RoleCard";
import { SearchAndSortRoleToolbar } from "./components/SearchAndSortRoleToolbar";
import { useModal } from "../../../hooks/useModal";
import { RoleCreateModal } from "./components/RoleCreateModal";
import "../../layout/Layout.css";

export const Roles = () => {
  const navigate = useNavigate();
  const [openModal, toggleModal] = useModal();
  const dispatch = useDispatch();
  const roles = useSelector(selectAllRoles);
  const title = "Редактирование роли";

  useEffect(() => {
    dispatch(fetchRoles());
  }, [dispatch]);

  const handleNavigateToUsersPage = () => {
    navigate(`/users`);
  };

  const iconStar = (
    <StarIcon className="icon-role active" />
  );

  return (
    <div className="component-container">
      <div className="header-toolbar">
        <Grid direction="row" container>
          <Grid item xs={9} sm={9} lg={9}>
            <div className="header-title">Роли</div>
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
      {openModal &&
        <RoleCreateModal
          toggleModal={toggleModal}
          roles={roles}
        />
      }
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
};

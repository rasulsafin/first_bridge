import React, { useEffect } from "react";
import { useNavigate } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { Box, Grid } from "@mui/material";
import {
  fetchUsers,
  selectAllUsers
} from "../../../services/usersSlice";
import { UserCard } from "./components/UserCard";
import "./Users.css";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import { SearchAndSortUserToolbar } from "./components/SearchAndSortUserToolbar";
import { useModal } from "../../../hooks/useModal";
import { fetchProjects, selectAllProjects } from "../../../services/projectsSlice";
import { Controls } from "../../controls/Controls";
import { fetchRoles, selectAllRoles } from "../../../services/rolesSlice";
import { UserCreateModal } from "./components/UserCreateModal";

export const Users = () => {
  const [openModal, toggleModal] = useModal();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const projects = useSelector(selectAllProjects);
  const roles = useSelector(selectAllRoles);
  const title = "Добавление участника";

  console.log(openModal);

  useEffect(() => {
    dispatch(fetchUsers());
    dispatch(fetchRoles());
    dispatch(fetchProjects());
  }, [dispatch]);

  const handleNavigateToRolesPage = () => {
    navigate(`/roles`);
  };

  const iconBurger = (
    <BurgerIcon className="icon-role active" />
  );

  return (
    <div>
      <Grid direction="row" container>
        <Grid item xs={9} sm={9} lg={9}>
          <h3 className="mb-2">Участники</h3>
        </Grid>
        <Grid container item xs={3} sm={3} lg={3} justifyContent="flex-end">
          <Grid item>
            <Controls.Button
              startIcon={iconBurger}
              className="m-0"
              style={{
                backgroundColor: "#2D2926",
                color: "#FFF",
                border: "none"
              }}
            >Участники</Controls.Button>
            <Controls.Button
              startIcon={<StarIcon />}
              className="m-0"
              style={{
                backgroundColor: "#FFF",
                color: "#2D2926",
                border: "none"
              }}
              onClick={handleNavigateToRolesPage}
            >Роли</Controls.Button>
          </Grid>
        </Grid>
      </Grid>
      <Box>
        <SearchAndSortUserToolbar />
      </Box>
      <div className="user-cards-container">
        {users.map(user => <UserCard key={user.id} user={user} />)}
      </div>
      {openModal &&
        <UserCreateModal
          toggleModal={toggleModal}
          roles={roles}
          projects={projects}
          title={title}
        />
      }
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
};

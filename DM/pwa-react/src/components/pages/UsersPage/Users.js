import * as React from "react";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import {
  addNewUser,
  fetchUsers,
  selectAllUsers
} from "../../../services/usersSlice";
import { useEffect, useState } from "react";
import { UserCard } from "./components/UserCard";
import "./Users.css";
import { Box, Button, Grid, IconButton, List, Modal } from "@mui/material";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import UserForm from "./components/UserForm";
import { getInitialValues } from "./utils/getInitialValues";
import { useNavigate } from "react-router";
import { SearchAndSortUserToolbar } from "./components/SearchAndSortUserToolbar";
import { ReactComponent as CancelIcon } from "../../../assets/icons/cancel.svg";
import { useModal } from "../../../hooks/useModal";
import { selectAllProjects } from "../../../services/projectsSlice";
import { ProjectCard } from "../ProjectsPage/components/ProjectCard";


const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "none",
  borderRadius: 3,
  boxShadow: 24,
  pt: 2,
  px: 4,
  pb: 3
};

function ChildModal() {
  const [openModal, toggleModal] = useModal();

  return (
    <React.Fragment>
      <Controls.Button onClick={toggleModal}>Настроить доступ</Controls.Button>
      <Modal
        open={openModal}
        onClose={toggleModal}
        aria-labelledby="child-modal-title"
        aria-describedby="child-modal-description"
      >
        <Box sx={{ ...style, width: 500 }}>
          <h2 id="child-modal-title">Доступ к проектам</h2>
          <p id="child-modal-description">
            Lorem ipsum, dolor sit amet consectetur adipisicing elit.
          </p>
          <Controls.Button
            // onClick={handleAddUsersToProject}
          >Продолжить</Controls.Button>
          <Controls.Button
            onClick={toggleModal}
          >Отменить</Controls.Button>
          {/*<Button onClick={toggleModal}>Close Child Modal</Button>*/}
        </Box>
      </Modal>
    </React.Fragment>
  );
}

export const Users = () => {
  const [openModal, toggleModal] = useModal();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const projects = useSelector(selectAllProjects);
  const initialValues = getInitialValues();
  const title = "Добавление участника";

  useEffect(() => {
    dispatch(fetchUsers());
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
      <Controls.Modal
        titleModal={title}
        open={openModal}
        onClose={toggleModal}
      >
        <UserForm
          initialValues={initialValues}
          onSubmit={(values, formikHelpers) => {
            console.log(values);
            dispatch(addNewUser(values));
            formikHelpers.resetForm();
          }}
        />
        <Box>
          <h3>Доступ к проектам</h3>
          <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
            {projects ?
              projects.map(project =>
                <>
                  <Grid alignItems="center" container>
                    <Grid item xs={10}>
                      <ProjectCard key={project.id} project={project} />
                    </Grid>
                    <Grid item xs={2}>
                      <IconButton
                        aria-label="delete"
                        // onClick={() => handleDeleteUserFromProject(user.id, projectId)}
                      >
                        <CancelIcon />
                      </IconButton>
                    </Grid>
                  </Grid>
                </>
              )
              : null
            }
          </List>
        </Box>
        <Box>
          <ChildModal />
        </Box>
      </Controls.Modal>
      <Controls.RoundButton
        onClick={toggleModal}
      >
      </Controls.RoundButton>
    </div>
  );
};

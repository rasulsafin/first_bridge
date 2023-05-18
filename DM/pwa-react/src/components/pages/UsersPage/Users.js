import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import { useDispatch, useSelector } from "react-redux";
import { Box, Grid, List, ListItemButton, Modal } from "@mui/material";
import {
  addNewUser, 
  addProjectListToUser,
  fetchUsers,
  selectAllUsers
} from "../../../services/usersSlice";
import { UserCard } from "./components/UserCard";
import "./Users.css";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as StarIcon } from "../../../assets/icons/star.svg";
import UserForm from "./components/UserForm";
import { getInitialValues } from "./utils/getInitialValues";
import { SearchAndSortUserToolbar } from "./components/SearchAndSortUserToolbar";
import { useModal } from "../../../hooks/useModal";
import { selectAllProjects } from "../../../services/projectsSlice";
import { Controls } from "../../controls/Controls";
import { SearchAndSortProjectToolbar } from "../ProjectsPage/components/SearchAndSortProjectToolbar";
import { fetchRoles, selectAllRoles } from "../../../services/rolesSlice";

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

function ChildModal(props) {
  const { projects } = props;
  const dispatch = useDispatch();
  const [checked, setChecked] = useState([]);
  const [projectsAddToUser, setProjectsAddToUser] = useState([]);
  const [openModal, toggleModal] = useModal();

  const handleToggle = (project) => {
    const currentIndex = checked.indexOf(project.id);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(project.id);
      setProjectsAddToUser(usersAddToProject => [...usersAddToProject, { projectId: project.id }]);
    } else {
      newChecked.splice(currentIndex, 1);
      setProjectsAddToUser(usersAddToProject => usersAddToProject.filter(userProj => userProj.projectId !== project.id));
    }

    setChecked(newChecked);
  };

  const handleAddProjectsToUser = () => {
    dispatch(addProjectListToUser(projectsAddToUser));
    setProjectsAddToUser([]);
    setChecked([]);
  };
  
  return (
    <>
      <Controls.Button
        onClick={toggleModal}
        sx={{ width: "100%" }}
      >Настроить доступ</Controls.Button>
      <Modal
        open={openModal}
        onClose={toggleModal}
        aria-labelledby="child-modal-title"
        aria-describedby="child-modal-description"
      >
        <Box sx={{ ...style, width: 500 }}>
          <h2 id="child-modal-title">Доступ к проектам</h2>
          <SearchAndSortProjectToolbar />
          <p>Выбрано: {projectsAddToUser.length <= 0 ? 0 : projectsAddToUser.length}</p>
          <List style={{ height: "300px", gap: "2px", overflowY: "auto", overflowX: "hidden" }}>
            {projects.map(project =>
              <ListItemButton
                key={project.id}
                sx={{
                  margin: "2px",
                  padding: 0,
                  "&.Mui-selected": {
                    backgroundColor: "#FFF",
                    border: "1px gray solid",
                    borderRadius: "5px"
                  },
                  "&.Mui-selected:hover": {
                    backgroundColor: "#FFF"
                  }
                }}
                autoFocus={false}
                onClick={() => handleToggle(project)}
                dense
                selected={checked.indexOf(project.id) !== -1}
              >
                <Box
                  key={project.id}
                  sx={{
                    height: "73px",
                    backgroundColor: "#F4F4F4",
                    margin: "4px",
                    padding: "16px",
                    borderRadius: "5px"
                  }}>
                  <Grid direction="column" container>
                    <Grid item xs={2}>
                      <span style={{ "fontSize": "12px" }}>Участники: {project.users.length}</span>
                    </Grid>
                    <Grid item xs={10}>
                      <span style={{ "fontWeight": "bold" }}>{project.title}</span>
                    </Grid>
                  </Grid>
                </Box>
              </ ListItemButton>
            )}
          </List>
          <Controls.Button
            onClick={handleAddProjectsToUser}
          >Продолжить</Controls.Button>
          <Controls.Button
            onClick={toggleModal}
          >Отменить</Controls.Button>
        </Box>
      </Modal>
    </>
  );
}

export const Users = () => {
  const [openModal, toggleModal] = useModal();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const users = useSelector(selectAllUsers);
  const projects = useSelector(selectAllProjects);
  const roles = useSelector(selectAllRoles);
  const initialValues = getInitialValues();
  const title = "Добавление участника";

  useEffect(() => {
    dispatch(fetchUsers());
    dispatch(fetchRoles());
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
          roles={roles}
        />
        <Box sx={{ width: "58%" }}>
          <h3>Доступ к проектам</h3>
          <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
            {projects ?
              projects.map(project =>
                <Box
                  key={project.id}
                  sx={{
                    height: "73px",
                    backgroundColor: "#F4F4F4",
                    margin: "4px",
                    padding: "16px",
                    borderRadius: "5px"
                  }}>
                  <Grid direction="column" container>
                    <Grid item xs={2}>
                      <span style={{ "fontSize": "12px" }}>Участники: {project.users.length}</span>
                    </Grid>
                    <Grid item xs={10}>
                      <span style={{ "fontWeight": "bold" }}>{project.title}</span>
                    </Grid>
                  </Grid>
                </Box>
              )
              : null
            }
          </List>
          <ChildModal projects={projects} />
        </Box>
      </Controls.Modal>
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
};

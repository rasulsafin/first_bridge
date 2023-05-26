import { useDispatch } from "react-redux";
import React, { useState } from "react";
import { Box, Grid, List, ListItemButton, Modal } from "@mui/material";
import { useModal } from "../../../../hooks/useModal";
import { Controls } from "../../../controls/Controls";
import { SearchAndSortProjectToolbar } from "../../ProjectsPage/components/SearchAndSortProjectToolbar";

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

export function UserCreateChildModal(props) {
  const { projects, setAddedProjects } = props;
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

  setAddedProjects(checked);

  const handleAddProjectsToUser = () => {
    // setProjectsAddToUser([]);
    // setChecked([]);
    toggleModal();
  };

  return (
    <>
      <Controls.Button
        onClick={toggleModal}
        sx={{ width: "100%" }}
        className="ml-0 mb-2"
      >Настроить доступ</Controls.Button>
      <Modal
        open={openModal}
        onClose={toggleModal}
        aria-labelledby="child-modal-title"
        aria-describedby="child-modal-description"
      >
        <Box sx={{ ...style, width: 450 }}>
          <h3 >Доступ к проектам</h3>
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
                    width: "100%",
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
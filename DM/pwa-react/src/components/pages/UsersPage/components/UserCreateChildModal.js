import React, { useState, useCallback } from "react";
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

export function UserCreateChildModal({ projects, setAddedProjects }) {
  const [checked, setChecked] = useState(new Set());
  const [projectsAddToUser, setProjectsAddToUser] = useState([]);
  const [openModal, toggleModal] = useModal();

  const handleToggle = useCallback((project) => {
    setChecked(prevChecked => {
      const newChecked = new Set(prevChecked);
      if (newChecked.has(project.id)) {
        newChecked.delete(project.id);
        setProjectsAddToUser(prevProjects => prevProjects.filter(userProj => userProj.projectId !== project.id));
      } else {
        newChecked.add(project.id);
        setProjectsAddToUser(prevProjects => [...prevProjects, { projectId: project.id }]);
      }
      return newChecked;
    });
  }, []);

  const handleAddProjectsToUser = useCallback(() => {
    setAddedProjects(Array.from(checked));
    setProjectsAddToUser([]);
    setChecked(new Set());
    toggleModal();
  }, [checked, setAddedProjects, toggleModal]);

  const projectsAddToUserLength = projectsAddToUser.length;

  return (
    <>
      <Controls.Button
        onClick={toggleModal}
        sx={{ width: "100%" }}
        className="ml-0 mb-2"
        variant="outlined"
      >Настроить доступ</Controls.Button>
      <Modal
        open={openModal}
        onClose={toggleModal}
        aria-labelledby="child-modal-title"
        aria-describedby="child-modal-description"
      >
        <Box sx={{ ...style, width: 450 }}>
          <h3>Доступ к проектам</h3>
          <SearchAndSortProjectToolbar />
          <p>Выбрано: {projectsAddToUserLength <= 0 ? 0 : projectsAddToUserLength}</p>
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
                selected={checked.has(project.id)}
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
            variant="outlined"
            onClick={handleAddProjectsToUser}
          >Продолжить</Controls.Button>
          <Controls.Button
            variant="outlined"
            onClick={toggleModal}
          >Отменить</Controls.Button>
        </Box>
      </Modal>
    </>
  );
}
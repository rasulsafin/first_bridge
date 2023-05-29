import { useDispatch } from "react-redux";
import React, { useState } from "react";
import { Box, List, ListItemButton, Modal } from "@mui/material";
import { addUserListToProject } from "../../../../services/projectsSlice";
import { Controls } from "../../../controls/Controls";
import { SearchAndSortUserToolbar } from "../../UsersPage/components/SearchAndSortUserToolbar";
import { UserCard } from "../../UsersPage/components/UserCard";
import { useModal } from "../../../../hooks/useModal";

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

export function ProjectCreateChildModal(props) {
  const { users, setAddedUsers } = props;
  const dispatch = useDispatch();
  const [open, setOpen] = useState(false);
  const [checked, setChecked] = useState([]);
  const [usersAddToProject, setUsersAddToProject] = useState([]);
  const projectId = 1;
  const [openModal, toggleModal] = useModal();
  
  
  const handleClose = () => {
    setOpen(false);
    setUsersAddToProject([]);
    setChecked([]);
  };

  const handleToggle = (user) => {
    const currentIndex = checked.indexOf(user.id);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(user.id);
      setUsersAddToProject(usersProject => [...usersProject, { userId: user.id, projectId }]);
    } else {
      newChecked.splice(currentIndex, 1);
      setUsersAddToProject(usersAddToProject => usersAddToProject.filter(userProj => userProj.userId !== user.id));
    }

    setChecked(newChecked);
  };

  setAddedUsers(checked);
  
  const handleAddUsersToProject = () => {
    // dispatch(addUserListToProject(usersAddToProject));
    // setOpen(false);
    // setUsersAddToProject([]);
    // setChecked([]);
    
    toggleModal();
  };

  return (
    <>
      <Controls.Button
        onClick={toggleModal}
        className="m-0"
        sx={{ width: "100%" }}
      >Добавить</Controls.Button>
      <Modal
        hideBackdrop
        open={openModal}
        onClose={toggleModal}
        aria-labelledby="child-modal-title"
        aria-describedby="child-modal-description"
      >
        <Box sx={{ ...style, width: 500 }}>
          <h2 id="child-modal-title">Добавление участника</h2>
          <Box sx={{
            marginTop: "40px"
          }}>
            <SearchAndSortUserToolbar />
            <p>Выбрано: {usersAddToProject.length <= 0 ? 0 : usersAddToProject.length}</p>
            <List style={{ height: "300px", gap: "2px", overflowY: "auto", overflowX: "hidden" }}>
              {users.map(user =>
                <ListItemButton
                  key={user.id}
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
                  onClick={() => handleToggle(user)}
                  dense
                  selected={checked.indexOf(user.id) !== -1}
                >
                  <UserCard key={user.id} user={user} />
                </ ListItemButton>
              )}
            </List>
          </Box>
          <Controls.Button
            onClick={handleAddUsersToProject}
          >Добавить</Controls.Button>
          <Controls.Button
            onClick={toggleModal}
          >Отменить</Controls.Button>
        </Box>
      </Modal>
    </>
  );
}
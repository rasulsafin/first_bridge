import React, { useEffect, useRef, useState } from "react";
import {
  TextField,
  Grid,
  IconButton,
  List,
  Modal,
  Paper,
  Box,
  styled, InputLabel
} from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";
import { ReactComponent as ClipIcon } from "../../../../assets/icons/clip.svg";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { Controls } from "../../../controls/Controls";
import { UserCard } from "../../UsersPage/components/UserCard";
import { FileItem } from "../../../upload/FileItem";
import { uploadFileService } from "../../../../services/filesSlice";
import { fileExtensions } from "../../../../constants/fileExtensions";
import { deleteUserFromProject, updateProject } from "../../../../services/projectsSlice";
import { SearchAndSortUserToolbar } from "../../UsersPage/components/SearchAndSortUserToolbar";
import { fetchUsers, selectAllUsers } from "../../../../services/usersSlice";
import { ProjectUpdateChildModal } from "./ProjectUpdateChildModal";

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
  pt: 4,
  px: 3,
  pb: 3
};

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: "#F4F4F4",
  padding: theme.spacing(2),
  textAlign: "left",
  color: theme.palette.text.secondary,
  marginBottom: "10px",
  boxShadow: "none"
}));

export function ProjectUpdateModal(props) {
  const { project, onClose } = props;
  const [titleProject, setTitleProject] = useState(project.title);
  const [selectedFile, setSelectedFile] = useState(null);
  const dispatch = useDispatch();
  const projectId = project.id;
  const uploadInputRef = useRef(null);
  const users = useSelector(selectAllUsers);
  const projectUsers = users.filter(user => user.projects.every(proj => proj.id !== projectId));

  useEffect(() => {
    dispatch(fetchUsers());
  }, [project, dispatch]);

  const handleChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  if (selectedFile != null) {
    const formData = new FormData();
    formData.set("id", projectId);
    formData.append("file", selectedFile);
    dispatch(uploadFileService(formData));
    setSelectedFile(null);
  }

  const handleDeleteUserFromProject = (userId, projectId) => {
    dispatch(deleteUserFromProject({ userId, projectId }));
  };

  const data = {
    id: projectId,
    title: titleProject
  };

  const handleUpdateProject = () => {
    dispatch(updateProject(data));
    onClose();
  };

  const handleCloseModal = () => {
    setTitleProject(project.title);
    onClose();
  };

  return (
    <div>
      <Modal
        {...props}
        aria-labelledby="parent-modal-title"
        aria-describedby="parent-modal-description"
      >
        <Box sx={{ ...style, width: "70%", height: "90%" }}>
          <h2 style={{ marginBottom: "30px" }}>Редактирование проекта</h2>
          <Grid container spacing={5}>
            <Grid item xs={7}>
              <Box>
                <InputLabel>Название проекта</InputLabel>
                <TextField
                  value={titleProject}
                  onChange={(e) => setTitleProject(e.target.value)}
                  variant="outlined"
                  type="text"
                  fullWidth
                  size="small"
                />
                <Box sx={{
                  marginTop: "40px"
                }}>
                  <h3>Участники {project.users === null
                    ? 0
                    : project.users.length
                  }</h3>
                  <SearchAndSortUserToolbar />
                  <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
                    {project.users ?
                      project.users.map(user =>
                        <Grid alignItems="center" container>
                          <Grid item xs={10}>
                            <UserCard key={user.id} user={user} />
                          </Grid>
                          <Grid item xs={2}>
                            <IconButton
                              aria-label="delete"
                              onClick={() => handleDeleteUserFromProject(user.id, projectId)}
                            >
                              <CancelIcon />
                            </IconButton>
                          </Grid>
                        </Grid>
                      )
                      : null
                    }
                  </List>
                </Box>
                <Box>
                  <ProjectUpdateChildModal
                    users={projectUsers}
                    projectId={projectId}
                  />
                </Box>
              </Box>
            </Grid>
            <Grid item xs={5}>
              <Item>
                <span>Файлы</span>
                <List style={{ height: "150px", overflowY: "auto", overflowX: "hidden" }}>
                  {(project.items === undefined)
                    ? ""
                    : (project.items.map(file => <FileItem key={file.id} file={file} />))}
                </List>
                <>
                  <input
                    ref={uploadInputRef}
                    type="file"
                    accept={fileExtensions}
                    style={{ display: "none" }}
                    onChange={handleChange}
                  />
                  <Controls.Button
                    className="m-0 mt-1"
                    sx={{ width: "100%" }}
                    startIcon={<ClipIcon />}
                    onClick={() => uploadInputRef.current && uploadInputRef.current.click()}
                  >Выберите файл</Controls.Button>
                </>
              </Item>
              <Item>
                <span>План</span>
                <p>План 1</p>
                <p>План 2</p>
                <p>План 3</p>
                <Controls.Button
                  className="m-0"
                  sx={{ width: "100%" }}
                  startIcon={<ClipIcon />}
                  disabled
                >Выберите файл
                </Controls.Button>
              </Item>
            </Grid>
          </Grid>
          <Grid style={{ marginTop: "100px" }} container>
            <Grid item xs={10}>
              <Controls.Button
                variant="outlined"
                onClick={handleUpdateProject}
              >Сохранить
              </Controls.Button>
              <Controls.Button
                variant="outlined"
                onClick={handleCloseModal}
              >Отменить
              </Controls.Button>
            </Grid>
            <Grid item xs={2}>
              <Controls.Button startIcon={<TrashIcon />}>В архив</Controls.Button>
            </Grid>
          </Grid>
        </Box>
      </Modal>
    </div>
  );
}
import React, { useRef, useState } from "react";
import { 
  Grid,
  IconButton,
  List,
  ListItemButton,
  Modal,
  styled
} from "@mui/material";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import Paper from "@mui/material/Paper";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";
import { ReactComponent as ClipIcon } from "../../../../assets/icons/clip.svg";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { Controls } from "../../../controls/Controls";
import { UserCard } from "../../UsersPage/components/UserCard";
import { FileItem } from "../../../upload/FileItem";
import { fetchFiles, uploadFileService } from "../../../../services/filesSlice";
import { useDispatch } from "react-redux";
import { fileExtensions } from "../../../../constants/fileExtensions";
import { deleteUserFromProject } from "../../../../services/projectsSlice";
import { SearchAndSortUserToolbar } from "../../UsersPage/components/SearchAndSortUserToolbar";

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
  const [open, setOpen] = useState(false);
  const [checked, setChecked] = useState([]);
  const [usersAddToProject, setUsersAddToProject] = useState([]);

  const handleOpen = () => {
    setOpen(true);
  };
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
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);

    if (!usersAddToProject.some(userProj => userProj.id === user.id)) {
      setUsersAddToProject(usersAddToProject => [...usersAddToProject, user]);
    } else {
      setUsersAddToProject(usersAddToProject => usersAddToProject.filter(userProj => userProj.id !== user.id));
    }
  };

  console.log(checked);
  console.log(usersAddToProject);

  return (
    <>
      <Controls.Button
        onClick={handleOpen}
        className="m-0"
        sx={{ width: "100%" }}
      >Добавить</Controls.Button>
      <Modal
        hideBackdrop
        open={open}
        onClose={handleClose}
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
              {props.users.map(user =>
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
                    },
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
          >Добавить</Controls.Button>
          <Controls.Button
            onClick={handleClose}
          >Отменить</Controls.Button>
        </Box>
      </Modal>
    </>
  );
}

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: "#F4F4F4",
  padding: theme.spacing(2),
  textAlign: "left",
  color: theme.palette.text.secondary,
  marginBottom: "10px"
}));

export function ProjectModal(props) {
  const [value, setValue] = useState(props.project.project.title);
  const [selectedFile, setSelectedFile] = useState(null);
  const dispatch = useDispatch();
  const project = props.project.project.id;
  const uploadInputRef = useRef(null);

  const handleChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  if (selectedFile != null) {
    const formData = new FormData();
    formData.set("id", project);
    formData.append("file", selectedFile);
    dispatch(uploadFileService(formData));
    setSelectedFile(null);
  }

  useDispatch(() => {
    dispatch(fetchFiles(project));
  }, []);

  const handleDeleteUserFromProject = (userId, projectId) => {
    dispatch(deleteUserFromProject({ userId, projectId }));
  };

  return (
    <div>
      <Modal
        {...props}
        aria-labelledby="parent-modal-title"
        aria-describedby="parent-modal-description"
      >
        <Box sx={{ ...style, width: "70%", height: "90%" }}>
          <h2 id="parent-modal-title" style={{ marginBottom: "30px" }}>Редактирование проекта</h2>
          <Grid container spacing={5}>
            <Grid item xs={7}>
              <Box>
               <span style={{ color: "#B3B3B3" }}>
                 Название проекта 
               </span>
                <TextField
                  value={value}
                  onChange={(e) => setValue(e.target.value)}
                  variant="outlined"
                  type="text"
                  fullWidth
                  size="small"
                />
                <Box sx={{
                  marginTop: "40px"
                }}>
                  <h3>Участники {props.project.project.users === null
                    ? 0
                    : props.project.project.users.length
                  }</h3>
                  <SearchAndSortUserToolbar />
                  <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
                    {props.project.project.users ?
                      props.project.project.users.map(user =>
                        <>
                          <Grid alignItems="center" container>
                            <Grid item xs={10}>
                              <UserCard key={user.id} user={user} />
                            </Grid>
                            <Grid item xs={2}>
                              <IconButton
                                aria-label="delete"
                                onClick={() => handleDeleteUserFromProject(user.id, project)}
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
                  <ChildModal
                    users={props.users}
                  />
                </Box>
              </Box>
            </Grid>
            <Grid item xs={5}>
              <Item>
                <span>Файлы</span>
                <List style={{ height: "150px", overflowY: "auto", overflowX: "hidden" }}>
                  {(props.files === undefined)
                    ? ""
                    : (props.files.map(file => <FileItem key={file.id} file={file} />))}
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
                >Выберите файл
                </Controls.Button>
              </Item>
            </Grid>
          </Grid>
          <Grid style={{ marginTop: "100px" }} container>
            <Grid item xs={10}>
              <Controls.Button
              >Сохранить</Controls.Button>
              <Controls.Button>Отменить</Controls.Button>
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
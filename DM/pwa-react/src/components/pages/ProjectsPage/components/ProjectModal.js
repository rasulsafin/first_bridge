import React, { useRef, useState } from "react";
import { Button, Grid, List, Modal, styled } from "@mui/material";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import { SearchBar } from "../../../searchBar/SearchBar";
import Paper from "@mui/material/Paper";
import { ReactComponent as TrashIcon } from "../../../../assets/icons/trashcan.svg";
import { ReactComponent as ClipIcon } from "../../../../assets/icons/clip.svg";
import { Controls } from "../../../controls/Controls";
import { UserCard } from "../../UsersPage/components/UserCard";
import { FileItem } from "../../../upload/FileItem";
import { fetchFiles, uploadFileService } from "../../../../services/filesSlice";
import { useDispatch } from "react-redux";
import { fileExtensions } from "../../../../constants/fileExtensions";
import { searchUsersByName, sortUsersByNameAsc, sortUsersByNameDesc } from "../../../../services/usersSlice";

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
  const dispatch = useDispatch();
  const [open, setOpen] = React.useState(false);

  const handleOpen = () => {
    setOpen(true);
  };
  const handleClose = () => {
    setOpen(false);
  };

  function filterByInput(e) {
    dispatch(searchUsersByName(e.target.value));
  }

  const handleSortByAsc = () => {
    dispatch(sortUsersByNameAsc());
  };

  const handleSortByDesc = () => {
    dispatch(sortUsersByNameDesc());
  };

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
            <SearchBar
              onChange={e => filterByInput(e)}
            />
            <Controls.Button
              className="ml-0"
              style={{
                backgroundColor: "#2D2926",
                color: "#FFF",
                border: "none"
              }}
              onClick={handleSortByAsc}
            >От А до Я</Controls.Button>
            <Controls.Button
              style={{
                backgroundColor: "#b4b3b2",
                color: "#2D2926",
                border: "none"
              }}
              onClick={handleSortByDesc}
            >От Я до А</Controls.Button>
            <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
              {props.users.map(user => <UserCard key={user.id} user={user} />)}
            </List>
          </Box>
          <Button onClick={handleClose}>Close Child Modal</Button>
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
                  <SearchBar />
                  <Controls.Button
                    className="ml-0"
                    style={{
                      backgroundColor: "#2D2926",
                      color: "#FFF",
                      border: "none"
                    }}
                  >От А до Я</Controls.Button>
                  <Controls.Button
                    style={{
                      backgroundColor: "#b4b3b2",
                      color: "#2D2926",
                      border: "none"
                    }}
                  >От Я до А</Controls.Button>
                  <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
                    {props.project.project.users ?
                      props.project.project.users.map(user => <UserCard key={user.id} user={user} />)
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
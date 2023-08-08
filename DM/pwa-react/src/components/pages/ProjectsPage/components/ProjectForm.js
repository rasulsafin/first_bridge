import React, { useEffect, useRef, useState } from "react";
import { Field, useFormikContext } from "formik";
import {
  Box,
  Grid,
  IconButton,
  InputLabel,
  List,
  Paper,
  styled,
  Typography
} from "@mui/material";
import { Controls } from "../../../controls/Controls";
import { SearchAndSortUserToolbar } from "../../UsersPage/components/SearchAndSortUserToolbar";
import { UserCard } from "../../UsersPage/components/UserCard";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { FileItem } from "../../../upload/FileItem";
import { fileExtensions } from "../../../../constants/fileExtensions";
import { ReactComponent as ClipIcon } from "../../../../assets/icons/clip.svg";
import { ProjectCreateChildModal } from "./ProjectCreateChildModal";

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: "#F4F4F4",
  padding: theme.spacing(2),
  textAlign: "left",
  color: theme.palette.text.secondary,
  marginBottom: "10px",
  boxShadow: "none"
}));

export const ProjectForm = ({ users }) => {
  const { setFieldValue } = useFormikContext();
  const uploadInputRef = useRef(null);
  const [addedUsers, setAddedUsers] = useState([]);
  console.log(addedUsers);

  const filterUsers = users.filter(user => addedUsers.some(id => id === user.id));

  const items = [];

  useEffect(() => {
    setFieldValue("userIds", addedUsers);
  }, [addedUsers]);

  return (
    <div>
      <Grid container spacing={2}>
        <Grid item xs={7} md={7} lg={7}>
          <InputLabel>Название проекта</InputLabel>
          <Field name="title" as={Controls.ValidationFormTextfield} />
          <Box sx={{
            marginTop: "40px"
          }}>
            <Typography variant="h5">Участники {filterUsers === null
              ? 0
              : filterUsers.length
            }</Typography>
            <SearchAndSortUserToolbar />
            <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
              {filterUsers ?
                filterUsers.map(user =>
                  <Grid alignItems="center" container>
                    <Grid item xs={10}>
                      <UserCard key={user.id} user={user} />
                    </Grid>
                    <Grid item xs={2}>
                      <IconButton
                        aria-label="delete"
                        // onClick={() => addedUsers(user.id, projectId)}
                      >
                        <CancelIcon />
                      </IconButton>
                    </Grid>
                  </Grid>
                )
                : null
              }
            </List>
            <ProjectCreateChildModal
              users={users}
              setAddedUsers={setAddedUsers}
            />
          </Box>
        </Grid>
        <Grid item xs={5}>
          <Item>
            <span>Файлы</span>
            <List style={{ height: "150px", overflowY: "auto", overflowX: "hidden" }}>
              {(items === undefined)
                ? ""
                : (items.map(file => <FileItem key={file.index} file={file} />))}
            </List>
            <>
              <input
                ref={uploadInputRef}
                type="file"
                accept={fileExtensions}
                style={{ display: "none" }}
                // onChange={handleChange}
              />
              <Controls.Button
                className="m-0 mt-1"
                sx={{ width: "100%" }}
                startIcon={<ClipIcon />}
                variant="outlined"
                disabled
                onClick={() => uploadInputRef.current && uploadInputRef.current.click()}
              >Выберите файл</Controls.Button>
            </>
          </Item>
          <Item>
            <span>План</span>
            <Controls.Button
              className="m-0"
              sx={{ width: "100%" }}
              startIcon={<ClipIcon />}
              variant="outlined"
              disabled
            >Выберите файл
            </Controls.Button>
          </Item>
        </Grid>
      </Grid>
    </div>
  );
};

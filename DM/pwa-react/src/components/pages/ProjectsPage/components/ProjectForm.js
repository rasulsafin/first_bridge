import { Field, Form, Formik } from "formik";
import { Controls } from "../../../controls/Controls";
import { projectValidationSchema } from "../utils/validationSchema";
import { Box, Button, Grid, IconButton, InputLabel, List, Paper, styled } from "@mui/material";
import { project } from "../../../../locale/ru/project";
import React, { useRef } from "react";
import { SearchAndSortUserToolbar } from "../../UsersPage/components/SearchAndSortUserToolbar";
import { UserCard } from "../../UsersPage/components/UserCard";
import { ReactComponent as CancelIcon } from "../../../../assets/icons/cancel.svg";
import { FileItem } from "../../../upload/FileItem";
import { fileExtensions } from "../../../../constants/fileExtensions";
import { ReactComponent as ClipIcon } from "../../../../assets/icons/clip.svg";
import { ProjectUpdateChildModal } from "./ProjectUpdateChildModal";
import { ProjectCreateChildModal } from "./ProjectCreateChildModal";

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: "#F4F4F4",
  padding: theme.spacing(2),
  textAlign: "left",
  color: theme.palette.text.secondary,
  marginBottom: "10px"
}));

export const ProjectForm = (props) => {
  const { onSubmit } = props;
  const uploadInputRef = useRef(null);

  const projectUsers = [];
  const items = [];

  return (
    <div>
      <Grid container spacing={2}>
        <Grid item xs={7} md={7} lg={7}>
          <InputLabel>{project.title}</InputLabel>
          <Field name="title" as={Controls.ValidationFormTextfield} />
          <Box sx={{
            marginTop: "40px"
          }}>
            <h3>Участники {projectUsers === null
              ? 0
              : projectUsers.length
            }</h3>
            <SearchAndSortUserToolbar />
            <List style={{ height: "300px", overflowY: "auto", overflowX: "hidden" }}>
              {projectUsers ?
                projectUsers.map(user =>
                  <Grid alignItems="center" container>
                    <Grid item xs={10}>
                      <UserCard key={user.id} user={user} />
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
                )
                : null
              }
            </List>
            <ProjectCreateChildModal
              users={projectUsers}
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

    </div>
  );
};

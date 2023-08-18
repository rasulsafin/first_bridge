import React, { useRef } from "react";
import { Grid, InputLabel, List, Paper, styled } from "@mui/material";
import { record } from "../../../../locale/ru/record";
import { Field } from "formik";
import { Controls } from "../../../controls/Controls";
import { FileItem } from "../../../upload/FileItem";
import { fileExtensions } from "../../../../constants/fileExtensions";
import { ReactComponent as ClipIcon } from "../../../../assets/icons/clip.svg";

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: "#F4F4F4",
  padding: theme.spacing(2),
  textAlign: "left",
  color: theme.palette.text.secondary,
  marginBottom: "10px",
  boxShadow: "none"
}));

export const RecordForm = () => {
  const uploadInputRef = useRef(null);
  
  return (
    <>
      <Grid container spacing={4}>
        <Grid item container xs={6}>
          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.title}</InputLabel>
            <Field name="title" as={Controls.ValidationFormTextfield} />
          </Grid>
          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.id}</InputLabel>
            <Field name="id" as={Controls.ValidationFormTextfield} />
          </Grid>
          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.status}</InputLabel>
            <Field name="status" as={Controls.ValidationFormTextfield} />
          </Grid>
          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.priority}</InputLabel>
            <Field name="priority" as={Controls.ValidationFormTextfield} />
          </Grid>

          {/*<Grid item xs={12} md={12} lg={12}>*/}
          {/*  <InputLabel>{record.role}</InputLabel>*/}
          {/*  <Field name="roleId" as={Controls.Select}>*/}
          {/*    {roles.map(role => (*/}
          {/*      <MenuItem*/}
          {/*        key={role.id}*/}
          {/*        value={role.id}*/}
          {/*      >{role.name}*/}
          {/*      </MenuItem>)*/}
          {/*    )}*/}
          {/*  </Field>*/}
          {/*</Grid>*/}

          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.executor}</InputLabel>
            <Field name="executor" as={Controls.ValidationFormTextfield} />
          </Grid>
          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.createdAt}</InputLabel>
            <Field name="createdAt" as={Controls.ValidationFormTextfield} />
          </Grid>
          <Grid item xs={12} md={12} lg={12}>
            <InputLabel>{record.createdBy}</InputLabel>
            <Field name="createdBy" as={Controls.ValidationFormTextfield} />
          </Grid>
        </Grid>
        <Grid item container xs={6}>
          <Grid item xs={10}>
            <Item>
              <span>Файлы</span>
              <List style={{ height: "150px", overflowY: "auto", overflowX: "hidden" }}>
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
          </Grid>
          <Grid item xs={10}><Item>
            <span>Документы</span>
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
      </Grid>
    </>
  );
};

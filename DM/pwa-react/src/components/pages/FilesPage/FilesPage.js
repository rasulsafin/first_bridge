import { Toolbar } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as React from "react";
import { useNavigate } from "react-router";
import { fetchFiles, selectAllFiles } from "../../../services/filesSlice";
import { UploadFile } from "../../upload/UploadFile";
import { FileItem } from "../../upload/FileItem";
import { Controls } from "../../controls/Controls";

export const FilesPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const files = useSelector(selectAllFiles);

  const goBack = () => {
    navigate(-1);
  };

  useEffect(() => {
    dispatch(fetchFiles());
  }, [dispatch]);

  return (
    <div className="component-container">
      <Toolbar>
        <Controls.Button onClick={goBack} size="small" variant="outlined">
        </Controls.Button>
      </Toolbar>
      <hr />
      <h3>Files</h3>
      <UploadFile />
      <div 
        className="col-8"
      style={{
        display: "flex",
        flexWrap: "wrap"
      }}
      >
        {files.map(file => <FileItem file={file} />)}
      </div>
    </div>
  );
};
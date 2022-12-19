import { Toolbar } from "@mui/material";
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as React from "react";
import { BiArrowBack } from "react-icons/bi";
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
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Controls.Button>
      </Toolbar>
      <hr />
      <h3>Files</h3>
      <UploadFile />
      {files.map(file => <FileItem file={file} />)}
    </div>
  );
};
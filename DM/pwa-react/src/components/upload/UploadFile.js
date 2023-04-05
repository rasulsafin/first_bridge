import { useState, useRef } from "react";
import "./UploadFile.css";
import { useDispatch } from "react-redux";
import { uploadFileService } from "../../services/filesSlice";
import { fileExtensions } from "../../constants/fileExtensions";
import { Controls } from "../controls/Controls";
import { useParams } from "react-router";
import { Button, IconButton, styled } from "@mui/material";
import Stack from "@mui/material/Stack";

export const UploadFile = () => {
  const dispatch = useDispatch();
  const filePicker = useRef(null);
  const [selectedFile, setSelectedFile] = useState(null);
  const { id } = useParams();
  const project = id;
  console.log(project)
  const handleChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  const handleUpload = async () => {
    if (!selectedFile) {
      alert("Please select a file");
      return;
    }
    const formData = new FormData();
    formData.set('id', project);
    formData.append("file", selectedFile);
    dispatch(uploadFileService(formData));
  };

  const handleClick = () => {
    filePicker.current.click();
  };

  const Input = styled('input')({
    display: 'none',
  });

  const uploadInputRef = useRef(null);
  
  const [file, setFile] = useState(null);
  
  const onChange = (event) => {
    setFile(event.target.files[0]);
    // console.log(uploadInputRef.current)
  }
  
  
  console.log(file)
  return (
    <>
      <>
        <input
          ref={uploadInputRef}
          type="file"
          accept="image/*"
          style={{ display: "none" }}
          onChange={onChange}
        />
        <Button
          onClick={() => uploadInputRef.current && uploadInputRef.current.click()}
          variant="contained"
        >
          Upload
        </Button>
      </>
      
      <div>
        <Stack direction="row" alignItems="center" spacing={2}>
          <label htmlFor="contained-button-file">
            <Input accept="image/*" id="contained-button-file" multiple type="file" />
            <Button variant="contained" component="span">
              Upload
            </Button>
          </label>
          <label htmlFor="icon-button-file">
            <Input accept="image/*" id="icon-button-file" type="file" />
            <IconButton color="primary" aria-label="upload picture" component="span">
              q
            </IconButton>
          </label>
        </Stack>
      </div>
      <Controls.Button
        onClick={handleClick}
      > Pick File
      </Controls.Button>
      <input
        className="hidden"
        type="file"
        ref={filePicker}
        onChange={handleChange}
        accept={fileExtensions}
      />
      <Controls.Button
        onClick={handleUpload}
      > Upload
      </Controls.Button>
      {selectedFile && (
        <ul>
          <li> Name: {selectedFile.name}</li>
          <li> Size: {selectedFile.size}</li>
        </ul>
      )}
    </>
  );
};
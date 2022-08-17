import { useState, useRef, useEffect } from "react";
import "./UploadFile.css";
import { useDispatch } from "react-redux";
import { uploadFileService } from "../../services/filesSlice";

export const UploadFile = () => {
  const dispatch = useDispatch();
  const filePicker = useRef(null);
  const [selectedFile, setSelectedFile] = useState(null);

  const handleChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  const handleUpload = async () => {
    if (!selectedFile) {
      alert("Please select a file");
      return;
    }
    const formData = new FormData();
    formData.append("file", selectedFile);
    dispatch(uploadFileService(formData));
  };

  const handleClick = () => {
    filePicker.current.click();
  };

  return (
    <>
      <button
        onClick={handleClick}
      > Pick File
      </button>
      <input
        className="hidden"
        type="file"
        ref={filePicker}
        onChange={handleChange}
        accept=".png, .jpg, .ifc, .bim"
      />
      <button
        onClick={handleUpload}
      > Upload
      </button>
      {selectedFile && (
        <ul>
          <li> Name: {selectedFile.name}</li>
          <li> Size: {selectedFile.size}</li>
        </ul>
      )}
    </>
  );
};
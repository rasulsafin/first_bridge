import { useDispatch } from "react-redux";
import { getFile } from "../../services/filesSlice";

export const FileItem = (file) => {
  const dispatch = useDispatch();

  const handleDownload = () => {
    dispatch(getFile(file.file.name));
  };

  return (
    <div
      style={{
        margin: "5px",
        width: "10vw",
        padding: "5px",
        wordWrap: "break-word"
      }}>
      <h6>{file.file.name}</h6>
      <button
        onClick={handleDownload}
      >Download
      </button>
    </div>
  );
};
import { useDispatch } from "react-redux";
import { getFile } from "../../services/filesSlice";
import { Link } from "react-router-dom";

export const FileItem = (file) => {
  const dispatch = useDispatch();

  const handleDownload = async () => {
    dispatch(getFile(file.file.name));
  };

  const path = file.file.relativePath.toString();
  console.log(path);
  return (
    <div
      style={{
        margin: "5px",
        width: "30vw",
        padding: "5px",
        border: "2px blue solid",
        wordWrap: "break-word"
      }}>
      <h6> {file.file.name}</h6>
      <button
        onClick={handleDownload}
      >Download
      </button>
    </div>
  );
};
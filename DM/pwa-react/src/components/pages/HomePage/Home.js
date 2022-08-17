import { PdfViewer } from "../../pdfViewer/PdfViewer";
import { PdfCreate } from "../../pdfCreate/PdfCreate";
import { UploadFile } from "../../upload/UploadFile";
import { useDispatch, useSelector } from "react-redux";
import { fetchFiles, selectAllFiles } from "../../../services/filesSlice";
import { useEffect } from "react";
import { fetchUsers } from "../../../services/usersSlice";
import { FileItem } from "../../upload/FileItem";

export const Home = () => {
  const dispatch = useDispatch();
  const files = useSelector(selectAllFiles);
  console.log(files)

  useEffect(() => {
    dispatch(fetchFiles());
  }, [dispatch]);
  
  return (
    <div className="p-4">
     <h1> This is Home page. </h1> 
      <h3> Dashboard </h3>
      {/*<PdfViewer />*/}
      {/*<PdfCreate />*/}
      <UploadFile />
      {files.map(file => <FileItem key={file.id} file={file} />)}
    </div>
  );
};
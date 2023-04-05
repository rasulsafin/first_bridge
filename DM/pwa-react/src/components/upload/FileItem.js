import { useDispatch } from "react-redux";
import { getFile } from "../../services/filesSlice";
import { ReactComponent as JPGIcon } from "../../assets/icons/JPG.svg";
import { ReactComponent as TrashIcon } from "../../assets/icons/trashcan.svg";
import { Grid } from "@mui/material";

export const FileItem = (file) => {
  const dispatch = useDispatch();

  const handleDownload = () => {
    dispatch(getFile(file.file.name));
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-between",
        alignContent: "center",
        alignItems: "center",
        margin: "5px",
        width: "350px",
        padding: "5px",
      }}>
      
      <Grid container>
        <Grid direction="column" item xs={11} md={11}>
          <JPGIcon />
          <span style={{ 
            fontSize: "12px", 
            fontWeight: "bold", 
            marginLeft: "5px"
          }}>{file.file.name}</span>
        </Grid>
        <Grid item xs={1} md={1}><TrashIcon /></Grid>
      </Grid>
      {/*<div*/}
      {/*style={{*/}
      {/*  display: "flex",*/}
      {/*  flexDirection: "row",*/}
      {/*  justifyContent: "space-evenly",*/}
      {/*  alignContent: "center",*/}
      {/*  alignItems: "center",*/}
      {/*}}*/}
      {/*><JPGIcon />*/}
      {/*  <h6>{file.file.name}</h6></div>*/}
      {/*<div><TrashIcon /></div>*/}
      
      
      {/*<button*/}
      {/*  onClick={handleDownload}*/}
      {/*>Download*/}
      {/*</button>*/}
    </div>
  );
};
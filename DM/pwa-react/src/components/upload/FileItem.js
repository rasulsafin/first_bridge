import { useDispatch } from "react-redux";
import { getFile } from "../../services/filesSlice";
import { ReactComponent as JPGIcon } from "../../assets/icons/JPG.svg";
import { ReactComponent as TrashIcon } from "../../assets/icons/trashcan.svg";
import { Grid, IconButton } from "@mui/material";

export const FileItem = (file) => {
  const dispatch = useDispatch();

  const handleDownload = () => {
    dispatch(getFile(file.file.name));
  };
  
  const handleDelete = () => {
    //TODO delete item
    console.log("delete item")
  }

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "row",
        justifyContent: "space-between",
        alignContent: "center",
        alignItems: "center",
        margin: "5px",
        width: "100%",
        padding: "5px",
        paddingRight: "15px",
      }}>
      <Grid container>
        <Grid item xs={11} md={11}>
          <JPGIcon />
          <span style={{ 
            fontSize: "12px", 
            fontWeight: "bold", 
            marginLeft: "5px"
          }}>{file.file.name}</span>
        </Grid>
        <Grid item xs={1} md={1}>
          <IconButton 
            aria-label="delete"
            onClick={handleDelete}
          >
            <TrashIcon />
          </IconButton>
        </Grid>
      </Grid>
    </div>
  );
};
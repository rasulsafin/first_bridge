import { Button, ListItem, ListItemIcon, ListItemText, Toolbar } from "@mui/material";
import { createRef, useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import * as React from "react";
import { BiArrowBack } from "react-icons/bi";
import { useNavigate } from "react-router";
import { fetchFiles, selectAllFiles } from "../../../services/filesSlice";
import { UploadFile } from "../../upload/UploadFile";
import { FileItem } from "../../upload/FileItem";
import { Controls } from "../../controls/Controls";
import { ViewerXbim } from "./ViewerXbim";
import { ViewerIFC } from "./ViewerIFC";
import { IfcViewerAPI } from "web-ifc-viewer";
import { IfcContainer } from "./IfcContainer";
import { CompareArrowsSharp, FolderOpenOutlined } from "@mui/icons-material";
import Box from "@mui/material/Box";
import { List } from "reactstrap";


export const FilesPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const files = useSelector(selectAllFiles);
  const ifcContainer = createRef();
  const [viewer, setViewer] = useState();
  
  useEffect(() => {
    if (ifcContainer.current) {
      const container = ifcContainer.current;
      const ifcViewer = new IfcViewerAPI({ container });
      ifcViewer.addAxes();
      ifcViewer.addGrid();
      ifcViewer.IFC.loader.ifcManager.applyWebIfcConfig({
        COORDINATE_TO_ORIGIN: true,
        USE_FAST_BOOLS: false
      });
      setViewer(ifcViewer);
    }
  }, []);
  
  const goBack = () => {
    navigate(-1);
  };

  useEffect(() => {
    dispatch(fetchFiles());
  }, [dispatch]);


  const ifcOnLoad = async (e) => {
    const file = e && e.target && e.target.files && e.target.files[0];
    if (file && viewer) {

      // load file
      const model = await viewer.IFC.loadIfc(file, true);
      await viewer.shadowDropper.renderShadow(model.modelID);
    }
  };
  
  console.log(files);
  
  return (
    <div className="p-3">
      <Toolbar>
        <Controls.Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Controls.Button>
      </Toolbar>
      <hr />
      <h3>Files</h3>
      <UploadFile />
      {/*{files.map(file => <FileItem file={file} />)}*/}
      {/*<hr />*/}
      {/*<ViewerXbim />*/}
      <hr />
      {/*<ViewerIFC />*/}
<Box>
  <List>
    <input
      type='file'
      id='file'
      accept='.ifc'
      onChange={ifcOnLoad}
      style={{ display: 'none' }}
    />
    <label htmlFor='file'>
      <ListItem button key={'openFile'}>
        <ListItemIcon>
          <FolderOpenOutlined />
        </ListItemIcon>
        <ListItemText primary={'Open File'} />
      </ListItem>
    </label>
  </List>
  <IfcContainer
    ref={ifcContainer}
    viewer={viewer} />
</Box>
          
    </div>
  );
};
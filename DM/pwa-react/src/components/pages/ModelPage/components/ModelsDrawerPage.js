import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { List } from "@mui/material";
import { SearchBar } from "../../../searchBar/SearchBar";
import { ModelCard } from "./ModelCard";
import {
  fetchFiles,
  searchFilesByName,
  selectAllFiles
} from "../../../../services/filesSlice";
import { Controls } from "../../../controls/Controls";
import "./ModelsDrawerPage.css";

export const ModelsDrawerPage = () => {
  const dispatch = useDispatch();
  const files = useSelector(selectAllFiles);
  const darkModeSearchBar = true;
  const [checked, setChecked] = useState([]);
  const [fileToScene, setFileToScene] = useState([]);

  useEffect(() => {
    dispatch(fetchFiles(1));
  }, []);

  function filterByInput(e) {
    dispatch(searchFilesByName(e.target.value));
  }

  const handleToggle = (file) => {
    const currentIndex = checked.indexOf(file.id);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(file.id);
      setFileToScene(fileToScene => [...fileToScene, { id: file.id }]);
    } else {
      newChecked.splice(currentIndex, 1);
      setFileToScene(fileToScene => fileToScene.filter(fileScene => fileScene.id !== file.id));
    }

    setChecked(newChecked);
  };

  return (
    <div>
      <div className="header-model-toolbar">
        <div className="header-model-title">
          Модели
        </div>
        <SearchBar
          darkMode={darkModeSearchBar}
          onChange={e => filterByInput(e)}
        />
      </div>
      <List
        sx={{
          display: "flex",
          alignItems: "flex-end",
          // justifyContent: "start",
          flexDirection: "row",
          flexWrap: "wrap",
          gap: "16px"
        }}>
        {files.map(file =>
          <ModelCard
            file={file}
            handleToggle={handleToggle}
            checked={checked}
          />)}
      </List>
      <div className="footer-model-toolbar">
        <Controls.Button
          className="mx-0 my-3 p-2"
          color="primary"
          variant="contained"
          sx={{
            width: "374px"
          }}
        >Загрузить</Controls.Button>
      </div>
    </div>
  );
};

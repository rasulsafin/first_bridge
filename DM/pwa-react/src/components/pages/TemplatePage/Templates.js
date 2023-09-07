import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { List } from "@mui/material";
import {
  fetchRecordTemplates,
  searchRecordTemplatesByName,
  selectAllRecordTemplates,
  sortRecordTemplatesByDateAsc,
  sortRecordTemplatesByDateDesc,
  sortRecordTemplatesByNameAsc,
  sortRecordTemplatesByNameDesc
} from "../../../services/recordTemplatesSlice";
import { Controls } from "../../controls/Controls";
import { SearchBar } from "../../searchBar/SearchBar";
import { TemplateCard } from "./components/TemplateCard";
import { useModal } from "../../../hooks/useModal";
import "../../layout/Layout.css";
import { TemplateCreateModal } from "./components/TemplateCreateModal";
import { selectCurrentProject } from "../../../services/projectsSlice";

export const Templates = () => {
  const dispatch = useDispatch();
  const recordTemplates = useSelector(selectAllRecordTemplates);
  const [openModal, toggleModal] = useModal();
  const [checked, setChecked] = useState([]);
  const [activeButton, setActiveButton] = useState("");
  const currentProject = useSelector(selectCurrentProject);
  const isCurrentProjectNull = currentProject === null;

  const handleToggle = (templateId) => () => {
    const currentIndex = checked.indexOf(templateId);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(templateId);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

  useEffect(() => {
    dispatch(fetchRecordTemplates(currentProject));
  }, []);

  function filterByInput(e) {
    dispatch(searchRecordTemplatesByName(e.target.value));
  }

  const handleSortByDateAsc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordTemplatesByDateAsc());
  };

  const handleSortByDateDesc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordTemplatesByDateDesc());
  };

  const handleSortByNameAsc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordTemplatesByNameAsc());
  };

  const handleSortByNameDesc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordTemplatesByNameDesc());
  };

  return (
    <div className="component-container">
      <div className="header-toolbar">
        <div className="header-title">Шаблоны</div>
        <SearchBar
          onChange={e => filterByInput(e)}
          filter="true"
        />
        <div>
          <Controls.Button
            id="1"
            className="ml-0"
            style={{
              backgroundColor: activeButton === "1" ? "#2D2926" : "#FFF",
              color: activeButton === "1" ? "#FFF" : "#2D2926"
            }}
            onClick={handleSortByDateAsc}
          >Новые</Controls.Button>
          <Controls.Button
            id="2"
            style={{
              backgroundColor: activeButton === "2" ? "#2D2926" : "#FFF",
              color: activeButton === "2" ? "#FFF" : "#2D2926"
            }}
            onClick={handleSortByDateDesc}
          >Старые</Controls.Button>
          <Controls.Button
            id="3"
            style={{
              backgroundColor: activeButton === "3" ? "#2D2926" : "#FFF",
              color: activeButton === "3" ? "#FFF" : "#2D2926"
            }}
            onClick={handleSortByNameAsc}
          >От А до Я</Controls.Button>
          <Controls.Button
            id="4"
            style={{
              backgroundColor: activeButton === "4" ? "#2D2926" : "#FFF",
              color: activeButton === "4" ? "#FFF" : "#2D2926"
            }}
            onClick={handleSortByNameDesc}
          >От Я до А</Controls.Button>
        </div>
      </div>
      {isCurrentProjectNull
        ?
        (<div><h3>Для отображения шаблонов необходимо выбрать проект</h3></div>)
        :
        <List
          sx={{
            display: "flex",
            flexDirection: "row",
            flexWrap: "wrap"
          }}>
          {recordTemplates.map(template =>
            <TemplateCard
              key={template.id}
              template={template}
              handleToggle={handleToggle}
              checked={checked}
            />)}
        </List>
      }
      {openModal &&
        <TemplateCreateModal
          toggleModal={toggleModal}
        />
      }
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
};

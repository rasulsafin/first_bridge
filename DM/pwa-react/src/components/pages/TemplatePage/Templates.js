import React, { useEffect, useState } from "react";
import { SearchBar } from "../../searchBar/SearchBar";
import { Controls } from "../../controls/Controls";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchRecordTemplates,
  searchRecordTemplatesByName,
  selectAllRecordTemplates,
  sortRecordTemplatesByDateAsc,
  sortRecordTemplatesByDateDesc,
  sortRecordTemplatesByNameAsc,
  sortRecordTemplatesByNameDesc
} from "../../../services/recordTemplatesSlice";
import { List } from "@mui/material";
import { TemplateCard } from "./components/TemplateCard";

export const Templates = () => {
  const dispatch = useDispatch();
  const recordTemplates = useSelector(selectAllRecordTemplates);
  const [openModal, setModal] = useState(false);

  useEffect(() => {
    dispatch(fetchRecordTemplates(1));
  }, []);

  function filterByInput(e) {
    dispatch(searchRecordTemplatesByName(e.target.value));
  }

  const handleSortByDateAsc = () => {
    dispatch(sortRecordTemplatesByDateAsc());
  };

  const handleSortByDateDesc = () => {
    dispatch(sortRecordTemplatesByDateDesc());
  };

  const handleSortByNameAsc = () => {
    dispatch(sortRecordTemplatesByNameAsc());
  };

  const handleSortByNameDesc = () => {
    dispatch(sortRecordTemplatesByNameDesc());
  };

  const handleModalOpen = () => {
    setModal(true);
  };

  const handleModalClose = () => {
    setModal(false);
  };

  return (
    <div>
      <h3 className="mb-2">Шаблоны</h3>
      <div className="toolbar-project">
        <SearchBar
          onChange={e => filterByInput(e)}
        />
        <div>
          <Controls.Button
            className="ml-0"
            style={{
              backgroundColor: "#2D2926",
              color: "#FFF",
              border: "none"
            }}
            onClick={handleSortByDateAsc}
          >Новые</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
            onClick={handleSortByDateDesc}
          >Старые</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
            onClick={handleSortByNameAsc}
          >От А до Я</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
            onClick={handleSortByNameDesc}
          >От Я до А</Controls.Button>
        </div>
      </div>
      <div>
      </div>
      <List
        sx={{
          display: "flex",
          flexDirection: "row",
          flexWrap: "wrap"
        }}>
        {recordTemplates.map(template => <TemplateCard key={template.id} template={template} />)}
      </List>
      <Controls.RoundButton
        onClick={handleModalOpen}
      >
      </Controls.RoundButton>
    </div>
  );
};

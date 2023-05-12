import React, { useEffect } from "react";
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

export const Templates = () => {
  const dispatch = useDispatch();
  const recordTemplates = useSelector(selectAllRecordTemplates);
  const [openModal, toggleModal] = useModal();

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
      <List
        sx={{
          display: "flex",
          flexDirection: "row",
          flexWrap: "wrap"
        }}>
        {recordTemplates.map(template => <TemplateCard key={template.id} template={template} />)}
      </List>
      <Controls.Modal
        open={openModal}
        onClose={toggleModal}
      >
    </Controls.Modal>
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
};

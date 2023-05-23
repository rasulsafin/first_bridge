import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { List } from "@mui/material";
import {
  fetchDocuments,
  searchDocumentsByName,
  selectAllDocuments,
  sortDocumentsByDateAsc,
  sortDocumentsByDateDesc,
  sortDocumentsByNameAsc,
  sortDocumentsByNameDesc
} from "../../../services/documentsSlice";
import { SearchBar } from "../../searchBar/SearchBar";
import DocumentCard from "./components/DocumentCard";
import { Controls } from "../../controls/Controls";
import { useModal } from "../../../hooks/useModal";

export const Documents = () => {
  const dispatch = useDispatch();
  const documents = useSelector(selectAllDocuments);
  const [openModal, toggleModal] = useModal();

  useEffect(() => {
    dispatch(fetchDocuments());
  }, []);

  function filterByInput(e) {
    dispatch(searchDocumentsByName(e.target.value));
  }

  const handleSortByDateAsc = () => {
    dispatch(sortDocumentsByDateAsc());
  };

  const handleSortByDateDesc = () => {
    dispatch(sortDocumentsByDateDesc());
  };

  const handleSortByNameAsc = () => {
    dispatch(sortDocumentsByNameAsc());
  };

  const handleSortByNameDesc = () => {
    dispatch(sortDocumentsByNameDesc());
  };

  return (
    <div>
      <h3 className="mb-2">Документы</h3>
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
        {documents.map(document => <DocumentCard key={document.id} document={document} />)}
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

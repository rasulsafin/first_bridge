import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { List } from "@mui/material";
import { fetchDocuments, selectAllDocuments } from "../../../services/documentsSlice";
import { SearchBar } from "../../searchBar/SearchBar";
import DocumentCard from "./components/DocumentCard";

export const Documents = () => {
  const dispatch = useDispatch();
  const documents = useSelector(selectAllDocuments);

  useEffect(() => {
    dispatch(fetchDocuments());
  }, []);
  
  return (
    <div>
      <h3 className="mb-2">Документы</h3>
      <div className="toolbar-project">
        <SearchBar />
      </div>
      <List
        sx={{
          display: "flex",
          flexDirection: "row",
          flexWrap: "wrap"
        }}>
        {documents.map(document => <DocumentCard key={document.id} document={document} />)}
      </List>
    </div>
  );
};

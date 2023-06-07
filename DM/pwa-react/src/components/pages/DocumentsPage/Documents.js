import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { List } from "@mui/material";
import { Formik } from "formik";
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
import DocumentFilterForm from "./components/DocumentFilterForm";
import { fetchProjects, selectAllProjects } from "../../../services/projectsSlice";
import { getInitialValues } from "./utils/getInitialValues";
import { isDrawerOpenFromStore } from "../../../services/controlUISlice";
import { fetchUsers, selectAllUsers } from "../../../services/usersSlice";

export const Documents = () => {
  const dispatch = useDispatch();
  const documents = useSelector(selectAllDocuments);
  const projects = useSelector(selectAllProjects);
  const users = useSelector(selectAllUsers);
  const [openModal, toggleModal] = useModal();
  const initialValues = getInitialValues();
  const isOpen = useSelector(isDrawerOpenFromStore);

  useEffect(() => {
    dispatch(fetchDocuments());
    dispatch(fetchProjects());
    dispatch(fetchUsers());
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
          filter="true"
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
      <Controls.Drawer
        open={isOpen}
        onClose={() => dispatch(toggleDrawer)}
      >
        <Formik
          initialValues={initialValues}
          onSubmit={(values, formikHelpers) => {
            console.log(values);
            // formikHelpers.resetForm();
          }}
        >
          <Controls.DrawerForm>
            <Controls.DrawerContent
              title="Фильтр"
              isWithActions="true"
            >
              <DocumentFilterForm
                projects={projects}
                users={users}
              />
            </Controls.DrawerContent>
          </Controls.DrawerForm>
        </Formik>
      </Controls.Drawer>
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
};

import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Grid, List } from "@mui/material";
import { Formik } from "formik";
import {
  fetchDocuments, filteringDocuments,
  searchDocumentsByName,
  selectAllDocuments,
  sortDocumentsByDateAsc,
  sortDocumentsByDateDesc,
  sortDocumentsByNameAsc,
  sortDocumentsByNameDesc
} from "../../../services/documentsSlice";
import { SearchBar } from "../../searchBar/SearchBar";
import { DocumentCard } from "./components/DocumentCard";
import { Controls } from "../../controls/Controls";
import { useModal } from "../../../hooks/useModal";
import { DocumentFilterForm } from "./components/DocumentFilterForm";
import { fetchProjects, selectAllProjects } from "../../../services/projectsSlice";
import { getInitialValues } from "./utils/getInitialValues";
import { isDrawerOpenFromStore, toggleDrawer } from "../../../services/controlUISlice";
import { fetchUsers, selectAllUsers } from "../../../services/usersSlice";
import "../../layout/Layout.css";
import { ReactComponent as ExportIcon } from "../../../assets/icons/export.svg";
import { ReactComponent as TrashIcon } from "../../../assets/icons/trashcan.svg";

export const Documents = () => {
  const dispatch = useDispatch();
  const documents = useSelector(selectAllDocuments);
  const projects = useSelector(selectAllProjects);
  const users = useSelector(selectAllUsers);
  const [openModal, toggleModal] = useModal();
  const isOpen = useSelector(isDrawerOpenFromStore);
  const [initialValues, setInitialValues] = useState(getInitialValues());
  const [numberOfActiveFilters, setNumberOfActiveFilters] = useState(null);
  const [checked, setChecked] = useState([]);
  const [activeButton, setActiveButton] = useState("");

  useEffect(() => {
    dispatch(fetchDocuments());
    dispatch(fetchProjects());
    dispatch(fetchUsers());
  }, []);

  function filterByInput(e) {
    dispatch(searchDocumentsByName(e.target.value));
  }

  const handleSortByDateAsc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortDocumentsByDateAsc());
  };

  const handleSortByDateDesc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortDocumentsByDateDesc());
  };

  const handleSortByNameAsc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortDocumentsByNameAsc());
  };

  const handleSortByNameDesc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortDocumentsByNameDesc());
  };

  const applyFilters = (filters) => {
    const activeFilters = { ...filters };

    Object.keys(activeFilters).forEach(key => {
      if (activeFilters[key] === "") {
        delete activeFilters[key];
      }
    });

    setNumberOfActiveFilters(Object.keys(activeFilters).length);

    dispatch(filteringDocuments(activeFilters));
  };

  const handleToggle = (documentId) => () => {
    const currentIndex = checked.indexOf(documentId);
    const newChecked = [...checked];
    console.log(documentId);
    if (currentIndex === -1) {
      newChecked.push(documentId);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

  const handleSelectAll = () => {
    const docIds = [];
    documents.forEach(function(item) {
      docIds.push(item.id);
    });
    setChecked(docIds);
  };

  return (
    <div className="component-container">
      <div className="header-toolbar">
        <div className="header-title">Документы</div>
        <SearchBar
          onChange={e => filterByInput(e)}
          filter="true"
          numberOfActiveFilters={numberOfActiveFilters}
        />
        <Grid direction="row" container>
          <Grid item xs={8} sm={8} lg={8}>
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
          </Grid>
          <Grid container item xs={4} sm={4} lg={4} justifyContent="flex-end">
            {checked.length > 0 ?
              <div>
                <Controls.Button
                  startIcon={<ExportIcon width="16px" />}
                  style={{
                    backgroundColor: "#FFF",
                    color: "#2D2926"
                  }}
                >Экспортировать</Controls.Button>
                <Controls.Button
                  startIcon={<TrashIcon />}
                  className="m-0"
                  style={{
                    backgroundColor: "#FFF",
                    color: "#2D2926"
                  }}
                >В архив</Controls.Button>
              </div>
              : null
            }
          </Grid>
        </Grid>
        {checked.length > 0 ?
          <Grid container>
            <Grid item xs={8} sm={8} lg={8}>
              Выбрано: {checked.length}
            </Grid>
            <Grid container item xs={4} sm={4} lg={4} justifyContent="flex-end">
              <Controls.Button
                className="m-0"
                onClick={handleSelectAll}
              >Выбрать все
              </Controls.Button>
            </Grid>
          </Grid>
          : null
        }
      </div>
      <List
        sx={{
          display: "flex",
          flexDirection: "row",
          flexWrap: "wrap"
          // justifyContent: "space-between"
        }}>
        {documents.map(document =>
          <DocumentCard
            key={document.id}
            document={document}
            handleToggle={handleToggle}
            checked={checked}
          />)}
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
            console.log("values", values);
            applyFilters(values);
            // formikHelpers.resetForm();
            setInitialValues(values);
            dispatch(toggleDrawer());
          }}
        >
          <Controls.DrawerForm>
            <Controls.DrawerContent
              title="Фильтр"
              isWithActions="true"
              // cancelButtonProps={{ onClick: formikHelpers.resetForm() }}
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

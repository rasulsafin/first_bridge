import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Grid, List } from "@mui/material";
import {
  fetchRecords,
  searchRecordsByName,
  selectAllRecords,
  sortRecordsByDateAsc,
  sortRecordsByDateDesc,
  sortRecordsByNameAsc,
  sortRecordsByNameDesc
} from "../../../services/recordsSlice";
import { SearchBar } from "../../searchBar/SearchBar";
import { RecordCard } from "./components/RecordCard";
import { Controls } from "../../controls/Controls";
import { ReactComponent as PlanIcon } from "../../../assets/icons/plan.svg";
import { ReactComponent as BurgerIcon } from "../../../assets/icons/burger.svg";
import { ReactComponent as TrashIcon } from "../../../assets/icons/trashcan.svg";
import { ReactComponent as ReportIcon } from "../../../assets/icons/report.svg";
import { useModal } from "../../../hooks/useModal";
import "../../layout/Layout.css";

export function Records() {
  const dispatch = useDispatch();
  const records = useSelector(selectAllRecords);
  const [openModal, toggleModal] = useModal();

  useEffect(() => {
    dispatch(fetchRecords());
  }, []);

  function filterByInput(e) {
    dispatch(searchRecordsByName(e.target.value));
  }

  const handleSortByDateAsc = () => {
    dispatch(sortRecordsByDateAsc());
  };

  const handleSortByDateDesc = () => {
    dispatch(sortRecordsByDateDesc());
  };

  const handleSortByNameAsc = () => {
    dispatch(sortRecordsByNameAsc());
  };

  const handleSortByNameDesc = () => {
    dispatch(sortRecordsByNameDesc());
  };

  const iconBurger = (
    <BurgerIcon className="icon-role active" />
  );

  return (
    <div className="component-container">
      <div className="header-toolbar">
        <Grid direction="row" container>
          <Grid item xs={9} sm={9} lg={9}>
            <div className="header-title">Задачи</div>
          </Grid>
          <Grid container item xs={3} sm={3} lg={3} justifyContent="flex-end">
            <Grid item>
              <Controls.Button
                startIcon={iconBurger}
                className="m-0"
                style={{
                  backgroundColor: "#2D2926",
                  color: "#FFF",
                  border: "none"
                }}
              >Список</Controls.Button>
              <Controls.Button
                startIcon={<PlanIcon />}
                className="m-0"
                style={{
                  backgroundColor: "#FFF",
                  color: "#2D2926",
                  border: "none"
                }}
                // onClick={handleNavigateToRolesPage}
              >План</Controls.Button>
            </Grid>
          </Grid>
        </Grid>
        <SearchBar
          onChange={e => filterByInput(e)}
          filter="true"
        />
        <Grid direction="row" container>
          <Grid item xs={8} sm={8} lg={8}>
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
          </Grid>
          <Grid container item xs={4} sm={4} lg={4} justifyContent="flex-end">
            <div>
              <Controls.Button
                startIcon={<ReportIcon />}
                style={{
                  backgroundColor: "#FFF",
                  color: "#2D2926",
                  border: "none"
                }}
              >Отчет</Controls.Button>
              <Controls.Button
                startIcon={<TrashIcon />}
                className="m-3 mr-0"
                style={{
                  backgroundColor: "#FFF",
                  color: "#2D2926",
                  border: "none"
                }}
              >В архив</Controls.Button>
            </div>
          </Grid>
        </Grid>
      </div>
      <List>
        {records.map(record => <RecordCard key={record.id} record={record} />)}
      </List>
      <Controls.Modal
        titleModal="TitleCreateModal"
        open={openModal}
        onClose={toggleModal}
      >
        <Grid container direction="column">
          <Grid item>
          </Grid>
        </Grid>
      </Controls.Modal>
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
}

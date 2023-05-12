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
import { useModal } from "../../../hooks/useModal";

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
    <div>
      <Grid direction="row" container>
        <Grid item xs={9} sm={9} lg={9}>
          <h3 className="mb-2">Задачи</h3>
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
          >Сначала новые</Controls.Button>
          <Controls.Button
            style={{
              backgroundColor: "#FFF",
              color: "#2D2926",
              border: "none"
            }}
            onClick={handleSortByDateDesc}
          >Сначала старые</Controls.Button>
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

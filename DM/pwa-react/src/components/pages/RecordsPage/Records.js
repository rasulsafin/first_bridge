import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Grid, List } from "@mui/material";
import { Formik } from "formik";
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
import "./Records.css";
import { RecordForm } from "./components/RecordForm";
import { getRecordInitialValues } from "./utils/getRecordInitialValues";

export function Records() {
  const dispatch = useDispatch();
  const records = useSelector(selectAllRecords);
  const [openModal, toggleModal] = useModal();
  const [checked, setChecked] = useState([]);
  const [activeButton, setActiveButton] = useState("");
  const initialValues = getRecordInitialValues();

  useEffect(() => {
    dispatch(fetchRecords());
  }, []);

  function filterByInput(e) {
    dispatch(searchRecordsByName(e.target.value));
  }

  const handleSortByDateAsc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordsByDateAsc());
  };

  const handleSortByDateDesc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordsByDateDesc(event));
  };

  const handleSortByNameAsc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordsByNameAsc(event));
  };

  const handleSortByNameDesc = (event) => {
    setActiveButton(event.target.id);
    dispatch(sortRecordsByNameDesc());
  };

  const iconBurger = (
    <BurgerIcon className="icon-role active" />
  );

  const handleToggle = (recordId) => () => {
    const currentIndex = checked.indexOf(recordId);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(recordId);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

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
                id="1"
                className="ml-0"
                style={{
                  backgroundColor: activeButton === "1" ? "#2D2926" : "#FFF",
                  color: activeButton === "1" ? "#FFF" : "#2D2926"
                }}
                onClick={(event) => handleSortByDateAsc(event)}
              >Новые</Controls.Button>
              <Controls.Button
                id="2"
                style={{
                  backgroundColor: activeButton === "2" ? "#2D2926" : "#FFF",
                  color: activeButton === "2" ? "#FFF" : "#2D2926"
                }}
                onClick={(event) => handleSortByDateDesc(event)}
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
                  startIcon={<ReportIcon />}
                  style={{
                    backgroundColor: "#FFF",
                    color: "#2D2926"
                  }}
                >Отчет</Controls.Button>
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
      </div>
      <List>
        {records.map(record =>
          <RecordCard
            key={record.id}
            record={record}
            handleToggle={handleToggle}
            checked={checked}
          />
        )
        }
      </List>
      <Controls.Modal
        open={openModal}
        onClose={toggleModal}
      >
        <Formik
          initialValues={initialValues}
          onSubmit={(values, formikHelpers) => {
            console.log(values);
            // dispatch(addNewUserWithProjects(values));
            formikHelpers.resetForm();
          }}
        >
          <Controls.ModalForm>
            <Controls.ModalContent
              title="Создание задачи"
              isWithActions="true"
              confirmButtonProps={{
                children: "Создать"
                // disabled: !(isValid && dirty)
              }}
              // cancelButtonProps={{ onClick: toggleIt }}
            >
              <RecordForm />
            </Controls.ModalContent>
          </Controls.ModalForm>
        </Formik>
      </Controls.Modal>
      <Controls.RoundButton onClick={toggleModal} />
    </div>
  );
}

import * as React from "react";
import { Controls } from "../../controls/Controls";
import { useState } from "react";
import { inputTypes } from "../../../constants/inputTypes";
import { useDispatch, useSelector } from "react-redux";
import { addNewTemplate } from "../../../services/recordTemplatesSlice";
import { selectAllProjects } from "../../../services/projectsSlice";
import { Button, Toolbar } from "@mui/material";
import { BiArrowBack } from "react-icons/bi";
import { useNavigate } from "react-router";
import { TiDeleteOutline } from "react-icons/ti";
import { openSnackbar } from "../../../services/snackbarSlice";

const initialProject = {
  id: 0,
  title: ""
};

export function TemplateCreatePage() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [required, setRequired] = useState(false);
  const [inputType, setInputType] = useState("");
  const [inputTitle, setInputTitle] = useState("");
  const [inputList, setInputList] = useState([]);
  const [recordName, setRecordName] = useState("");
  const [project, setProject] = useState(initialProject);
  const projects = useSelector(selectAllProjects);

  const goBack = () => {
    navigate(-1);
  };

  const handleRemoveClick = index => {
    const list = [...inputList];
    list.splice(index, 1);
    setInputList(list);
  };

  const handleAddClick = () => {
    if (inputType === "") {
      alert("Choose your type!");
      return;
    }

    if (inputTitle === "") {
      alert("Choose your destiny!");
      return;
    }

    setInputList([...inputList, {
      title: inputTitle,
      type: inputType,
      required: required
    }]);
    handleResetClick();
  };

  const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRequired(event.target.checked);
  };

  const handleSaveTemplate = () => {
    dispatch(addNewTemplate({
      name: recordName,
      projectId: project,
      recordTemplate: { ...inputList }
    }));
    dispatch(openSnackbar());
    navigate(`/projects`);
  };

  function handleResetClick() {
    setInputType("");
    setInputTitle("");
  }

  return (
    <div className="p-3">
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
      </Toolbar>
      <hr />
      <h3>Create template</h3>
      <div style={{
        display: "flex",
        flexDirection: "column"
      }}>
        <Controls.Input
          name="title"
          label="RecordName"
          type="text"
          value={recordName}
          onChange={(event) => setRecordName(event.target.value)}
          required
        />
        <Controls.SelectProject
          name="project"
          label="project"
          value={project}
          onChange={(event) => setProject(event.target.value)}
          options={projects}
          autoWidth={false}
        />

        {inputList.map((x, i) => {
          return (
            <div
              key={i.index}
              style={{
                alignItems: "center",
                justifyContent: "flex-start",
                display: "flex"
              }}>
              {i >= 0 && <>
                <Controls.Input
                  label={x.title}
                  name="name"
                  type={x.type}
                  required={x.required}
                  inputProps={{ readOnly: true }}
                />
                <Button
                  sx={{
                    minHeight: 0,
                    minWidth: 0,
                    padding: 0
                  }}
                >
                  <TiDeleteOutline
                    size={25}
                    color="#dc143c"
                    onClick={() => handleRemoveClick(i)}
                  />
                </Button>
              </>}
            </div>
          );
        })}
        <hr
          style={{
            width: "28vh"
          }}
        />
        <div style={{
          display: "flex",
          flexDirection: "row",
          alignItems: "center"
        }}>
          <form>
            <Controls.Input
              name="title"
              label="title"
              type="text"
              value={inputTitle}
              onChange={(event) => setInputTitle(event.target.value)}
              required
            />
            <Controls.Select
              name="type"
              label="type"
              value={inputType}
              options={inputTypes}
              onChange={(event) => setInputType(event.target.value)}
              required
            />
            <Controls.Checkbox
              name="isRequired"
              label="Is Required?"
              onChange={handleCheckboxChange}
            />
            <div>
              <Button
                type="reset"
                size="small"
                variant="outlined"
                onClick={handleResetClick}
              >Reset</Button>
              <Button
                type="submit"
                size="small"
                variant="outlined"
                onClick={handleAddClick}
              >Add</Button>
            </div>
          </form>
        </div>
      </div>
      <Button
        type="submit"
        size="small"
        variant="outlined"
        onClick={handleSaveTemplate}
      >Save changes</Button>
    </div>
  );
}
import * as React from "react";
import { Controls } from "../../controls/Controls";
import { Button } from "react-bootstrap";
import { useState } from "react";
import { inputTypes } from "../../../constants/inputTypes";
import { useDispatch, useSelector } from "react-redux";
import { addNewTemplate } from "../../../services/recordTemplatesSlice";
import { selectAllProjects } from "../../../services/projectsSlice";

const initialProject = {
  id: 0,
  title: ""
};

export function GenerateFormPage() {
  const dispatch = useDispatch();
  const [required, setRequired] = useState(false);
  const [inputType, setInputType] = useState("");
  const [inputTitle, setInputTitle] = useState("");
  const [inputList, setInputList] = useState([]);
  const [recordName, setRecordName] = useState("");
  const [project, setProject] = useState(initialProject);

  const projects = useSelector(selectAllProjects);
  console.log(projects);

  const handleInputChange = (e) => {
    console.log(e.target.value);
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

  const styleDiv = {
    margin: "5px",
    padding: "5px",
    border: "1px solid black",
    height: "70vh",
    width: "calc(50% - 20px)"
  };

  const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRequired(event.target.checked);
  };

  const handleSaveTemplate = () => {
    console.log(project);
    dispatch(addNewTemplate({
      name: recordName,
      projectId: project,
      recordTemplate: { ...inputList }
    }));
  };

  const recordTemplate = inputList.map(item => item.index);

  function handleResetClick() {
    setInputType("");
    setInputTitle("");
    console.log(recordTemplate);
  }

  return (
    <div>
      <h1>Generate Form</h1>
      <div style={{
        display: "flex",
        justifyContent: "space-evenly"
      }}>
        <div style={styleDiv}>
          <form>
            <h2>Consructor</h2>
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
                text="reset"
                onClick={handleResetClick}
              >Reset</Button>
              <Button
                text="submit"
                color="default"
                onClick={handleAddClick}
              >Add</Button>
            </div>
          </form>
        </div>
        <div style={styleDiv}>
          <h2>View</h2>
          <div>
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
          </div>

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
                    onChange={e => handleInputChange(e)}
                  />
                  <button
                    style={{
                      margin: "5px",
                      padding: "5px",
                      backgroundColor: "crimson",
                      height: "30px",
                      width: "30px",
                      display: "flex",
                      justifyContent: "center",
                      alignItems: "center",
                      border: "1px black solid"
                    }}
                    onClick={() => handleRemoveClick(i)}>x
                  </button>
                </>}
              </div>
            );
          })}
          <Button
            type="submit"
            text="Submit"
            onClick={handleSaveTemplate}>Save changes</Button>
        </div>
      </div>
    </div>
  );
}
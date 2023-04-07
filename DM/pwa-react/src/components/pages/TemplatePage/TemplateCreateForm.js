import * as React from "react";
import { Controls } from "../../controls/Controls";
import { useState } from "react";
import { inputTypes } from "../../../constants/inputTypes";
import { useDispatch, useSelector } from "react-redux";
import { addNewTemplate } from "../../../services/recordTemplatesSlice";
import { selectAllProjects } from "../../../services/projectsSlice";
import { Button } from "@mui/material";
import { useNavigate } from "react-router";
import { TiDeleteOutline } from "react-icons/ti";
import { TemplateEnumForm } from "./TemplateEnumForm";
import { TemplateInputForm } from "./TemplateInputForm";
import { TemplateLinkForm } from "./TemplateLinkForm";

const initialProject = {
  id: 0,
  title: ""
};

export function TemplateCreateForm() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [inputType, setInputType] = useState("");
  const [inputList, setInputList] = useState([]);
  const [recordName, setRecordName] = useState("");
  const [project, setProject] = useState(initialProject);
  const projects = useSelector(selectAllProjects);

  const handleRemoveClick = index => {
    const list = [...inputList];
    list.splice(index, 1);
    setInputList(list);
  };

  function handleAddList(enumTitle, enumList) {
    setInputList([...inputList, {
      title: enumTitle,
      type: "enum",
      options: enumList
    }]);
    setInputType("");
  }

  function handleAddInput(inputTitle, inputType, required) {
    setInputList([...inputList, {
      title: inputTitle,
      type: inputType,
      required: required
    }]);
    setInputType("");
  }

  const handleSaveTemplate = () => {
    dispatch(addNewTemplate({
      name: recordName,
      projectId: project,
      recordTemplate: { ...inputList }
    }));
    navigate(`/projects`);
  };

  return (
    <div>
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
          <Controls.Select
            name="type"
            label="type"
            value={inputType}
            options={inputTypes}
            onChange={(event) => setInputType(event.target.value)}
            required
          />
        </div>
      </div>
      <Controls.Button
        className="m-1"
        type="submit"
        onClick={handleSaveTemplate}
      >Save changes</Controls.Button>

      {(inputType === "enum") &&
        (<>
          <hr />
          <TemplateEnumForm onClickAddList={handleAddList} />
        </>)
      }
      {(inputType === "input") &&
        (<>
          <hr />
          <TemplateInputForm onClickAddInput={handleAddInput} />
        </>)
      }
      {(inputType === "link") &&
        (<>
          <hr />
          <TemplateLinkForm />
        </>)
      }
    </div>
  );
}
import { Controls } from "../../controls/Controls";
import * as React from "react";
import { useState } from "react";
import { textfieldTypes } from "../../../constants/textfieldTypes";

export const TemplateInputForm = (props) => {
  const [required, setRequired] = useState(false);
  const [textfieldType, setTextfieldType] = useState("");
  const [textfieldTitle, setTextfieldTitle] = useState("");

  return (
    <div>
      <form>
        <Controls.Input
          name="title"
          label="title"
          type="text"
          value={textfieldTitle}
          onChange={(e) => setTextfieldTitle(e.target.value)}
          required
        />
        <Controls.Select
          name="type"
          label="type"
          options={textfieldTypes}
          value={textfieldType}
          onChange={(e) => setTextfieldType(e.target.value)}
          required
        />
        <Controls.Checkbox
          name="required"
          label="Is Required?"
          checked={required}
          onChange={() => setRequired(!required)}
        />
        <div>
          <Controls.Button
            className="m-1"
            onClick={() => props.onClickAddInput(textfieldTitle, textfieldType, required)}
          >Add
          </Controls.Button>
        </div>
      </form>
    </div>
  );
};
import { Controls } from "../../controls/Controls";
import { Button } from "@mui/material";
import { useState } from "react";
import { TiDeleteOutline } from "react-icons/ti";
import * as React from "react";
import { RiAddCircleLine } from "react-icons/ri";

export const TemplateEnumForm = (props) => {
  const [optionList, setOptionList] = useState([]);
  const [value, setValue] = useState("");
  const [enumTitle, setEnumTitle] = useState("");

  function handlerAddOption() {
    setOptionList([...optionList,
      { value }
    ]);
    setValue("");
  }

  function handlerRemoveClick(index) {
    const list = [...optionList];
    list.splice(index, 1);
    setOptionList(list);
  }

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column"
      }}
    >
      <p>
        Ctor Dropdown
      </p>
      <Button
        style={{
          width: "200px"
        }}
        variant="outlined"
        onClick={() => props.onClickAddList(enumTitle, optionList)}
      >
        Create Select
      </Button>
      <Controls.Input
        label="Select Title"
        type="text"
        onChange={(e) => setEnumTitle(e.target.value)}
      />
      <div
        style={{
          display: "flex",
          alignItems: "baseline"
        }}
      >
        <Controls.Input
          name="value"
          label="SelectItem"
          type="text"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />
        <Button
          sx={{
            minHeight: 0,
            minWidth: 0,
            padding: 0
          }}
        ><RiAddCircleLine
          size={25}
          color="#66b834"
          onClick={() => handlerAddOption()}
        />
        </Button>
      </div>

      {optionList.map((x, i) => {
        return (
          <div
            key={i.index}
            style={{
              alignItems: "baseline",
              display: "flex"
            }}>
            <>
              {i >= 0 && <>
                <Controls.Input
                  label={x.value}
                  name="value"
                  type="text"
                />
              </>}
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
                  onClick={(index) => handlerRemoveClick(index)}
                />
              </Button>
            </>
          </div>
        );
      })}
    </div>
  );
};
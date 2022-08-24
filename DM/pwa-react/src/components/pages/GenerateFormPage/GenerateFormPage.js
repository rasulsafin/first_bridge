import * as React from "react";
import { Controls } from "../../controls/Controls";
import { Button } from "react-bootstrap";
import { Checkbox, FormControlLabel } from "@mui/material";

const items = [
  { id: '1', title: '1' },
  { id: '2', title: '2' },
  { id: '3', title: '3' },
]

export function GenerateFormPage() {

  const styleDiv = {
    margin: "5px",
    padding: "5px",
    border: "1px solid black",
    height: "70vh",
    width: "calc(50% - 20px)"
  };

  const inputs = [
    Controls.Input,
    Controls.Select
  ]

  const input = inputs.map(i => <option key={i.index} value={i.name}>{i.name}</option>)
  
  return (
    <div style={{
    }}>
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
          />
            <Controls.Select
              name="type"
              label="type"
              options={inputs}
            />
          <Controls.Input
            name="description"
            label="description"
            type="text"
          />
            <Controls.Checkbox
              name="isRequired"
              label="Is Required?"
            />
              <Controls.RadioGroup
                name="state"
                label="State"
                items={items}
              />
            <div>
              <Button
                type="reset"
                text="Submit">Reset</Button>
              <Button
                text="submit"
                color="default"
              >Submit</Button>
            </div>
          </form>
        </div>
        <div style={styleDiv}>
          <h2>View</h2>
        </div>
      </div>
    </div>
  )
}
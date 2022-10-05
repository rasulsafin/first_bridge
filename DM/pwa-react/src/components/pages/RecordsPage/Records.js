import { Component } from "react";
import RecordsGrid from "./components/RecordsGrid";

export class Records extends Component {
  constructor(props) {
    super(props);
    this.state = {
      records: [],
      addModalShow: false
    };
  }

  render() {
    return (
      <div className="p-4">
        <h1 className="mb-4">Records</h1>
        <div>
        </div>
        <RecordsGrid />
      </div>
    );
  }
}

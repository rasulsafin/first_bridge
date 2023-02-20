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
      <div>
        <h1 className="mb-4">Records</h1>
        <div>
        </div>
        <RecordsGrid />
      </div>
    );
  }
}

import { PdfViewer } from "../../pdfViewer/PdfViewer";
import { PdfCreate } from "../../pdfCreate/PdfCreate";
import Chart from "../../charts/Chart";

export const Home = () => {

  return (
    <div className="p-4">
      <h1> This is Home page. </h1>
      {/*<PdfViewer />*/}
      {/*<PdfCreate />*/}
      <div className="col-6">
        <Chart title="Last 6 Months (Revenue)" aspect={2 / 1} />

      </div>

    </div>
  );
};
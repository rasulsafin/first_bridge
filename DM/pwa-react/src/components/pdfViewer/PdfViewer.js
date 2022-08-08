import { useState } from "react";
import { Worker } from "@react-pdf-viewer/core";
import { Viewer } from "@react-pdf-viewer/core";
import "@react-pdf-viewer/core/lib/styles/index.css";
import { defaultLayoutPlugin } from "@react-pdf-viewer/default-layout";
import "@react-pdf-viewer/default-layout/lib/styles/index.css";
import { scrollModePlugin } from "@react-pdf-viewer/scroll-mode";
import ".//PdfViewer.css";

export function PdfViewer() {

  const defaultLayoutPluginInstance = defaultLayoutPlugin();
  const scrollModePluginInstance = scrollModePlugin();

  const [pdfFile, setPdfFile] = useState(null);
  const [pdfError, setPdfError] = useState("");

  const allowedFiles = ["application/pdf"];
  const handleFile = (e) => {
    let selectedFile = e.target.files[0];
    // console.log(selectedFile.type)
    if (selectedFile) {
      if (selectedFile && allowedFiles.includes(selectedFile.type)) {
        let reader = new FileReader();
        reader.readAsDataURL(selectedFile);
        reader.onloadend = (e) => {
          setPdfError("");
          setPdfFile(e.target.result);
        };
      } else {
        setPdfError("Not a valid pdf");
      }
    } else {
      console.log("please select a pdf");
    }
  };
  return (
    <div className="pdf-container">
      <form>
        <input className="pdf-upload-input" type="file" onChange={handleFile} />
      </form>
      <div className="pdf-viewer">
        {pdfFile && (
          <Worker workerUrl="https://unpkg.com/pdfjs-dist@2.14.305/build/pdf.worker.min.js"
          >
            <Viewer plugins={[defaultLayoutPluginInstance, scrollModePluginInstance]} fileUrl={pdfFile}></Viewer>
          </Worker>
        )}
        {!pdfFile && <>No file</>}
      </div>
    </div>
  );
}
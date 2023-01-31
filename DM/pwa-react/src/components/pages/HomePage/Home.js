import { PdfViewer } from "../../pdfViewer/PdfViewer";
import { PdfCreate } from "../../pdfCreate/PdfCreate";
import Chart from "../../charts/Chart";
import { CircularProgressbar, buildStyles } from "react-circular-progressbar";

export const Home = () => {
  const percentage = 66;
  const percentage1 = 47;
  const percentage2 = 17;
  return (
    <div className="component-container">
      <h3> Dashboard </h3>
      <div style={{
        display: "flex",
        // flexDirection: "row",
        flexWrap: "wrap",
        alignItems: "center",
        justifyContent: "flex-start"
      }}>
        {/*<PdfViewer />*/}
        {/*<PdfCreate />*/}
        <div className="col-12">
          <Chart title="Quantity of Records for 6 Months" aspect={2 / 1} />
        </div>

        <div className="col-2 p-4">
          <h6 className="pl-2">Done tasks</h6>

          <CircularProgressbar
            
            value={percentage}
            text={`${percentage}%`}
            styles={buildStyles({
              // Rotation of path and trail, in number of turns (0-1)
              rotation: 0.25,
              // Whether to use rounded or flat corners on the ends - can use 'butt' or 'round'
              strokeLinecap: 'butt',
              // Text size
              textSize: '16px',
              // How long animation takes to go from one percentage to another, in seconds
              pathTransitionDuration: 0.5,
              // Can specify path transition in more detail, or remove it entirely
              // pathTransition: 'none',
              // Colors
              pathColor: `rgba(62, 152, 199, ${percentage / 100})`,
              textColor: '#f88',
              trailColor: '#d6d6d6',
              backgroundColor: '#3e98c7',
            })}
          />
        </div>
        <div className="col-2 p-4">
          <h6 className="pl-2">Tasks in process</h6>

          <CircularProgressbar
            value={percentage1}
            text={`${percentage1}%`}
            styles={buildStyles({
              // Rotation of path and trail, in number of turns (0-1)
              rotation: 0.25,
              // Whether to use rounded or flat corners on the ends - can use 'butt' or 'round'
              strokeLinecap: 'butt',
              // Text size
              textSize: '16px',
              // How long animation takes to go from one percentage to another, in seconds
              pathTransitionDuration: 0.5,
              // Can specify path transition in more detail, or remove it entirely
              // pathTransition: 'none',
              // Colors
              pathColor: `rgba(210, 5, 43, ${percentage1 / 100})`,
              textColor: '#f88',
              trailColor: '#d6d6d6',
              backgroundColor: '#d2052b',
            })}
          />
        </div>
        <div className="col-2 p-4">
          <h6 className="pl-2">Failed tasks</h6>
          <CircularProgressbar
            
            value={percentage2}
            text={`${percentage2}%`}
            styles={buildStyles({
              // Rotation of path and trail, in number of turns (0-1)
              rotation: 0.25,
              // Whether to use rounded or flat corners on the ends - can use 'butt' or 'round'
              strokeLinecap: 'butt',
              // Text size
              textSize: '16px',
              // How long animation takes to go from one percentage to another, in seconds
              pathTransitionDuration: 0.5,
              // Can specify path transition in more detail, or remove it entirely
              // pathTransition: 'none',
              // Colors
              pathColor: `rgba(210, 5, 43, ${percentage1 / 100})`,
              textColor: '#f88',
              trailColor: '#d6d6d6',
              backgroundColor: '#d2052b',
            })}
          />
        </div>
      </div>
    </div>
    
  );
};
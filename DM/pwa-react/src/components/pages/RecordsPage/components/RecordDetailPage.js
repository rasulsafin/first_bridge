import { useParams } from "react-router";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { selectAllRecords } from "../../../../services/recordsSlice";
import { BsArrowLeftSquareFill } from "react-icons/bs";

export const RecordDetailPage = () => {
  const { id } = useParams();
  const records = useSelector(selectAllRecords);

  const record = records.find(record => record.id === Number(id));

  // const fields = record.fields.map(field => <div><p>NameField: {field.name}</p> <p>Description: {field.description}</p>
  // </div>);
  
  return (
    <div className="p-4">
      <Link to="/records">
        <span style={{ color: "black", textDecoration: "none" }}>
         <BsArrowLeftSquareFill size={30} color="#1d62ad" />
        </span>
      </Link>
      <h1>Record Detail Page</h1>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {record.name}</span></p>
      </div>
      <h3>Fields:</h3>
      <p>
        {/*{fields}*/}
      </p>
    </div>
  );
};
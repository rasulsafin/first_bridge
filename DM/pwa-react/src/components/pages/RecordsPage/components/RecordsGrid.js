import { Table } from "react-bootstrap";

export default function RecordsGrid(props) {

  return (
    <div>
      <Table
        className="mt-5"
        striped
        bordered
        hover
        size="sm"
      >
        <thead>
        <tr>
          <td>Record Name</td>
          <td>fieldName</td>
          <td>Description</td>
          <td>Edit</td>
        </tr>
        </thead>
        <tbody>
        {props.records.map(record =>
          <tr key={record.id}>
            <td>{record.name}</td>
            <td>{record.fields.map(field => <tr key={field.id}>
              <td>{field.name}</td>
            </tr>)}</td>
            <td>{record.fields.map(field => <tr key={field.id}>
              <td>{field.description}</td>
            </tr>)}</td>
            <td>Edit / Delete</td>
          </tr>)}
        </tbody>
      </Table>
    </div>
  );
}
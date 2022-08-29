import { useParams } from "react-router";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { BsArrowLeftSquareFill } from "react-icons/bs";
import { selectAllOrganizations } from "../../../../services/organizationsSlice";

export const OrganizationDetailPage = () => {
  const { id } = useParams();
  const organizations = useSelector(selectAllOrganizations);

  const organization = organizations.find(organization => organization.id === Number(id));

  return (
    <div className="p-4">
      <Link to="/organizations">
        <span style={{ color: "black", textDecoration: "none" }}>
         <BsArrowLeftSquareFill size={30} color="#1d62ad" />
        </span>
      </Link>
      <h1>Project Detail Page</h1>
      <div style={{
        border: "black solid 2px",
        width: "40vh",
        background: "whitesmoke",
        padding: 5
      }}>
        <p>Name: <span style={{ fontSize: 32 }}> {organization.name}</span></p>
      </div>
    </div>
  );
};
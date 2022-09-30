import { Controls } from "../../../controls/Controls";
import { Button, Toolbar } from "@mui/material";
import { useState } from "react";
import { useDispatch } from "react-redux";
import { addNewProject } from "../../../../services/projectsSlice";
import { useNavigate } from "react-router-dom";
import { BiArrowBack } from "react-icons/bi";
import { addNewOrganization } from "../../../../services/organizationsSlice";
import { openSnackbar } from "../../../../services/snackbarSlice";
import SuccessSnackbar from "../../../snackbar/SuccessSnackbar";

const initialValues = {
  name: "",
  address: "",
  inn: "",
  ogrn: "",
  kpp: "",
  phone: "",
  email: "",
};

export const OrganizationCreatePage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [values, setValues] = useState(initialValues);

  const goBack = () => {
    navigate(-1);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value
    });
  };
  
  function createOrg() {
      dispatch(addNewOrganization({
        name: values.name,
        address: values.address,
        inn: values.inn,
        ogrn: values.ogrn,
        kpp: values.kpp,
        phone: values.phone,
        email: values.email,
      }))
    dispatch(openSnackbar());
    navigate(`/organizations`);
  }

  return (
    <div  className="p-3">
      <SuccessSnackbar />
      <Toolbar>
        <Button onClick={goBack} size="small" variant="outlined">
          <BiArrowBack size={24} color="#1d62ad" /></Button>
      </Toolbar>
      <hr />
      <div style={{
        display: "flex",
        flexDirection: "column"
      }}>
        <h3>Create organization</h3>
        <Controls.Input
          name="name"
          label="name"
          type="text"
          onChange={handleInputChange}
          required
        />
        <Controls.Input
          name="address"
          label="address"
          type="text"
          onChange={handleInputChange}
          required
        />
        <Controls.Input
          name="inn"
          label="inn"
          type="number"
          onChange={handleInputChange}
          required
        />
        <Controls.Input
          name="ogrn"
          label="ogrn"
          type="number"
          onChange={handleInputChange}
          required
        />
        <Controls.Input
          name="kpp"
          label="kpp"
          type="number"
          onChange={handleInputChange}
          required
        />
        <Controls.Input
          name="phone"
          label="phone"
          type="text"
          onChange={handleInputChange}
          required
        />
        <Controls.Input
          name="email"
          label="email"
          type="email"
          onChange={handleInputChange}
          required
        />
      </div>
      <Button
        className="m-3"
        size="small"
        variant="outlined"
        onClick={createOrg}>
        Add organization
      </Button>
    </div>
  );
};
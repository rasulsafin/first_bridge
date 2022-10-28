import { FormProvider, useForm } from "react-hook-form";
import { RecordInputCheckboxForm } from "./RecordInputCheckboxForm";
import { RecordInputDropdownForm } from "./RecordInputDropdownForm";
import { RecordInputTextForm } from "./RecordInputTextForm";
import { Controls } from "../../../../controls/Controls";
import { useDispatch } from "react-redux";
import { addNewRecord } from "../../../../../services/recordsSlice";
import { openSnackbar } from "../../../../../services/snackbarSlice";
import { useNavigate } from "react-router";
import { Button } from "@mui/material";

export const RecordCreateForm = (props) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { arrayInput, arrayDropdown, projectId, templateForm, ...other } = props;
  const methods = useForm();
  const { handleSubmit } = methods;

  const onSubmit = (data) => {
    dispatch(addNewRecord({
      name: templateForm.name,
      projectId: projectId,
      fields: data
    }));
    dispatch(openSnackbar());
    navigate(`/project/${projectId}`);
  };

  const arrayInputForm = arrayInput.map((item, index) =>
    <RecordInputTextForm
      key={index}
      name={item.title}
      label={item.title}
      type={item.type}
      required={item.required}
    />
  );

  const arrayDropdownForm = arrayDropdown.map((item, index) =>
    <RecordInputDropdownForm
      key={index}
      name={item.title}
      label={item.title}
      options={item.options}
    />
  );
  
  return (
    <>
      <div
        style={{
          display: "grid",
          width: "40vw"
        }}>
        <FormProvider {...methods}>
          {arrayInputForm}
          {arrayDropdownForm}
        </FormProvider>
        <Controls.Button
          sx={{ width: 200 }}
          onClick={handleSubmit(onSubmit)}
          variant="contained">
          Save Record
        </Controls.Button>
      </div>
    </>
  );
};
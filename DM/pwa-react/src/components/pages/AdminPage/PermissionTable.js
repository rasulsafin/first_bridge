import { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { fetchProjects, selectAllProjects } from "../../../services/projectsSlice";
import "./PermissionTable.css";
import { Button } from "@mui/material";
import { addNewPermission } from "../../../services/permissionsSlice";
import { useParams } from "react-router";

export function PermissionTable() {
  const dispatch = useDispatch();
  const projects = useSelector(selectAllProjects);
  const { id } = useParams();
  const [permissionState, setPermissions] = useState([]);
  const userId = id;

  useEffect(() => {
    dispatch(fetchProjects());
  }, [dispatch]);

  useEffect(() => {
    let permissionState = projects;

    setPermissions(
      permissionState.map(d => {
        return {
          id: d.id,
          title: d.title,
          select: [false, false, false, false]
        };
      })
    );
  }, []);

  function HandleSaveChecks() {
    const permission = permissionState.filter((item) => item.select.some(e => e !== false));

    for (let i = 0; i < permission.length; i++) {
      dispatch(addNewPermission({
        userId: userId,
        objectId: permission[i].id,
        type: 1,
        create: permission[i].select[0],
        read: permission[i].select[1],
        update: permission[i].select[2],
        delete: permission[i].select[3]
      }));
    }
  }

  return (
    <div>
      <table className="permission-table">
        <thead className="permission-thead">
        <tr>
          <th></th>
          <th>Create</th>
          <th>Read</th>
          <th>Uplate</th>
          <th>Delete</th>
        </tr>
        </thead>
        <tbody>
        {permissionState.map((object) =>
          <tr key={object.id}>
            <th>{object.title}</th>
            {object.select.map((s, index) =>
              <td key={index}>
                <input
                  type="checkbox"
                  checked={object.select[index]}
                  onChange={event => {
                    let checked = event.target.checked;
                    setPermissions(
                      permissionState.map((data, i) => {
                        if (object.id === data.id) {
                          data.select[index] = checked;
                        }
                        return data;
                      })
                    );
                  }}
                  value={object.value && "permission" + index + 1}
                  name="permission1" 
                />
              </td>
            )}
          </tr>
        )}
        </tbody>
      </table>
      <div>
        <Button
          variant="outlined"
          onClick={() => HandleSaveChecks()}>Save</Button>
      </div>
    </div>
  );
}
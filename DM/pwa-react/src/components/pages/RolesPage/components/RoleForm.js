import React, { useState } from "react";
import { Grid, TextField } from "@mui/material";
import { getPermissionTypeName } from "../utils/getPermissionTypeName";

export function CheckBoxRow(props) {
  const { permission, setPermissions } = props;

  const styleCheckbox = {
    "marginLeft": "40px"
  };

  function handleChangedPermission(key, IsChecked) {
    setPermissions({ ...permission, [key]: IsChecked });
  }

  console.log(permission);

  return (
    <Grid container>
      <Grid item xs={3}>{getPermissionTypeName(permission.type)}</Grid>
      <Grid item xs={1}>
        <input
          style={styleCheckbox}
          type="checkbox"
          checked={permission.create}
          onChange={e => handleChangedPermission("create", e.currentTarget.checked)}
        />
      </Grid>
      <Grid item xs={1}>
        <input
          style={styleCheckbox}
          type="checkbox"
          checked={permission.read}
          onChange={e => handleChangedPermission("read", e.currentTarget.checked)}
        />
      </Grid>
      <Grid item xs={1}>
        <input
          style={styleCheckbox}
          type="checkbox"
          checked={permission.update}
          onChange={e => handleChangedPermission("update", e.currentTarget.checked)}
        />
      </Grid>
      <Grid item xs={1}>
        <input
          style={styleCheckbox}
          type="checkbox"
          checked={permission.delete}
          onChange={e => handleChangedPermission("delete", e.currentTarget.checked)}
        />
      </Grid>
    </Grid>
  );
}

export const RoleForm = (props) => {
  const { role } = props;
  const [titleRole, setTitleRole] = useState(role.name);
  const [permissions, setPermissions] = useState(role.permissions);

  return (
    <Grid direction="column" container>
      <Grid direction="row" item container>
        <Grid item xs={5}>
          <span style={{ color: "#B3B3B3" }}>
                 Название роли
               </span>
          <TextField
            value={titleRole}
            onChange={(e) => setTitleRole(e.target.value)}
            variant="outlined"
            type="text"
            fullWidth
            size="small"
          />
        </Grid>
      </Grid>
      <Grid item container>
        <Grid item container direction="row">
          <Grid item lg={3}>
            <h6>Модули</h6>
          </Grid>
          <Grid item lg={1}>
            <span>Создание</span>
          </Grid>
          <Grid item lg={1}>
            <span>Просмотр</span>
          </Grid>
          <Grid item lg={1}>
            <span>Изменение</span>
          </Grid>
          <Grid item lg={1}>
            <span>Удаление</span>
          </Grid>
        </Grid>
      </Grid>
      <Grid item container>
        {Object.keys(permissions).map(key =>
          <CheckBoxRow
            key={key}
            permission={permissions[key]}
            setPermissions={newPermissions =>
              setPermissions({
                ...permissions,
                [key]: { ...newPermissions }
              })
            }
          />
        )}
      </Grid>
    </Grid>
  );
};

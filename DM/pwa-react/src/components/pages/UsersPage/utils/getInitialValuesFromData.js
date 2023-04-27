export const getInitialValuesFromData = (user) => {
  return {
    name: user.name ?? "",
    lastName: user.lastName ?? "",
    fathersName: user.fathersName ?? "",
    login: user.login ?? "",
    email: user.email ?? "",
    password: user.password ?? "",
    roles: user.roles ?? "",
    position: user.position ?? "",
    organizationId: user.organizationId ?? ""
  };
};
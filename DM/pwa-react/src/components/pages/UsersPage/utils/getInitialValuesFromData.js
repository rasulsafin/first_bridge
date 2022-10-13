export const getInitialValuesFromData = (user) => {
  return {
    name: user.name ?? "",
    lastName: user.lastName ?? "",
    fathersName: user.fathersName ?? "",
    login: user.login ?? "",
    email: user.email ?? "",
    password: user.password ?? "",
    roles: user.roles ?? "",
    birthdate: user.birthdate ?? "",
    snils: user.snils ?? "",
    position: user.position ?? "",
    organizationId: user.organizationId ?? ""
  };
};
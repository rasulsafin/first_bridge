import { formatDate } from "./formatDate";

export const getUpdateInputDataFromValues = (user) => {
  return {
    userId: user.id,
    name: user.name ?? "",
    lastName: user.lastName ?? "",
    fathersName: user.fathersName ?? "",
    login: user.login ?? "",
    email: user.email ?? "",
    password: user.password ?? "",
    roles: user.roles ?? "",
    birthdate: user.birthdate ? formatDate(user.birthdate, "server")
      : "",
    snils: user.snils ?? "",
    position: user.position ?? "",
    organizationId: user.organizationId ?? ""
  };
};
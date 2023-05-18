export function getPermissionTypeName(type) {
  const typeName = {
    1: "Project",
    2: "Item",
    3: "Record",
    4: "Template",
    5: "Role",
    6: "User",
    7: "Organization",
    8: "Document"
  };

  return typeName[type];
}
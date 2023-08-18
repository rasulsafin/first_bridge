export const getInitialValuesFromData = (template) => {
  return {
    id: template.id ?? "",
    name: template.name ?? "",
    projectId: template.projectId ?? "",
    createdById: template.createdById ?? "",
    updatedById: template.updatedById ?? "",
    createdAt: template.createdAt ?? "",
    updatedAt: template.updatedAt ?? "",
    fields: template.fields ?? [],
    listFields: template.listFields ?? []
  };
};
import dateFnsFormat from "date-fns/format";

const mapFormatToTemplate = (format) => {
  if (format === "server") return "yyyy-MM-dd";
  return "dd-MM-yyyy";
};

export const formatDate = (date, format = "default") => {
  return dateFnsFormat(new Date(date), mapFormatToTemplate(format));
};

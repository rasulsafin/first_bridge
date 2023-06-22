export const formatDate = (date) => {
  const options = {
    year: "numeric",
    month: "long",
    day: "numeric"
  };
  return new Date(date).toLocaleDateString("ru", options).slice(0, -3);
};
export const AdminPage = () => {
  return (
    <div>
      <h1>Admin Page</h1>
      <h2> {localStorage.getItem("user")}</h2>
    </div>
  );
};

import UsersGrid from "../UsersPage/components/UsersGrid";

export const AdminPage = () => {

  const styleDiv = {
    margin: "10px",
    padding: "5px",
  };
  
  return (
    <div>
      <h1>Admin Page</h1>
      <h2> {localStorage.getItem("user")}</h2>
      <div style={{
        border: "1px solid black",
        backgroundColor: "darkgrey",
        height: "10vh",
        display: "flex",
        justifyContent: "flex-start",
        alignItems: "center"
      }}>
         <button style={styleDiv}>Users</button>
         <button style={styleDiv}>Records</button>
         <button style={styleDiv}>Projects</button>
      </div>
      <div>
        <UsersGrid />
      </div>
    </div>
  );
};

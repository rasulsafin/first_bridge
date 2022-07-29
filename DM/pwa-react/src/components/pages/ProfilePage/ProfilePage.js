export const ProfilePage = () => {
  // let userId = TokenHelper.getCurrentUserId();
  return (
    <div>
      <h1>Profile Page</h1>
      <h2> {localStorage.getItem("user")}</h2>
      
    </div>
  );
};

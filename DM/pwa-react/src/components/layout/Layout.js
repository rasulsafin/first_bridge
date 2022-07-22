import { Outlet } from "react-router";
import Sidebar from "./sidebar/Sidebar";
import './Layout.css';


function Layout({ children, ...rest }) {

  const [showNav, setShowNav] = useState(0);
  const toggle = () => setShowNav(Number(!showNav));
  return (
    <Grid {...rest}>
        <>
          <GridNav>
            <Navbar visible={showNav} close={toggle} />
          </GridNav>
          <GridHeader>
            <Header toggle={toggle} />
          </GridHeader>
        </>
      <GridMain>{children}</GridMain>
    </Grid>
  );
}

export default Layout;
import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Badge from '@mui/material/Badge';
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
import AccountCircle from '@mui/icons-material/AccountCircle';
import MailIcon from '@mui/icons-material/Mail';
import NotificationsIcon from '@mui/icons-material/Notifications';
import "./Appbar.css";
import { removeAuthUser } from "../../services/authSlice";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router";

export function Appbar() {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const userAuth = useSelector((state) => state.auth.name);
  const [anchorEl, setAnchorEl] = React.useState(null);

  const isMenuOpen = Boolean(anchorEl);

  const handleProfileMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const toSignIn = () => {
    navigate(`/login`);
  };

  const toProfile = () => {
    navigate(`/profile`);
  };

  const toSettings = () => {
    navigate(`/settings`);
  };
  
  const logOut = () => {
    dispatch(removeAuthUser())
    navigate(`/login`);
  }

  const menuId = 'primary-search-account-menu';
  const renderMenu = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'right',
      }}
      id={menuId}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      open={isMenuOpen}
      onClose={handleMenuClose}
    >
      {!userAuth &&
        <MenuItem onClick={toSignIn}>Sign in</MenuItem>}
      <MenuItem onClick={toProfile}>Profile</MenuItem>
      <MenuItem onClick={toSettings}>Settings</MenuItem>
      {userAuth &&
        <MenuItem onClick={logOut}>Log out</MenuItem>}
    </Menu>
  );

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar className="appbar" >
        <Toolbar>
          
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ display: { sm: 'block' } }}
          >
            BRIO
            
          </Typography>
          
          <Box sx={{ flexGrow: 1 }} />
          <Box sx={{ display: { md: 'flex' } }}>
            
            <IconButton size="large" aria-label="mails" color="inherit">
              <Badge badgeContent={4} color="error">
                <MailIcon />
              </Badge>
            </IconButton>
            
            <IconButton
              size="large"
              aria-label="notifications"
              color="inherit"
            >
              <Badge badgeContent={23} color="error">
                <NotificationsIcon />
              </Badge>
            </IconButton>
            
            <IconButton
              size="large"
              edge="end"
              aria-label="account of current user"
              aria-controls={menuId}
              aria-haspopup="true"
              onClick={handleProfileMenuOpen}
              color="inherit"
              sx={{
                paddingRight: "50px"
              }}
            >
              <AccountCircle />
            </IconButton>
            
          </Box>
        
        </Toolbar>
      </AppBar>
      {renderMenu}
    </Box>
  );
}

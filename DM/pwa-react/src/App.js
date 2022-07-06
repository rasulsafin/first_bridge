import React from 'react';
import './App.css';
import {BrowserRouter, Route, Link, Routes } from 'react-router-dom';
import {AppBar, Box, Typography, Toolbar} from '@mui/material';
import {Home} from "./components/Home";

function App() {
  return (
      <>
          <BrowserRouter>
              <Box sx={{ flexGrow: 1 }}>
                  <AppBar>
                      <Toolbar>
                          <Link to="/"> <Typography sx={{color:"#fff"}}>Home</Typography></Link>
                      </Toolbar>
                  </AppBar>
              </Box>
              <div style={{marginTop: '65px'}}>
                  <Routes>
                      <Route path='/' element={<Home/>}/>
                  </Routes>
              </div>

          </BrowserRouter>
      </>
  );
}

export default App;

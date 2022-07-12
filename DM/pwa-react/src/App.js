import React from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.css';
import {BrowserRouter, Route, Link, Routes } from 'react-router-dom';
import {AppBar, Box, Typography, Toolbar} from '@mui/material';
import {Home} from "./components/pages/HomePage/Home";
import {Users} from "./components/pages/UsersPage/Users";
import {Projects} from "./components/pages/ProjectsPage/Projects";

function App() {
    return (
        <>
            <BrowserRouter>
                <Box sx={{ flexGrow: 1 }}>
                    <AppBar>
                        <Toolbar>
                            <Link to="/"> <Typography sx={{color:"#fff", paddingRight: "40px "}}> Home </Typography></Link>
                            <Link to="/projects"> <Typography sx={{color:"#fff", paddingRight: "40px"}}> Projects </Typography></Link>
                            <Link to="/users"> <Typography sx={{color:"#fff", paddingRight: "40px", }}> Users </Typography></Link>
                        </Toolbar>
                    </AppBar>
                </Box>
                <div style={{marginTop: '80px'}}>
                    <Routes>
                        <Route path='/' element={<Home/>}/>
                        <Route path='/users' element={<Users/>}/>
                        <Route path='/projects' element={<Projects/>}/>
                    </Routes>
                </div>
            </BrowserRouter>
        </>
    );
}

export default App;

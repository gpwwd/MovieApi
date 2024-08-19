import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom'
import Home from './pages/Home';
import About from './pages/About';
import Login from './pages/Login';
import User from  './pages/User';
import "./App.css";
import Movie from './components/Movie';
import MovieDetails from './components/MovieDetails';

const App = () => {
  return (
    <Router>
        <Routes>
          <Route exact path="/" element={<Home/>}/>
          <Route exact path="/login" element={<Login/>}/>
          {/* <Route exact path="/recovery-password" element={<RecoveryPassword/>}/> */}
          <Route exact path="/about" element={<About/>}/>
          {/* <Route path="*" element={<NotFound/>}/> */}
          <Route exact path="/user" element={<User/>}/>
          
          <Route exact path="movie/:id" element={<MovieDetails/>}/>
        </Routes>
    </Router>
  );
}

export default App;
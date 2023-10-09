import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import "./Header.css";
import Account from '../pages/Account'


function Header() {
  return (
    <Router>
      <div className="main-body">
        <nav>
            <ul className="navBar">
              <li><img src="/mlb.png" alt="MLB" className="navBar-icon" /></li>
              <li><Link to="/">Account</Link></li>
            </ul>
        </nav>

        <Routes>
          <Route path="/" element={<Account/>} />
        </Routes>
      </div>
    </Router>
  );
}

export default Header;

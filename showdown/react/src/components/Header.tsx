import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import "./Header.css";
import {ACCOUNT_ROUTE} from 'pages/Account'
import {CHAT_ROUTE} from 'pages/Chat'


function Header() {
  return (
      <div className="main-body">
        <nav>
            <ul className="navBar">
              <li><img src="/mlb.png" alt="MLB" className="navBar-icon" /></li>
              <li><Link to={ACCOUNT_ROUTE}>Account</Link></li>
              <li><Link to={CHAT_ROUTE}>Chat</Link></li>
            </ul>
        </nav>
      </div>
  );
}

export default Header;

import React from 'react';
import './App.css';
import { UserProvider } from './components/UserProvider';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Header from './components/Header';
import Footer from './components/Footer';
import { ACCOUNT_ROUTE, Account } from 'pages/Account'
import { CHAT_ROUTE, Chat } from 'pages/Chat'

function App() {
  return (
    <UserProvider>
      <Router>
        <Routes>
          <Route path={ACCOUNT_ROUTE} element={<Account />} />
          <Route path={CHAT_ROUTE} element={<Chat />} />
        </Routes>
      </Router>
    </UserProvider>
  );
}

export default App;

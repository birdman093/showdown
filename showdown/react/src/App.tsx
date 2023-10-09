import React from 'react';
import './App.css';
import { UserProvider } from './components/UserProvider';
import Header from './components/Header';
import Footer from './components/Footer';

function App() {
  return (
    <>
    <UserProvider>
      <Header/>
    </UserProvider>
    <Footer/>
  </>
  );
}

export default App;

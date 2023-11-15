import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Portfolio from './Portfolio';

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/portfolio" element={<Portfolio />} />
        <Route
          path="/"
          element={<Navigate to="/portfolio" replace />} 
        />
      </Routes>
    </Router>
  );
};

export default App;

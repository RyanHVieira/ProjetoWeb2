import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

import Login from "./Pages/Auth/LoginPage";
import Register from "./Pages/Auth/RegisterPage";
import Home from "./Pages/Home/HomePage";
import Handler from "./Pages/Home/EquipmentHandler";
import ProtectedRoute from "./Components/ProtectedRoute";
import AdminRoute from "./Components/AdminRoute";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/home" />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        // jwt
        <Route element={<ProtectedRoute />}>
          <Route path="/home" element={<Home />} />
          //jwt e admin
          <Route element={<AdminRoute />}>
            <Route path="/handler" element={<Handler />} />
          </Route>
        </Route>

      </Routes>
    </BrowserRouter>
  );
}

export default App;

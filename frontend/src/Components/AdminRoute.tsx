import { Navigate, Outlet } from "react-router-dom";

function getRoleFromToken(token: string) {
  try{
    const payload = token.split(".")[1];
    const decoded = JSON.parse(atob(payload.replace(/-/g, "+").replace(/_/g, "/")));
    return decoded.role;
  }catch{
    return null;
  }
}

export default function AdminRoute() {
  const token = localStorage.getItem("token");
  if (!token) return <Navigate to="/login" replace />;
  const role = getRoleFromToken(token);
  if (role !== "Admin") return <Navigate to="/home" replace />;
  return <Outlet />;
}
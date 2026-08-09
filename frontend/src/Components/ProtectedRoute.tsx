import { useEffect } from "react";
import { Navigate, Outlet, useNavigate } from "react-router-dom";

function getExpiration(token: string) {
  try {
    const payload = token.split(".")[1];
    const decoded = JSON.parse(atob(payload.replace(/-/g, "+").replace(/_/g, "/")));

    return typeof decoded.exp === "number" ? decoded.exp * 1000 : null;
  } catch {
    return null;
  }
}

export default function ProtectedRoute() {
  const navigate = useNavigate();
  const token = localStorage.getItem("token");
  const expiresAt = token ? getExpiration(token) : null;

  useEffect(() => {
    if (!token || !expiresAt) return;
    const remainingTime = expiresAt - Date.now();
    if (remainingTime <= 0) {
      localStorage.removeItem("token");
      navigate("/login", { replace: true });
      return;
    }

    const timer = window.setTimeout(() => {
      localStorage.removeItem("token");
      navigate("/login", { replace: true });
    }, remainingTime);
    return () => window.clearTimeout(timer);
  },[token, expiresAt, navigate]);

  if (!token || !expiresAt || expiresAt <= Date.now()) {
    localStorage.removeItem("token");
    return <Navigate to="/login" replace />;
  }
  return <Outlet />;
}
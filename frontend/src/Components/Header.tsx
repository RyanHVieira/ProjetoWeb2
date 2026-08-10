import { Link } from "react-router-dom";
import "../styles/header.css";

function getRole() {
  const token = localStorage.getItem("token");
  if (!token) return null;
  try {
    const payload = token.split(".")[1];
    const decoded = JSON.parse(atob(payload.replace(/-/g, "+").replace(/_/g, "/")));
    return decoded.role || null;
  }catch(error){
    return null;
  }
}

export default function Header() {
  const role = getRole();
  const isAdmin = role === "Admin";
  const handleLogout = () => {
    localStorage.removeItem("token");
    window.location.href = "/login";
  };

  return (
    <header className="header-root">
      <div className="header-content">
        <div className="header-brand">
          <div className="header-logo">W2</div>
          <h2 className="header-title">Gestão de Equipamentos</h2>
        </div>
        <div className="header-actions">{isAdmin && (
            <>
              <Link className="header-link" to="/home">Início</Link>
              <Link className="header-link" to="/handler">Painel</Link>
            </>
          )}
          <button className="btn-logout" onClick={handleLogout}>Sair</button>
        </div>
      </div>
    </header>
  );
}
export default function Header() {
  const handleLogout = () => {
    localStorage.removeItem("token");
    window.location.href = "/login";
  };

  return (
    <>
      <style>{`
        .header-root {
          width: 100%;
          border-bottom: 1px solid var(--border);
          background: var(--bg);
          backdrop-filter: blur(8px);
          position: sticky;
          top: 0;
          z-index: 50;
        }

        .header-content {
          max-width: 1126px;
          margin: 0 auto;
          padding: 16px 24px;
          display: flex;
          align-items: center;
          justify-content: space-between;
        }

        .header-brand {
          display: flex;
          align-items: center;
          gap: 12px;
        }

        .header-logo {
          width: 32px;
          height: 32px;
          border-radius: 8px;
          background: var(--accent-bg);
          border: 1px solid var(--accent-border);
          display: flex;
          align-items: center;
          justify-content: center;
          color: var(--accent);
          font-weight: 700;
          font-size: 16px;
        }

        .header-title {
          font-size: 18px;
          font-weight: 600;
          color: var(--text-h);
          margin: 0;
        }

        .header-actions {
          display: flex;
          align-items: center;
          gap: 12px;
        }

        .btn-logout {
          background: var(--social-bg);
          color: var(--text-h);
          border: 1px solid var(--border);
          padding: 8px 16px;
          border-radius: 6px;
          font-size: 14px;
          font-weight: 500;
          cursor: pointer;
          transition: all 0.2s ease;

          &:hover {
            box-shadow: var(--shadow);
            border-color: var(--accent-border);
            color: var(--accent);
          }
        }
      `}</style>

      <header className="header-root">
        <div className="header-content">
          <div className="header-brand">
            <div className="header-logo">W2</div>
            <h2 className="header-title">Gestão de Equipamentos</h2>
          </div>

          <div className="header-actions">
            <button className="btn-logout" onClick={handleLogout}>
              Sair
            </button>
          </div>
        </div>
      </header>
    </>
  );
}
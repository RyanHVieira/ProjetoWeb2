import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import Header from "../../Components/Header";

interface EquipmentType {
  id: number;
  nome: string;
}

interface Equipment {
  id: number;
  nome: string;
  tipo?: EquipmentType;
}

function Home() {
  const [equipments, setEquipments] = useState<Equipment[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const token = localStorage.getItem("token");

  useEffect(() => {
    if (!token) return;

    fetch("http://localhost:5227/equipments", {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
      .then(async (response) => {
        if (!response.ok) {
          throw new Error("Não foi possível carregar os equipamentos.");
        }

        return response.json();
      })
      .then((data) => {
        console.log("Dados recebidos:", data);
        console.log("Equipamentos:", data.equipamentos);
        setEquipments(data.equipamentos || []);
      })
      .catch((error) => {
        setError(error.message);
      })
      .finally(() => {
        setLoading(false);
      });
  }, [token]);

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return (
    <>
      <style>{`
        * {
          box-sizing: border-box;
        }

        body {
          margin: 0;
          font-family: 'Inter', -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif;
          background-color: #09090b;
          color: #f4f4f5;
        }

        .home-container {
          min-height: 100vh;
          background: radial-gradient(circle at 50% 0%, #2e1065 0%, #09090b 70%);
          padding: 40px 24px;
        }

        .home-content {
          max-width: 1200px;
          margin: 0 auto;
        }

        .page-title {
          margin: 0 0 32px 0;
          font-size: 32px;
          font-weight: 700;
          letter-spacing: -0.5px;
          background: linear-gradient(135deg, #ffffff 0%, #a1a1aa 100%);
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
        }

        .status-state {
          display: flex;
          justify-content: center;
          align-items: center;
          min-height: 250px;
          background: rgba(24, 24, 27, 0.4);
          backdrop-filter: blur(16px);
          -webkit-backdrop-filter: blur(16px);
          border: 1px solid rgba(255, 255, 255, 0.08);
          border-radius: 20px;
          color: #a1a1aa;
          font-size: 16px;
        }

        .error-state {
          color: #fca5a5;
          border-color: rgba(239, 68, 68, 0.2);
          background: rgba(239, 68, 68, 0.05);
        }

        .equipment-grid {
          display: grid;
          grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
          gap: 24px;
        }

        .equipment-card {
          padding: 24px;
          background: rgba(24, 24, 27, 0.65);
          backdrop-filter: blur(16px);
          -webkit-backdrop-filter: blur(16px);
          border: 1px solid rgba(255, 255, 255, 0.08);
          border-radius: 16px;
          box-shadow: 0 10px 30px rgba(0, 0, 0, 0.3);
          transition: all 0.2s ease;
          display: flex;
          flex-direction: column;
          justify-content: space-between;
        }

        .equipment-card:hover {
          transform: translateY(-4px);
          border-color: rgba(124, 58, 237, 0.3);
          box-shadow: 0 16px 36px rgba(0, 0, 0, 0.4),
                      0 0 40px rgba(124, 58, 237, 0.15);
        }

        .equipment-card-header {
          display: flex;
          justify-content: space-between;
          align-items: flex-start;
          gap: 12px;
          margin-bottom: 16px;
        }

        .equipment-card h2 {
          margin: 0;
          font-size: 20px;
          font-weight: 600;
          color: #ffffff;
        }

        .type-badge {
          display: inline-block;
          padding: 4px 10px;
          background: rgba(124, 58, 237, 0.15);
          border: 1px solid rgba(124, 58, 237, 0.3);
          border-radius: 20px;
          color: #c4b5fd;
          font-size: 12px;
          font-weight: 500;
          white-space: nowrap;
        }

        .equipment-card-footer {
          display: flex;
          justify-content: flex-end;
          padding-top: 16px;
          border-top: 1px solid rgba(255, 255, 255, 0.05);
        }

        .equipment-id {
          font-size: 12px;
          color: #71717a;
          font-family: monospace;
        }
      `}</style>
      <Header/>
      <div className="home-container">
        <div className="home-content">
          <h1 className="page-title">Equipamentos</h1>

          {loading && (
            <div className="status-state">
              <p>Carregando equipamentos...</p>
            </div>
          )}

          {error && (
            <div className="status-state error-state">
              <p>{error}</p>
            </div>
          )}

          {!loading && !error && equipments.length === 0 && (
            <div className="status-state">
              <p>Nenhum equipamento encontrado.</p>
            </div>
          )}

          {!loading && !error && equipments.length > 0 && (
            <div className="equipment-grid">
              {equipments.map((equipment, index) => (
                <div className="equipment-card" key={equipment.id || index}>
                  <div>
                    <div className="equipment-card-header">
                      <h2>{equipment.nome}</h2>
                      <span className="type-badge">
                        {equipment.tipo?.nome || "Não definido"}
                      </span>
                    </div>
                  </div>

                  <div className="equipment-card-footer">
                    <span className="equipment-id">ID: #{equipment.id}</span>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </>
  );
}

export default Home;
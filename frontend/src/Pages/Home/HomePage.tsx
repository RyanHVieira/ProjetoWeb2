import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import Header from "../../Components/Header";
import { apiFetch } from "../../services/api";
import "../../styles/home.css";

interface EquipmentType {id: number; nome: string;}
interface Equipment {id: number; nome: string; tipo?: EquipmentType;}

function Home() {
  const [equipments, setEquipments] = useState<Equipment[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const token = localStorage.getItem("token");

  useEffect(() => {
    if (!token) return;
    const loadEquipments = async () => {
      try{
        const response = await apiFetch("/equipments");
        if (!response.ok) { throw new Error("Não foi possível carregar os equipamentos."); }
        const data = await response.json();
        setEquipments(data.equipamentos || []);
      }catch (error){
        setError(error instanceof Error ? error.message: "Erro ao carregar equipamentos.");
      }finally{
        setLoading(false);
      }
    };
    void loadEquipments();
  }, [token]);

  if (!token) { return <Navigate to="/login" replace />; }

  return (
    <>
      <Header />
      <main className="home-container">
        <div className="home-content">
          <h1 className="page-title">
            Equipamentos
          </h1>
          {loading && (<div className="status-state"><p>Carregando equipamentos...</p></div>)}
          {error && (<div className="status-state error-state"><p>{error}</p></div>)}
          {!loading && !error && equipments.length === 0 && (<div className="status-state"><p>Nenhum equipamento encontrado.</p></div>)}
          {!loading && !error && equipments.length > 0 && (
            <div className="equipment-grid">
              {equipments.map((equipment) => (
                <article className="equipment-card" key={equipment.id}>
                  <div className="equipment-card-header">
                    <h2>{equipment.nome}</h2>
                    <span className="type-badge"> {equipment.tipo?.nome || "Não definido"}</span>
                  </div>
                  <div className="equipment-card-footer"> <span className="equipment-id"> ID: #{equipment.id}</span></div>
                </article>
              ))}
            </div>
          )}
        </div>
      </main>
    </>
  );
}

export default Home;
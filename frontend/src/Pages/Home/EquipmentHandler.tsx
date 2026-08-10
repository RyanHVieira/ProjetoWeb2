import {useEffect,useState,type FormEvent} from "react";
import { Navigate } from "react-router-dom";
import Header from "../../Components/Header";
import "../../styles/equipment-handler.css";
import { apiFetch } from "../../services/api";

interface EquipmentType {id: number; nome: string;}
interface Equipment { id: number; nome: string; tipo?: {id: number; nome: string;};}

export default function EquipmentHandler() {
  const token = localStorage.getItem("token");
  const [equipments, setEquipments] = useState<Equipment[]>([]);
  const [types, setTypes] = useState<EquipmentType[]>([]);
  const [name, setName] = useState("");
  const [typeId, setTypeId] = useState("");
  const [editingId, setEditingId] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState("");
  const [message, setMessage] = useState("");
  const loadData = async () => {
    setLoading(true);
    setError("");
    try {
      const [equipmentsResponse,typesResponse] = await Promise.all([apiFetch("/equipments"),apiFetch("/equipTypes")]);
      const equipmentData = await equipmentsResponse.json();
      const typeData = await typesResponse.json();
      setEquipments(equipmentData.equipamentos || []);
      setTypes(typeData.equipTypes || []);
    }catch (error){ 
      setError(error instanceof Error? error.message: "Erro ao carregar dados.");
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    if (!token) return;
    void loadData();
  }, [token]);

  const clearForm = () => {
    setName("");
    setTypeId("");
    setEditingId(null);
  };

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    setError("");
    setMessage("");
    if(!name.trim() || !typeId){
      setError("Informe o nome e o tipo do equipamento.");
      return;
    }
    setSaving(true);
    try {
      if (editingId !== null) {
        await apiFetch(`/equipments/${editingId}`,{method: "PUT",body: JSON.stringify({nome: name.trim(),tipo: {id: Number(typeId)}})});
        setMessage("Equipamento atualizado com sucesso.");
      }else {
        await apiFetch("/equipments",{method: "POST",body: JSON.stringify({nome: name.trim(),tipoId: Number(typeId)})});
        setMessage("Equipamento criado com sucesso.");
      }
      clearForm();
      await loadData();
    }catch (error){
      setError(error instanceof Error? error.message : "Erro ao salvar equipamento.");
    }finally{
      setSaving(false);
    }
  };

  const startEdit = (equipment: Equipment) => {
    setEditingId(equipment.id);
    setName(equipment.nome);
    setTypeId(equipment.tipo?.id.toString() || "");
    setError("");
    setMessage("");
  };

  const removeEquipment = async (id: number) => {
    if (!window.confirm("Deseja realmente excluir este equipamento?")) return;
    setError("");
    setMessage("");
    try { await apiFetch(`/equipments/${id}`,{method: "DELETE"});
      setMessage("Equipamento excluído com sucesso.");
      if (editingId === id) clearForm();
      await loadData();
    }catch (error){
      setError(error instanceof Error ? error.message : "Erro ao excluir equipamento.");
    }
  };

  if(!token) return(<Navigate to="/login" replace />);

  return (
    <>
      <Header />
      <main className="handler-page">
        <div className="handler-content">
          <div className="handler-heading">
            <div>
              <p className="handler-eyebrow">Painel administrativo</p>
              <h1>{editingId ? "Editar equipamento" : "Gerenciar equipamentos"} </h1>
            </div>

            {editingId !== null && ( <button className="secondary-button" onClick={clearForm}>Cancelar edição</button>)}
          </div>
          <section className="handler-panel">
            <form className="equipment-form" onSubmit={handleSubmit}>
              <div className="form-group">
                <label htmlFor="equipment-name"> Nome </label>
                <input id="equipment-name" value={name} onChange={(event) => setName(event.target.value)} placeholder="Ex.: Notebook Dell" maxLength={50}/>
              </div>
              <div className="form-group">
                <label htmlFor="equipment-type">Tipo</label>
                <select id="equipment-type" value={typeId} onChange={(event) => setTypeId(event.target.value)}>
                  <option value="">Selecione um tipo</option>
                  {types.map((type) => (<option key={type.id} value={type.id}>{type.name}</option>))}
                </select>
              </div>
              <button className="primary-button" type="submit" disabled={saving}>
                {saving ? "Salvando..." : editingId !== null ? "Salvar alterações" : "Adicionar equipamento"}
              </button>
            </form>
            {error && (<p className="feedback feedback-error">{error}</p>)}
            {message && (<p className="feedback feedback-success">{message}</p>)}
          </section>
          <section className="handler-panel">
            <h2>Equipamentos cadastrados</h2>
            {loading ? ( <p className="empty-state">Carregando equipamentos...</p>) : equipments.length === 0 ? (
              <p className="empty-state">Nenhum equipamento cadastrado.</p>):(
              <div className="equipment-table-wrap">
                <table className="equipment-table">
                  <thead>
                    <tr>
                      <th>Nome</th>
                      <th>Tipo</th>
                      <th aria-label="Ações" />
                    </tr>
                  </thead>
                  <tbody>
                    {equipments.map((equipment) => (
                        <tr key={equipment.id}>
                          <td>{equipment.nome}</td>
                          <td>{equipment.tipo?.nome || "Não definido"}</td>
                          <td className="table-actions">
                            <button className="table-button" onClick={() => startEdit(equipment)}>Editar</button>
                            <button className="table-button danger-button"onClick={() =>void removeEquipment(equipment.id)}>Excluir</button>
                          </td>
                        </tr>
                      ))}
                  </tbody>
                </table>
              </div>
            )}
          </section>
        </div>
      </main>
    </>
  );
}
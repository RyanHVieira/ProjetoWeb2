import { useState } from "react";

export default function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const response = await fetch("http://localhost:5227/auth/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          username,
          password,
        }),
      });

      if (!response.ok) {
        throw new Error("Não foi possível criar a conta");
      }

      const data = await response.json();

      console.log("Registro realizado:", data);

      window.location.href = "/login";
    } catch (error) {
      console.error(error);
    }
  };

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

        .auth-container {
          min-height: 100vh;
          display: flex;
          align-items: center;
          justify-content: center;
          background: radial-gradient(circle at 50% 0%, #2e1065 0%, #09090b 70%);
          padding: 20px;
        }

        .auth-box {
          width: 100%;
          max-width: 420px;
          padding: 40px 32px;
          background: rgba(24, 24, 27, 0.65);
          backdrop-filter: blur(16px);
          -webkit-backdrop-filter: blur(16px);
          border: 1px solid rgba(255, 255, 255, 0.08);
          border-radius: 20px;
          box-shadow: 0 20px 40px rgba(0, 0, 0, 0.4),
                      0 0 80px rgba(124, 58, 237, 0.1);
        }

        .auth-header {
          text-align: center;
          margin-bottom: 32px;
        }

        .auth-header h1 {
          margin: 0 0 8px;
          font-size: 28px;
          font-weight: 700;
          letter-spacing: -0.5px;
          background: linear-gradient(135deg, #ffffff 0%, #a1a1aa 100%);
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
        }

        .auth-header p {
          margin: 0;
          font-size: 14px;
          color: #a1a1aa;
        }

        .form-group {
          margin-bottom: 20px;
        }

        .form-group label {
          display: block;
          margin-bottom: 8px;
          font-size: 13px;
          font-weight: 500;
          color: #d4d4d8;
          letter-spacing: 0.2px;
        }

        .form-group input {
          width: 100%;
          padding: 14px 16px;
          background: rgba(9, 9, 11, 0.6);
          border: 1px solid rgba(255, 255, 255, 0.1);
          border-radius: 10px;
          font-size: 15px;
          color: #ffffff;
          outline: none;
          transition: all 0.2s ease;
        }

        .form-group input::placeholder {
          color: #52525b;
        }

        .form-group input:focus {
          border-color: #7c3aed;
          box-shadow: 0 0 0 4px rgba(124, 58, 237, 0.15);
          background: rgba(9, 9, 11, 0.8);
        }

        .auth-button {
          width: 100%;
          padding: 14px;
          margin-top: 10px;
          border: none;
          border-radius: 10px;
          background: linear-gradient(135deg, #7c3aed 0%, #6d28d9 100%);
          color: white;
          font-size: 15px;
          font-weight: 600;
          cursor: pointer;
          transition: all 0.2s ease;
          box-shadow: 0 4px 12px rgba(124, 58, 237, 0.3);
        }

        .auth-button:hover {
          background: linear-gradient(135deg, #8b5cf6 0%, #7c3aed 100%);
          transform: translateY(-1px);
          box-shadow: 0 6px 20px rgba(124, 58, 237, 0.4);
        }

        .auth-button:active {
          transform: translateY(0);
        }

        .auth-link {
          margin-top: 24px;
          text-align: center;
          font-size: 14px;
          color: #71717a;
        }

        .auth-link a {
          color: #a78bfa;
          font-weight: 500;
          text-decoration: none;
          transition: color 0.2s ease;
        }

        .auth-link a:hover {
          color: #c4b5fd;
          text-decoration: underline;
        }
      `}</style>

      <div className="auth-container">
        <div className="auth-box">
          <div className="auth-header">
            <h1>Crie sua conta</h1>
            <p>Preencha os dados abaixo para começar</p>
          </div>

          <form onSubmit={handleSubmit}>
            <div className="form-group">
              <label>Usuário</label>
              <input
                type="text"
                placeholder="Escolha um nome de usuário"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                required
              />
            </div>

            <div className="form-group">
              <label>Senha</label>
              <input
                type="password"
                placeholder="Crie uma senha forte"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>

            <button className="auth-button" type="submit">
              Criar conta
            </button>
          </form>

          <div className="auth-link">
            Já possui uma conta? <a href="/login">Entrar</a>
          </div>
        </div>
      </div>
    </>
  );
}
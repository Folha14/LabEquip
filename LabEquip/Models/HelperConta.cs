using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace LabEquip.Models
{
    public class HelperConta
    {
        private readonly string ConetorHerdado = Program.Conetor;

        public Conta setGuest()
        {
            return new Conta
            {
                GuidConta = Guid.NewGuid(),
                Nome = "Visitante",
                Email = "visitante@labequip.pt",
                NivelAcesso = 0,
                Senha = ""
            };
        }

        public Conta authUser(string email, string senha)
        {
           
            if (email == "admin@labequip.pt" && senha == "admin123")
            {
                return new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Administrador",
                    Email = "admin@labequip.pt",
                    NivelAcesso = 2,
                    Senha = ""
                };
            }
            if (email == "user@labequip.pt" && senha == "user123")
            {
                return new Conta
                {
                    GuidConta = Guid.NewGuid(),
                    Nome = "Utilizador Demo",
                    Email = "user@labequip.pt",
                    NivelAcesso = 1,
                    Senha = ""
                };
            }

            // Se não encontrar hardcoded, tenta na BD
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QUtilizador_Auth";
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                return new Conta
                {
                    GuidConta = (Guid)linha["guidUtilizador"],
                    Nome = linha["nome"].ToString(),
                    Email = linha["email"].ToString(),
                    NivelAcesso = Convert.ToInt32(linha["nivelAcesso"]),
                    Senha = ""
                };
            }
            return setGuest();
        }

        public string serializeConta(Conta conta)
        {
            return JsonSerializer.Serialize(conta);
        }

        public Conta? deserializeConta(string json)
        {
            Conta? c;
            try
            {
                c = JsonSerializer.Deserialize<Conta>(json);
            }
            catch
            {
                c = null;
            }
            return c;
        }
    }
}
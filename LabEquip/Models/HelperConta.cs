using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace LabEquip.Models
{
    public class HelperConta : HelperBase
    {
        public Conta setGuest()
        {
            return new Conta
            {
                guidConta = Guid.NewGuid(),
                nome = "Visitante",
                email = "visitante@labequip.pt",
                nivelAcesso = 0,
                senha = ""
            };
        }

        public Conta authUser(string email, string senha)
        {
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
                    guidConta = (Guid)linha["guidUtilizador"],
                    nome = linha["nome"].ToString(),
                    email = linha["email"].ToString(),
                    nivelAcesso = Convert.ToInt32(linha["nivelAcesso"]),
                    senha = ""
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

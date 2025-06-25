using System.Text.Json;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LabEquip.Models
{
    public class HelperConta : HelperBase
    {
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
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QUtilizador_Auth";
            comando.Parameters.AddWithValue("@Email", email);
            comando.Parameters.AddWithValue("@Senha", senha);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                return new Conta
                {
                    GuidConta = (Guid)linha["GuidUtilizador"],
                    Nome = linha["Nome"].ToString(),
                    Email = linha["Email"].ToString(),
                    NivelAcesso = Convert.ToInt32(linha["NivelAcesso"]),
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
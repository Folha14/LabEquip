using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace LabEquip.Models
{
    public class HelperUtilizador : HelperBase
    {
        public List<Utilizador> list()
        {
            DataTable dt = new DataTable();
            List<Utilizador> saida = new List<Utilizador>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QUtilizador_List";

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Utilizador utilizador = new Utilizador(linha["GuidUtilizador"].ToString());

                utilizador.Nome = linha["Nome"].ToString();
                utilizador.Email = linha["Email"].ToString();
                utilizador.NivelAcesso = Convert.ToInt32(linha["NivelAcesso"]);
                utilizador.Ativo = Convert.ToBoolean(linha["Ativo"]);
                utilizador.DataCriacao = Convert.ToDateTime(linha["DataCriacao"]);
                saida.Add(utilizador);
            }
            return saida;
        }

        public void delete(string guidUtilizador2Del)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QUtilizador_Delete";
            comando.Parameters.AddWithValue("@GuidUtilizador", guidUtilizador2Del);

            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }

        public Utilizador? get(string guidUtilizador)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QUtilizador_Get";
            comando.Parameters.AddWithValue("@GuidUtilizador", guidUtilizador);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                Utilizador utilizador = new Utilizador(linha["GuidUtilizador"].ToString());

                utilizador.Nome = linha["Nome"].ToString();
                utilizador.Email = linha["Email"].ToString();
                utilizador.NivelAcesso = Convert.ToInt32(linha["NivelAcesso"]);
                utilizador.Ativo = Convert.ToBoolean(linha["Ativo"]);
                utilizador.DataCriacao = Convert.ToDateTime(linha["DataCriacao"]);
                return utilizador;
            }
            return null;
        }

        public Boolean save(Utilizador utilizadorSent, string guidUtilizador = "")
        {
            Boolean result = false;
            Utilizador? utilizador2Save;
            string instrucaoSQL = "";

            if (guidUtilizador.IsNullOrEmpty())
            {
                utilizador2Save = new Utilizador();
            }
            else
            {
                utilizador2Save = get(guidUtilizador);
            }

            if (utilizador2Save != null)
            {
                utilizador2Save.Nome = utilizadorSent.Nome;
                utilizador2Save.Email = utilizadorSent.Email;
                utilizador2Save.NivelAcesso = utilizadorSent.NivelAcesso;
                utilizador2Save.Ativo = utilizadorSent.Ativo;

                if (utilizador2Save.GuidUtilizador == Guid.Empty.ToString())
                {
                    utilizador2Save.Senha = utilizadorSent.Senha;
                    instrucaoSQL = "QUtilizador_Insert";
                }
                else
                {
                    instrucaoSQL = "QUtilizador_Update";
                }

                SqlCommand comando = new SqlCommand();
                SqlConnection conexao = new SqlConnection(ConetorHerdado);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = instrucaoSQL;
                comando.Connection = conexao;

                if (instrucaoSQL == "QUtilizador_Insert")
                {
                    comando.Parameters.AddWithValue("@Nome", utilizador2Save.Nome);
                    comando.Parameters.AddWithValue("@Email", utilizador2Save.Email);
                    comando.Parameters.AddWithValue("@Senha", utilizador2Save.Senha);
                    comando.Parameters.AddWithValue("@NivelAcesso", utilizador2Save.NivelAcesso);
                }
                else
                {
                    comando.Parameters.AddWithValue("@GuidUtilizador", utilizador2Save.GuidUtilizador);
                    comando.Parameters.AddWithValue("@Nome", utilizador2Save.Nome);
                    comando.Parameters.AddWithValue("@Email", utilizador2Save.Email);
                    comando.Parameters.AddWithValue("@NivelAcesso", utilizador2Save.NivelAcesso);
                    comando.Parameters.AddWithValue("@Ativo", utilizador2Save.Ativo);
                }

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();
                result = true;
            }
            return result;
        }

        public Boolean alterarEstado(string guidUtilizador, bool ativo)
        {
            Boolean result = false;
            Utilizador? utilizador = get(guidUtilizador);

            if (utilizador != null)
            {
                utilizador.Ativo = ativo;
                result = save(utilizador, guidUtilizador);
            }

            return result;
        }

        public Boolean alterarNivelAcesso(string guidUtilizador, int nivelAcesso)
        {
            Boolean result = false;
            Utilizador? utilizador = get(guidUtilizador);

            if (utilizador != null)
            {
                utilizador.NivelAcesso = nivelAcesso;
                result = save(utilizador, guidUtilizador);
            }

            return result;
        }
    }
}
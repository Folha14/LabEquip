using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace LabEquip.Models
{
    public class HelperRequisicao : HelperBase
    {
        /// <summary>
        /// Lista requisições por estado
        /// </summary>
        /// <param name="estado">Estado da requisição</param>
        /// <returns>Lista de requisições</returns>
        public List<Requisicao> list(Requisicao.EstadoRequisicao estado)
        {
            DataTable dt = new DataTable();
            List<Requisicao> saida = new List<Requisicao>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_List";
            comando.Parameters.AddWithValue("@Estado", (int)estado);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Requisicao requisicao = new Requisicao(linha["GuidRequisicao"].ToString());

                requisicao.GuidEquipamento = linha["GuidEquipamento"].ToString();
                requisicao.GuidUtilizador = linha["GuidUtilizador"].ToString();
                requisicao.DataInicio = Convert.ToDateTime(linha["DataInicio"]);
                requisicao.DataFim = Convert.ToDateTime(linha["DataFim"]);
                requisicao.Motivo = linha["Motivo"].ToString();
                requisicao.Estado = (Requisicao.EstadoRequisicao)Convert.ToByte(linha["Estado"]);
                requisicao.DataRequisicao = Convert.ToDateTime(linha["DataRequisicao"]);
                requisicao.DataAprovacao = linha["DataAprovacao"] == DBNull.Value ? null : Convert.ToDateTime(linha["DataAprovacao"]);
                requisicao.GuidAprovador = linha["GuidAprovador"]?.ToString();
                requisicao.Observacoes = linha["Observacoes"]?.ToString();

                // Propriedades para joins
                requisicao.NomeEquipamento = linha["NomeEquipamento"]?.ToString();
                requisicao.NomeUtilizador = linha["NomeUtilizador"]?.ToString();
                requisicao.EmailUtilizador = linha["EmailUtilizador"]?.ToString();
                requisicao.NomeAprovador = linha["NomeAprovador"]?.ToString();

                saida.Add(requisicao);
            }
            return saida;
        }

        /// <summary>
        /// Lista todas as requisições
        /// </summary>
        /// <returns>Lista de todas as requisições</returns>
        public List<Requisicao> listAll()
        {
            DataTable dt = new DataTable();
            List<Requisicao> saida = new List<Requisicao>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_ListAll";

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Requisicao requisicao = new Requisicao(linha["GuidRequisicao"].ToString());

                requisicao.GuidEquipamento = linha["GuidEquipamento"].ToString();
                requisicao.GuidUtilizador = linha["GuidUtilizador"].ToString();
                requisicao.DataInicio = Convert.ToDateTime(linha["DataInicio"]);
                requisicao.DataFim = Convert.ToDateTime(linha["DataFim"]);
                requisicao.Motivo = linha["Motivo"].ToString();
                requisicao.Estado = (Requisicao.EstadoRequisicao)Convert.ToByte(linha["Estado"]);
                requisicao.DataRequisicao = Convert.ToDateTime(linha["DataRequisicao"]);
                requisicao.DataAprovacao = linha["DataAprovacao"] == DBNull.Value ? null : Convert.ToDateTime(linha["DataAprovacao"]);
                requisicao.GuidAprovador = linha["GuidAprovador"]?.ToString();
                requisicao.Observacoes = linha["Observacoes"]?.ToString();

                // Propriedades para joins
                requisicao.NomeEquipamento = linha["NomeEquipamento"]?.ToString();
                requisicao.NomeUtilizador = linha["NomeUtilizador"]?.ToString();
                requisicao.EmailUtilizador = linha["EmailUtilizador"]?.ToString();
                requisicao.NomeAprovador = linha["NomeAprovador"]?.ToString();

                saida.Add(requisicao);
            }
            return saida;
        }

        /// <summary>
        /// Lista requisições de um utilizador específico
        /// </summary>
        /// <param name="guidUtilizador">GUID do utilizador</param>
        /// <returns>Lista de requisições do utilizador</returns>
        public List<Requisicao> listByUser(string guidUtilizador)
        {
            DataTable dt = new DataTable();
            List<Requisicao> saida = new List<Requisicao>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_ListByUser";
            comando.Parameters.AddWithValue("@GuidUtilizador", guidUtilizador);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Requisicao requisicao = new Requisicao(linha["GuidRequisicao"].ToString());

                requisicao.GuidEquipamento = linha["GuidEquipamento"].ToString();
                requisicao.GuidUtilizador = linha["GuidUtilizador"].ToString();
                requisicao.DataInicio = Convert.ToDateTime(linha["DataInicio"]);
                requisicao.DataFim = Convert.ToDateTime(linha["DataFim"]);
                requisicao.Motivo = linha["Motivo"].ToString();
                requisicao.Estado = (Requisicao.EstadoRequisicao)Convert.ToByte(linha["Estado"]);
                requisicao.DataRequisicao = Convert.ToDateTime(linha["DataRequisicao"]);
                requisicao.DataAprovacao = linha["DataAprovacao"] == DBNull.Value ? null : Convert.ToDateTime(linha["DataAprovacao"]);
                requisicao.GuidAprovador = linha["GuidAprovador"]?.ToString();
                requisicao.Observacoes = linha["Observacoes"]?.ToString();

                // Propriedades para joins
                requisicao.NomeEquipamento = linha["NomeEquipamento"]?.ToString();
                requisicao.NomeUtilizador = linha["NomeUtilizador"]?.ToString();
                requisicao.EmailUtilizador = linha["EmailUtilizador"]?.ToString();
                requisicao.NomeAprovador = linha["NomeAprovador"]?.ToString();

                saida.Add(requisicao);
            }
            return saida;
        }

        /// <summary>
        /// Obtém uma requisição específica pelo GUID
        /// </summary>
        /// <param name="guidRequisicao">GUID da requisição</param>
        /// <returns>Requisição encontrada ou null</returns>
        public Requisicao? get(string guidRequisicao)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_Get";
            comando.Parameters.AddWithValue("@GuidRequisicao", guidRequisicao);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                Requisicao requisicao = new Requisicao(linha["GuidRequisicao"].ToString());

                requisicao.GuidEquipamento = linha["GuidEquipamento"].ToString();
                requisicao.GuidUtilizador = linha["GuidUtilizador"].ToString();
                requisicao.DataInicio = Convert.ToDateTime(linha["DataInicio"]);
                requisicao.DataFim = Convert.ToDateTime(linha["DataFim"]);
                requisicao.Motivo = linha["Motivo"].ToString();
                requisicao.Estado = (Requisicao.EstadoRequisicao)Convert.ToByte(linha["Estado"]);
                requisicao.DataRequisicao = Convert.ToDateTime(linha["DataRequisicao"]);
                requisicao.DataAprovacao = linha["DataAprovacao"] == DBNull.Value ? null : Convert.ToDateTime(linha["DataAprovacao"]);
                requisicao.GuidAprovador = linha["GuidAprovador"]?.ToString();
                requisicao.Observacoes = linha["Observacoes"]?.ToString();

                // Propriedades para joins
                requisicao.NomeEquipamento = linha["NomeEquipamento"]?.ToString();
                requisicao.NomeUtilizador = linha["NomeUtilizador"]?.ToString();
                requisicao.EmailUtilizador = linha["EmailUtilizador"]?.ToString();
                requisicao.NomeAprovador = linha["NomeAprovador"]?.ToString();

                return requisicao;
            }
            return null;
        }

        /// <summary>
        /// Grava uma requisição (INSERT ou UPDATE)
        /// </summary>
        /// <param name="requisicaoSent">Requisição a gravar</param>
        /// <param name="guidRequisicao">GUID da requisição (vazio para INSERT)</param>
        /// <returns>True se gravou com sucesso</returns>
        public Boolean save(Requisicao requisicaoSent, string guidRequisicao = "")
        {
            Boolean result = false;
            Requisicao? requisicao2Save;
            string instrucaoSQL = "";

            if (guidRequisicao.IsNullOrEmpty())
            {
                requisicao2Save = new Requisicao();
            }
            else
            {
                requisicao2Save = get(guidRequisicao);
            }

            if (requisicao2Save != null)
            {
                requisicao2Save.GuidEquipamento = requisicaoSent.GuidEquipamento;
                requisicao2Save.GuidUtilizador = requisicaoSent.GuidUtilizador;
                requisicao2Save.DataInicio = requisicaoSent.DataInicio;
                requisicao2Save.DataFim = requisicaoSent.DataFim;
                requisicao2Save.Motivo = requisicaoSent.Motivo;
                requisicao2Save.Estado = requisicaoSent.Estado;
                requisicao2Save.Observacoes = requisicaoSent.Observacoes;

                if (requisicao2Save.GuidRequisicao == Guid.Empty.ToString())
                {
                    instrucaoSQL = "QRequisicao_Insert";
                    requisicao2Save.DataRequisicao = DateTime.Now;
                }
                else
                {
                    instrucaoSQL = "QRequisicao_Update";
                }

                SqlCommand comando = new SqlCommand();
                SqlConnection conexao = new SqlConnection(ConetorHerdado);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = instrucaoSQL;
                comando.Connection = conexao;

                comando.Parameters.AddWithValue("@GuidRequisicao", requisicao2Save.GuidRequisicao);
                comando.Parameters.AddWithValue("@GuidEquipamento", requisicao2Save.GuidEquipamento);
                comando.Parameters.AddWithValue("@GuidUtilizador", requisicao2Save.GuidUtilizador);
                comando.Parameters.AddWithValue("@DataInicio", requisicao2Save.DataInicio);
                comando.Parameters.AddWithValue("@DataFim", requisicao2Save.DataFim);
                comando.Parameters.AddWithValue("@Motivo", requisicao2Save.Motivo);
                comando.Parameters.AddWithValue("@Estado", (int)requisicao2Save.Estado);
                comando.Parameters.AddWithValue("@DataRequisicao", requisicao2Save.DataRequisicao);
                comando.Parameters.AddWithValue("@DataAprovacao", requisicao2Save.DataAprovacao ?? (object)DBNull.Value);
                comando.Parameters.AddWithValue("@GuidAprovador", requisicao2Save.GuidAprovador ?? (object)DBNull.Value);
                comando.Parameters.AddWithValue("@Observacoes", requisicao2Save.Observacoes ?? (object)DBNull.Value);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Elimina uma requisição
        /// </summary>
        /// <param name="guidRequisicao2Del">GUID da requisição a eliminar</param>
        public void delete(string guidRequisicao2Del)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_Delete";
            comando.Parameters.AddWithValue("@GuidRequisicao", guidRequisicao2Del);

            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }

        /// <summary>
        /// Aprova ou rejeita uma requisição
        /// </summary>
        /// <param name="guidRequisicao">GUID da requisição</param>
        /// <param name="novoEstado">Novo estado (Aprovada ou Rejeitada)</param>
        /// <param name="guidAprovador">GUID do aprovador</param>
        /// <param name="observacoes">Observações do aprovador</param>
        /// <returns>True se alterou com sucesso</returns>
        public Boolean approveReject(string guidRequisicao, Requisicao.EstadoRequisicao novoEstado, string guidAprovador, string? observacoes = null)
        {
            Boolean result = false;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_ApproveReject";
            comando.Parameters.AddWithValue("@GuidRequisicao", guidRequisicao);
            comando.Parameters.AddWithValue("@Estado", (int)novoEstado);
            comando.Parameters.AddWithValue("@GuidAprovador", guidAprovador);
            comando.Parameters.AddWithValue("@DataAprovacao", DateTime.Now);
            comando.Parameters.AddWithValue("@Observacoes", observacoes ?? (object)DBNull.Value);

            conexao.Open();
            int rowsAffected = comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();

            result = rowsAffected > 0;
            return result;
        }

        /// <summary>
        /// Cancela uma requisição
        /// </summary>
        /// <param name="guidRequisicao">GUID da requisição</param>
        /// <returns>True se cancelou com sucesso</returns>
        public Boolean cancel(string guidRequisicao)
        {
            Boolean result = false;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_Cancel";
            comando.Parameters.AddWithValue("@GuidRequisicao", guidRequisicao);

            conexao.Open();
            int rowsAffected = comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();

            result = rowsAffected > 0;
            return result;
        }

        /// <summary>
        /// Obtém número total de requisições
        /// </summary>
        /// <returns>Número de requisições</returns>
        public int getNrRequisicoes()
        {
            int nrRequisicoes = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_GetNumero";

            conexao.Open();
            nrRequisicoes = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();
            conexao.Dispose();

            return nrRequisicoes;
        }

        /// <summary>
        /// Obtém número de requisições pendentes
        /// </summary>
        /// <returns>Número de requisições pendentes</returns>
        public int getNrRequisicoesPendentes()
        {
            int nrPendentes = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_GetNumeroPendentes";

            conexao.Open();
            nrPendentes = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();
            conexao.Dispose();

            return nrPendentes;
        }
    }
}
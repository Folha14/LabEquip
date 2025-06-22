using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace LabEquip.Models
{
    public class HelperEquipamento : HelperBase
    {
        /// <summary>
        /// Lista equipamentos por estado
        /// </summary>
        /// <param name="estado">Estado do equipamento (Disponivel, EmUso, Manutencao, Inativo)</param>
        /// <returns>Lista de equipamentos</returns>
        public List<Equipamento> list(Equipamento.EstadoEquipamento estado)
        {
            DataTable dt = new DataTable();
            List<Equipamento> saida = new List<Equipamento>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_List";
            comando.Parameters.AddWithValue("@Estado", (int)estado);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Equipamento equipamento = new Equipamento(linha["GuidEquipamento"].ToString());

                equipamento.Designacao = linha["Designacao"].ToString();
                equipamento.Descricao = linha["Descricao"].ToString();
                equipamento.Localizacao = linha["Localizacao"].ToString();
                equipamento.NumeroSerie = linha["NumeroSerie"].ToString();
                equipamento.Fabricante = linha["Fabricante"].ToString();
                equipamento.Modelo = linha["Modelo"].ToString();
                equipamento.DataAquisicao = Convert.ToDateTime(linha["DataAquisicao"]);
                equipamento.Estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["Estado"]);
                equipamento.ImagemPath = linha["ImagemPath"]?.ToString();
                equipamento.DataCriacao = Convert.ToDateTime(linha["DataCriacao"]);
                equipamento.DataAtualizacao = Convert.ToDateTime(linha["DataAtualizacao"]);

                saida.Add(equipamento);
            }
            return saida;
        }

        /// <summary>
        /// Lista todos os equipamentos (usado para pesquisas gerais)
        /// </summary>
        /// <returns>Lista de todos os equipamentos</returns>
        public List<Equipamento> listAll()
        {
            DataTable dt = new DataTable();
            List<Equipamento> saida = new List<Equipamento>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_ListAll";

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Equipamento equipamento = new Equipamento(linha["GuidEquipamento"].ToString());

                equipamento.Designacao = linha["Designacao"].ToString();
                equipamento.Descricao = linha["Descricao"].ToString();
                equipamento.Localizacao = linha["Localizacao"].ToString();
                equipamento.NumeroSerie = linha["NumeroSerie"].ToString();
                equipamento.Fabricante = linha["Fabricante"].ToString();
                equipamento.Modelo = linha["Modelo"].ToString();
                equipamento.DataAquisicao = Convert.ToDateTime(linha["DataAquisicao"]);
                equipamento.Estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["Estado"]);
                equipamento.ImagemPath = linha["ImagemPath"]?.ToString();
                equipamento.DataCriacao = Convert.ToDateTime(linha["DataCriacao"]);
                equipamento.DataAtualizacao = Convert.ToDateTime(linha["DataAtualizacao"]);

                saida.Add(equipamento);
            }
            return saida;
        }

        /// <summary>
        /// Pesquisa equipamentos por termo
        /// </summary>
        /// <param name="termo">Termo de pesquisa</param>
        /// <returns>Lista de equipamentos encontrados</returns>
        public List<Equipamento> search(string termo)
        {
            DataTable dt = new DataTable();
            List<Equipamento> saida = new List<Equipamento>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_Search";
            comando.Parameters.AddWithValue("@Termo", termo);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Equipamento equipamento = new Equipamento(linha["GuidEquipamento"].ToString());

                equipamento.Designacao = linha["Designacao"].ToString();
                equipamento.Descricao = linha["Descricao"].ToString();
                equipamento.Localizacao = linha["Localizacao"].ToString();
                equipamento.NumeroSerie = linha["NumeroSerie"].ToString();
                equipamento.Fabricante = linha["Fabricante"].ToString();
                equipamento.Modelo = linha["Modelo"].ToString();
                equipamento.DataAquisicao = Convert.ToDateTime(linha["DataAquisicao"]);
                equipamento.Estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["Estado"]);
                equipamento.ImagemPath = linha["ImagemPath"]?.ToString();
                equipamento.DataCriacao = Convert.ToDateTime(linha["DataCriacao"]);
                equipamento.DataAtualizacao = Convert.ToDateTime(linha["DataAtualizacao"]);

                saida.Add(equipamento);
            }
            return saida;
        }

        /// <summary>
        /// Elimina um equipamento
        /// </summary>
        /// <param name="guidEquipamento2Del">GUID do equipamento a eliminar</param>
        public void delete(string guidEquipamento2Del)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_Delete";
            comando.Parameters.AddWithValue("@GuidEquipamento", guidEquipamento2Del);

            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }

        /// <summary>
        /// Obtém um equipamento específico pelo GUID
        /// </summary>
        /// <param name="guidEquipamento">GUID do equipamento</param>
        /// <returns>Equipamento encontrado ou null</returns>
        public Equipamento? get(string guidEquipamento)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_Get";
            comando.Parameters.AddWithValue("@GuidEquipamento", guidEquipamento);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                Equipamento equipamento = new Equipamento(linha["GuidEquipamento"].ToString());

                equipamento.Designacao = linha["Designacao"].ToString();
                equipamento.Descricao = linha["Descricao"].ToString();
                equipamento.Localizacao = linha["Localizacao"].ToString();
                equipamento.NumeroSerie = linha["NumeroSerie"].ToString();
                equipamento.Fabricante = linha["Fabricante"].ToString();
                equipamento.Modelo = linha["Modelo"].ToString();
                equipamento.DataAquisicao = Convert.ToDateTime(linha["DataAquisicao"]);
                equipamento.Estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["Estado"]);
                equipamento.ImagemPath = linha["ImagemPath"]?.ToString();
                equipamento.DataCriacao = Convert.ToDateTime(linha["DataCriacao"]);
                equipamento.DataAtualizacao = Convert.ToDateTime(linha["DataAtualizacao"]);

                return equipamento;
            }
            return null;
        }

        /// <summary>
        /// Grava um equipamento (INSERT ou UPDATE)
        /// </summary>
        /// <param name="equipamentoSent">Equipamento a gravar</param>
        /// <param name="guidEquipamento">GUID do equipamento (vazio para INSERT)</param>
        /// <returns>True se gravou com sucesso</returns>
        public Boolean save(Equipamento equipamentoSent, string guidEquipamento = "")
        {
            Boolean result = false;
            Equipamento? equipamento2Save;
            string instrucaoSQL = "";

            if (guidEquipamento.IsNullOrEmpty())
            {
                equipamento2Save = new Equipamento();
            }
            else
            {
                equipamento2Save = get(guidEquipamento);
            }

            if (equipamento2Save != null)
            {
                equipamento2Save.Designacao = equipamentoSent.Designacao;
                equipamento2Save.Descricao = equipamentoSent.Descricao;
                equipamento2Save.Localizacao = equipamentoSent.Localizacao;
                equipamento2Save.NumeroSerie = equipamentoSent.NumeroSerie;
                equipamento2Save.Fabricante = equipamentoSent.Fabricante;
                equipamento2Save.Modelo = equipamentoSent.Modelo;
                equipamento2Save.DataAquisicao = equipamentoSent.DataAquisicao;
                equipamento2Save.Estado = equipamentoSent.Estado;
                equipamento2Save.ImagemPath = equipamentoSent.ImagemPath;
                equipamento2Save.DataAtualizacao = DateTime.Now;

                if (equipamento2Save.GuidEquipamento == Guid.Empty.ToString())
                {
                    instrucaoSQL = "QEquipamento_Insert";
                    equipamento2Save.DataCriacao = DateTime.Now;
                }
                else
                {
                    instrucaoSQL = "QEquipamento_Update";
                }

                SqlCommand comando = new SqlCommand();
                SqlConnection conexao = new SqlConnection(ConetorHerdado);
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = instrucaoSQL;
                comando.Connection = conexao;

                comando.Parameters.AddWithValue("@GuidEquipamento", equipamento2Save.GuidEquipamento);
                comando.Parameters.AddWithValue("@Designacao", equipamento2Save.Designacao);
                comando.Parameters.AddWithValue("@Descricao", equipamento2Save.Descricao);
                comando.Parameters.AddWithValue("@Localizacao", equipamento2Save.Localizacao);
                comando.Parameters.AddWithValue("@NumeroSerie", equipamento2Save.NumeroSerie);
                comando.Parameters.AddWithValue("@Fabricante", equipamento2Save.Fabricante);
                comando.Parameters.AddWithValue("@Modelo", equipamento2Save.Modelo);
                comando.Parameters.AddWithValue("@DataAquisicao", equipamento2Save.DataAquisicao);
                comando.Parameters.AddWithValue("@Estado", (int)equipamento2Save.Estado);
                comando.Parameters.AddWithValue("@ImagemPath", equipamento2Save.ImagemPath ?? (object)DBNull.Value);
                comando.Parameters.AddWithValue("@DataCriacao", equipamento2Save.DataCriacao);
                comando.Parameters.AddWithValue("@DataAtualizacao", equipamento2Save.DataAtualizacao);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Altera o estado de um equipamento
        /// </summary>
        /// <param name="guidEquipamento">GUID do equipamento</param>
        /// <param name="novoEstado">Novo estado</param>
        /// <returns>True se alterou com sucesso</returns>
        public Boolean changeState(string guidEquipamento, Equipamento.EstadoEquipamento novoEstado)
        {
            Boolean result = false;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_ChangeState";
            comando.Parameters.AddWithValue("@GuidEquipamento", guidEquipamento);
            comando.Parameters.AddWithValue("@Estado", (int)novoEstado);
            comando.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);

            conexao.Open();
            int rowsAffected = comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();

            result = rowsAffected > 0;
            return result;
        }

        /// <summary>
        /// Obtém número total de equipamentos
        /// </summary>
        /// <returns>Número de equipamentos</returns>
        public int getNrEquipamentos()
        {
            int nrEquipamentos = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetNumero";

            conexao.Open();
            nrEquipamentos = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();
            conexao.Dispose();

            return nrEquipamentos;
        }

        /// <summary>
        /// Obtém número de equipamentos disponíveis
        /// </summary>
        /// <returns>Número de equipamentos disponíveis</returns>
        public int getNrEquipamentosDisponiveis()
        {
            int nrDisponiveis = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetNumeroDisponiveis";

            conexao.Open();
            nrDisponiveis = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();
            conexao.Dispose();

            return nrDisponiveis;
        }

        /// <summary>
        /// Obtém número de equipamentos em uso
        /// </summary>
        /// <returns>Número de equipamentos em uso</returns>
        public int getNrEquipamentosEmUso()
        {
            int nrEmUso = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetNumeroEmUso";

            conexao.Open();
            nrEmUso = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();
            conexao.Dispose();

            return nrEmUso;
        }

        /// <summary>
        /// Verifica se um equipamento está disponível para reserva
        /// </summary>
        /// <param name="guidEquipamento">GUID do equipamento</param>
        /// <param name="dataInicio">Data de início da reserva</param>
        /// <param name="dataFim">Data de fim da reserva</param>
        /// <returns>True se disponível</returns>
        public Boolean isAvailable(string guidEquipamento, DateTime dataInicio, DateTime dataFim)
        {
            Boolean disponivel = false;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_CheckAvailability";
            comando.Parameters.AddWithValue("@GuidEquipamento", guidEquipamento);
            comando.Parameters.AddWithValue("@DataInicio", dataInicio);
            comando.Parameters.AddWithValue("@DataFim", dataFim);

            conexao.Open();
            int conflitos = Convert.ToInt32(comando.ExecuteScalar());
            conexao.Close();
            conexao.Dispose();

            disponivel = conflitos == 0;
            return disponivel;
        }
    }
}
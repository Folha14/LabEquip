using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace LabEquip.Models
{
    public class HelperEquipamento : HelperBase
    {
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
                saida.Add(equipamento);
            }
            return saida;
        }

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
                return equipamento;
            }
            return null;
        }

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


                if (equipamento2Save.GuidEquipamento == Guid.Empty.ToString())
                {
                    instrucaoSQL = "QEquipamento_Insert";

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
                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();
                result = true;
            }
            return result;
        }

        public Boolean updateEstado(string guidEquipamento, Equipamento.EstadoEquipamento novoEstado)
        {
            Boolean result = false;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_UpdateEstado";
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

        public int getTotalDisponiveis()
        {
            int totalDisponiveis = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetNumeroDisponiveis";

            conexao.Open();
            try
            {
                totalDisponiveis = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch
            {
                totalDisponiveis = 0;
            }
            conexao.Close();
            conexao.Dispose();

            return totalDisponiveis;
        }

        public int getTotalEmUso()
        {
            int totalEmUso = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetNumeroEmUso";

            conexao.Open();
            try
            {
                totalEmUso = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch
            {
                totalEmUso = 0;
            }
            conexao.Close();
            conexao.Dispose();

            return totalEmUso;
        }

        public int getTotalInativos()
        {
            int totalInativos = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetNumeroInativos";

            conexao.Open();
            try
            {
                totalInativos = Convert.ToInt32(comando.ExecuteScalar());
            }
            catch
            {
                totalInativos = 0;
            }
            conexao.Close();
            conexao.Dispose();

            return totalInativos;
        }
    }
}
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
            comando.Parameters.AddWithValue("@estado", (int)estado);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Equipamento equipamento = new Equipamento(linha["guidEquipamento"].ToString());

                equipamento.designacao = linha["designacao"].ToString();
                equipamento.descricao = linha["descricao"].ToString();
                equipamento.localizacao = linha["localizacao"].ToString();
                equipamento.numeroSerie = linha["numeroSerie"].ToString();
                equipamento.fabricante = linha["fabricante"].ToString();
                equipamento.modelo = linha["modelo"].ToString();
                equipamento.dataAquisicao = Convert.ToDateTime(linha["dataAquisicao"]);
                equipamento.estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["estado"]);
                equipamento.dthrRegisto = Convert.ToDateTime(linha["dthrRegisto"]);
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
            comando.Parameters.AddWithValue("@guidEquipamento", guidEquipamento2Del);

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
            comando.Parameters.AddWithValue("@guidEquipamento", guidEquipamento);

            adapter.SelectCommand = comando;
            adapter.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                Equipamento equipamento = new Equipamento(linha["guidEquipamento"].ToString());

                equipamento.designacao = linha["designacao"].ToString();
                equipamento.descricao = linha["descricao"].ToString();
                equipamento.localizacao = linha["localizacao"].ToString();
                equipamento.numeroSerie = linha["numeroSerie"].ToString();
                equipamento.fabricante = linha["fabricante"].ToString();
                equipamento.modelo = linha["modelo"].ToString();
                equipamento.dataAquisicao = Convert.ToDateTime(linha["dataAquisicao"]);
                equipamento.estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["estado"]);
                equipamento.dthrRegisto = Convert.ToDateTime(linha["dthrRegisto"]);
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
                equipamento2Save.designacao = equipamentoSent.designacao;
                equipamento2Save.descricao = equipamentoSent.descricao;
                equipamento2Save.localizacao = equipamentoSent.localizacao;
                equipamento2Save.numeroSerie = equipamentoSent.numeroSerie;
                equipamento2Save.fabricante = equipamentoSent.fabricante;
                equipamento2Save.modelo = equipamentoSent.modelo;
                equipamento2Save.dataAquisicao = equipamentoSent.dataAquisicao;
                equipamento2Save.estado = equipamentoSent.estado;

                if (equipamento2Save.guidEquipamento == Guid.Empty.ToString())
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

                comando.Parameters.AddWithValue("@guidEquipamento", equipamento2Save.guidEquipamento);
                comando.Parameters.AddWithValue("@designacao", equipamento2Save.designacao);
                comando.Parameters.AddWithValue("@descricao", equipamento2Save.descricao);
                comando.Parameters.AddWithValue("@localizacao", equipamento2Save.localizacao);
                comando.Parameters.AddWithValue("@numeroSerie", equipamento2Save.numeroSerie);
                comando.Parameters.AddWithValue("@fabricante", equipamento2Save.fabricante);
                comando.Parameters.AddWithValue("@modelo", equipamento2Save.modelo);
                comando.Parameters.AddWithValue("@dataAquisicao", equipamento2Save.dataAquisicao);
                comando.Parameters.AddWithValue("@estado", (int)equipamento2Save.estado);

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
            comando.Parameters.AddWithValue("@guidEquipamento", guidEquipamento);
            comando.Parameters.AddWithValue("@estado", (int)novoEstado);

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

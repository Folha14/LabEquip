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

                equipamento.Designacao = linha["designacao"].ToString();
                equipamento.Descricao = linha["descricao"].ToString();
                equipamento.Localizacao = linha["localizacao"].ToString();
                equipamento.NumeroSerie = linha["numeroSerie"].ToString();
                equipamento.Fabricante = linha["fabricante"].ToString();
                equipamento.Modelo = linha["modelo"].ToString();
                equipamento.DataAquisicao = Convert.ToDateTime(linha["dataAquisicao"]);
                equipamento.Estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["estado"]);
                equipamento.DthrRegisto = Convert.ToDateTime(linha["dthrRegisto"]);
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

                equipamento.Designacao = linha["designacao"].ToString();
                equipamento.Descricao = linha["descricao"].ToString();
                equipamento.Localizacao = linha["localizacao"].ToString();
                equipamento.NumeroSerie = linha["numeroSerie"].ToString();
                equipamento.Fabricante = linha["fabricante"].ToString();
                equipamento.Modelo = linha["modelo"].ToString();
                equipamento.DataAquisicao = Convert.ToDateTime(linha["dataAquisicao"]);
                equipamento.Estado = (Equipamento.EstadoEquipamento)Convert.ToByte(linha["estado"]);
                equipamento.DthrRegisto = Convert.ToDateTime(linha["dthrRegisto"]);
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

                if (instrucaoSQL == "QEquipamento_Insert")
                {
                    comando.Parameters.AddWithValue("@Designacao", equipamento2Save.Designacao);
                    comando.Parameters.AddWithValue("@Descricao", equipamento2Save.Descricao);
                    comando.Parameters.AddWithValue("@Localizacao", equipamento2Save.Localizacao);
                    comando.Parameters.AddWithValue("@NumeroSerie", equipamento2Save.NumeroSerie);
                    comando.Parameters.AddWithValue("@Fabricante", equipamento2Save.Fabricante);
                    comando.Parameters.AddWithValue("@Modelo", equipamento2Save.Modelo);
                    comando.Parameters.AddWithValue("@DataAquisicao", equipamento2Save.DataAquisicao);
                    comando.Parameters.AddWithValue("@Estado", (int)equipamento2Save.Estado);
                }
                else
                {
                    comando.Parameters.AddWithValue("@guidEquipamento", equipamento2Save.GuidEquipamento);
                    comando.Parameters.AddWithValue("@designacao", equipamento2Save.Designacao);
                    comando.Parameters.AddWithValue("@descricao", equipamento2Save.Descricao);
                    comando.Parameters.AddWithValue("@localizacao", equipamento2Save.Localizacao);
                    comando.Parameters.AddWithValue("@numeroSerie", equipamento2Save.NumeroSerie);
                    comando.Parameters.AddWithValue("@fabricante", equipamento2Save.Fabricante);
                    comando.Parameters.AddWithValue("@modelo", equipamento2Save.Modelo);
                    comando.Parameters.AddWithValue("@dataAquisicao", equipamento2Save.DataAquisicao);
                    comando.Parameters.AddWithValue("@estado", (int)equipamento2Save.Estado);
                }

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();
                result = true;
            }
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

        public decimal getMediaIdadeDisponiveis()
        {
            decimal mediaIdade = 0;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QEquipamento_GetMediaIdadeDisponiveis";

            conexao.Open();
            try
            {
                var resultado = comando.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                {
                    mediaIdade = Convert.ToDecimal(resultado);
                }
            }
            catch
            {
                mediaIdade = 0;
            }
            conexao.Close();
            conexao.Dispose();

            return mediaIdade;
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
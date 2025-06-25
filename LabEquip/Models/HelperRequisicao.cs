using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace LabEquip.Models
{
    public class HelperRequisicao : HelperBase
    {

        public List<Requisicao> listByUser(string guidUtilizador)
        {
            DataTable dt = new DataTable();
            List<Requisicao> saida = new List<Requisicao>();
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_ListByUser";
            comando.Parameters.AddWithValue("@IdUtilizador", guidUtilizador);

            telefone.SelectCommand = comando;
            telefone.Fill(dt);

            foreach (DataRow linha in dt.Rows)
            {
                Requisicao requisicao = new Requisicao(linha["GuidRequisicao"].ToString());

                requisicao.IdEquipamento = linha["idEquipamento"].ToString();
                requisicao.IdUtilizador = linha["idUtilizador"].ToString();
                requisicao.DataInicio = Convert.ToDateTime(linha["dataInicio"]);
                requisicao.DataFim = Convert.ToDateTime(linha["dataFim"]);
                requisicao.Motivo = linha["motivo"].ToString();
                requisicao.Estado = (Requisicao.EstadoRequisicao)Convert.ToByte(linha["estado"]);
                saida.Add(requisicao);
            }
            return saida;
        }

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

        public Requisicao? get(string guidRequisicao)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_Get";
            comando.Parameters.AddWithValue("@GuidRequisicao", guidRequisicao);

            telefone.SelectCommand = comando;
            telefone.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                DataRow linha = dt.Rows[0];
                Requisicao requisicao = new Requisicao("" + linha["guidRequisicao"].ToString());
                requisicao.IdEquipamento = "" + linha["idEquipamento"].ToString();
                requisicao.IdUtilizador = "" + linha["idUtilizador"].ToString();
                requisicao.DataInicio = Convert.ToDateTime(linha["dataInicio"]);
                requisicao.DataFim = Convert.ToDateTime(linha["dataFim"]);
                requisicao.Motivo = "" + linha["motivo"].ToString();
                requisicao.Estado = (Requisicao.EstadoRequisicao)Convert.ToByte(linha["estado"]);
                return requisicao;
            }
            return null;
        }

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
                requisicao2Save.IdEquipamento = requisicaoSent.IdEquipamento;
                requisicao2Save.IdUtilizador = requisicaoSent.IdUtilizador;
                requisicao2Save.DataInicio = requisicaoSent.DataInicio;
                requisicao2Save.DataFim = requisicaoSent.DataFim;
                requisicao2Save.Motivo = requisicaoSent.Motivo;
                requisicao2Save.Estado = requisicaoSent.Estado;
                if (requisicao2Save.GuidRequisicao == Guid.Empty.ToString())
                {
                    instrucaoSQL = "QRequisicao_Insert";
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

                comando.Parameters.AddWithValue("@IdEquipamento", requisicao2Save.IdEquipamento);
                comando.Parameters.AddWithValue("@IdUtilizador", requisicao2Save.IdUtilizador);
                comando.Parameters.AddWithValue("@DataInicio", requisicao2Save.DataInicio);
                comando.Parameters.AddWithValue("@DataFim", requisicao2Save.DataFim);
                comando.Parameters.AddWithValue("@Motivo", requisicao2Save.Motivo);
                comando.Parameters.AddWithValue("@Estado", requisicao2Save.Estado);
                comando.Parameters.AddWithValue("@GuidRequisicao", requisicao2Save.GuidRequisicao);

                conexao.Open();
                comando.ExecuteNonQuery();
                conexao.Close();
                conexao.Dispose();
                result = true;
            }
            return result;
        }

        public Boolean finalizarRequisicao(string idEquipamento, string idUtilizador)
        {
            Boolean result = false;
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(ConetorHerdado);
            comando.CommandType = CommandType.StoredProcedure;
            comando.Connection = conexao;
            comando.CommandText = "QRequisicao_Finalizar";
            comando.Parameters.AddWithValue("@IdEquipamento", idEquipamento);
            comando.Parameters.AddWithValue("@IdUtilizador", idUtilizador);
            conexao.Open();
            int rowsAffected = comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
            result = rowsAffected > 0;
            return result;
        }
    }
}
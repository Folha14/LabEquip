    namespace LabEquip.Models
{
    public class Requisicao
    {

        public enum EstadoRequisicao
        {
            Pendente,
            Aprovada,
            Rejeitada,
            Cancelada,
            Ativa,
            Concluida
        }

        private Guid _guidRequisicao;

        public string GuidRequisicao
        {
            get { return _guidRequisicao.ToString(); }
        }

        public string IdEquipamento { get; set; }
        public string IdUtilizador { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Motivo { get; set; }
        public EstadoRequisicao Estado { get; set; }

        public Requisicao()
        {
            _guidRequisicao = Guid.Empty;
            IdEquipamento = "";
            IdUtilizador = "";
            Motivo = "";
            Estado = EstadoRequisicao.Pendente;
            DataInicio = DateTime.Now;
            DataFim = DateTime.Now.AddDays(1);
        }
        public Requisicao(string guidRequisicao)
        {
            Guid.TryParse(guidRequisicao, out _guidRequisicao);
            IdEquipamento = "";
            IdUtilizador = "";
            Motivo = "";
            Estado = EstadoRequisicao.Pendente;
            DataInicio = DateTime.Now;
            DataFim = DateTime.Now.AddDays(1);
        }
    }
}
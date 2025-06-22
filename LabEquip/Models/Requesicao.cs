using System.ComponentModel.DataAnnotations;

namespace LabEquip.Models
{
    public class Requisicao
    {
        public enum EstadoRequisicao
        {
            Pendente = 0,
            Aprovada = 1,
            Rejeitada = 2,
            Cancelada = 3,
            Concluida = 4
        }

        private Guid _guidRequisicao;

        public string GuidRequisicao
        {
            get { return _guidRequisicao.ToString(); }
        }

        public string GuidEquipamento { get; set; }
        public string GuidUtilizador { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "A data de fim é obrigatória")]
        public DateTime DataFim { get; set; }

        [StringLength(500, ErrorMessage = "O motivo não pode ter mais de 500 caracteres")]
        public string Motivo { get; set; }

        public EstadoRequisicao Estado { get; set; }

        public DateTime DataRequisicao { get; set; }

        public DateTime? DataAprovacao { get; set; }

        public string? GuidAprovador { get; set; }

        [StringLength(500, ErrorMessage = "As observações não podem ter mais de 500 caracteres")]
        public string? Observacoes { get; set; }

        // Propriedades para joins
        public string? NomeEquipamento { get; set; }
        public string? NomeUtilizador { get; set; }
        public string? EmailUtilizador { get; set; }
        public string? NomeAprovador { get; set; }

        public Requisicao()
        {
            _guidRequisicao = Guid.Empty;
            GuidEquipamento = "";
            GuidUtilizador = "";
            Motivo = "";
            Estado = EstadoRequisicao.Pendente;
            DataRequisicao = DateTime.Now;
            DataInicio = DateTime.Now;
            DataFim = DateTime.Now.AddDays(1);
        }

        public Requisicao(string guidRequisicao)
        {
            Guid.TryParse(guidRequisicao, out _guidRequisicao);
            GuidEquipamento = "";
            GuidUtilizador = "";
            Motivo = "";
            Estado = EstadoRequisicao.Pendente;
            DataRequisicao = DateTime.Now;
            DataInicio = DateTime.Now;
            DataFim = DateTime.Now.AddDays(1);
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace LabEquip.Models
{
    public class Equipamento
    {
        public enum EstadoEquipamento
        {
            Disponivel = 0,
            EmUso = 1,
            Manutencao = 2,
            Inativo = 3
        }

        private Guid _guidEquipamento;

        public string GuidEquipamento
        {
            get { return _guidEquipamento.ToString(); }
        }

        [Required(ErrorMessage = "A designação é obrigatória")]
        [StringLength(100, ErrorMessage = "A designação não pode ter mais de 100 caracteres")]
        public string Designacao { get; set; }

        [StringLength(500, ErrorMessage = "A descrição não pode ter mais de 500 caracteres")]
        public string Descricao { get; set; }

        [StringLength(50, ErrorMessage = "A localização não pode ter mais de 50 caracteres")]
        public string Localizacao { get; set; }

        [StringLength(50, ErrorMessage = "O número de série não pode ter mais de 50 caracteres")]
        public string NumeroSerie { get; set; }

        [StringLength(50, ErrorMessage = "O fabricante não pode ter mais de 50 caracteres")]
        public string Fabricante { get; set; }

        [StringLength(50, ErrorMessage = "O modelo não pode ter mais de 50 caracteres")]
        public string Modelo { get; set; }

        public DateTime DataAquisicao { get; set; }

        public EstadoEquipamento Estado { get; set; }

        public string? ImagemPath { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public Equipamento()
        {
            _guidEquipamento = Guid.Empty;
            Designacao = "";
            Descricao = "";
            Localizacao = "";
            NumeroSerie = "";
            Fabricante = "";
            Modelo = "";
            Estado = EstadoEquipamento.Disponivel;
            DataAquisicao = DateTime.Now;
            DataCriacao = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }

        public Equipamento(string guidEquipamento)
        {
            Guid.TryParse(guidEquipamento, out _guidEquipamento);
            Designacao = "";
            Descricao = "";
            Localizacao = "";
            NumeroSerie = "";
            Fabricante = "";
            Modelo = "";
            Estado = EstadoEquipamento.Disponivel;
            DataAquisicao = DateTime.Now;
            DataCriacao = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }
    }
}
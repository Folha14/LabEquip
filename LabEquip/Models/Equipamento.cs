using System;

namespace LabEquip.Models
{
    public class Equipamento
    {
        public enum EstadoEquipamento
        {
            Disponivel,
            EmUso,
            Manutencao,
            Inativo,
            Todos = 99
        }

        private Guid _guidEquipamento;

        public string guidEquipamento
        {
            get { return _guidEquipamento.ToString(); }
        }

        public string designacao { get; set; }
        public string descricao { get; set; }
        public string localizacao { get; set; }
        public string numeroSerie { get; set; }
        public string fabricante { get; set; }
        public string modelo { get; set; }
        public DateTime dataAquisicao { get; set; }
        public EstadoEquipamento estado { get; set; }
        public DateTime dthrRegisto { get; set; }

        public Equipamento()
        {
            _guidEquipamento = Guid.Empty;
            designacao = "";
            descricao = "";
            localizacao = "";
            numeroSerie = "";
            fabricante = "";
            modelo = "";
            estado = EstadoEquipamento.Disponivel;
            dataAquisicao = DateTime.Now;
            dthrRegisto = DateTime.Now;
        }

        public Equipamento(string guidEquipamento)
        {
            Guid.TryParse(guidEquipamento, out _guidEquipamento);
            designacao = "";
            descricao = "";
            localizacao = "";
            numeroSerie = "";
            fabricante = "";
            modelo = "";
            estado = EstadoEquipamento.Disponivel;
            dataAquisicao = DateTime.Now;
            dthrRegisto = DateTime.Now;
        }
    }
}
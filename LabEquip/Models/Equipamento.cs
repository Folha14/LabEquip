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

        public string GuidEquipamento
        {
            get { return _guidEquipamento.ToString(); }
        }

        public string Designacao { get; set; }
        public string Descricao { get; set; }
        public string Localizacao { get; set; }
        public string NumeroSerie { get; set; }
        public string Fabricante { get; set; }
        public string Modelo { get; set; }
        public DateTime DataAquisicao { get; set; }
        public EstadoEquipamento Estado { get; set; }

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
        }
    }
}
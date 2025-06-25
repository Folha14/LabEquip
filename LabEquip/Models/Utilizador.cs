using System.ComponentModel.DataAnnotations;

namespace LabEquip.Models
{
    public class Utilizador
    {
        private Guid _guidUtilizador;

        public string GuidUtilizador
        {
            get { return _guidUtilizador.ToString(); }
        }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Senha { get; set; }

        public int NivelAcesso { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public Utilizador()
        {
            _guidUtilizador = Guid.Empty;
            Nome = "";
            Email = "";
            Senha = "";
            NivelAcesso = 1;
            Ativo = true;
            DataCriacao = DateTime.Now;
        }

        public Utilizador(string guidUtilizador)
        {
            Guid.TryParse(guidUtilizador, out _guidUtilizador);
            Nome = "";
            Email = "";
            Senha = "";
            NivelAcesso = 1;
            Ativo = true;
            DataCriacao = DateTime.Now;
        }
    }
}
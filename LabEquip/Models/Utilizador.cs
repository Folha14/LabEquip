using System;

namespace LabEquip.Models
{
    public class Utilizador
    {
        private Guid _guidUtilizador;

        public string GuidUtilizador
        {
            get { return _guidUtilizador.ToString(); }
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int NivelAcesso { get; set; }
        public bool Ativo { get; set; }
        public DateTime DthrRegisto { get; set; }

        public Utilizador()
        {
            _guidUtilizador = Guid.Empty;
            Nome = "";
            Email = "";
            Senha = "";
            NivelAcesso = 1;
            Ativo = true;
            DthrRegisto = DateTime.Now;
        }

        public Utilizador(string guidUtilizador)
        {
            Guid.TryParse(guidUtilizador, out _guidUtilizador);
            Nome = "";
            Email = "";
            Senha = "";
            NivelAcesso = 1;
            Ativo = true;
            DthrRegisto = DateTime.Now;
        }
    }
}

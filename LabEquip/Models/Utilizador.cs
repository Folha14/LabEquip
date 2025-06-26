using System;

namespace LabEquip.Models
{
    public class Utilizador
    {
        private Guid _guidUtilizador;

        public string guidUtilizador
        {
            get { return _guidUtilizador.ToString(); }
        }

        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public int nivelAcesso { get; set; }
        public bool ativo { get; set; }
        public DateTime dthrRegisto { get; set; }

        public Utilizador()
        {
            _guidUtilizador = Guid.Empty;
            nome = "";
            email = "";
            senha = "";
            nivelAcesso = 1;
            ativo = true;
            dthrRegisto = DateTime.Now;
        }

        public Utilizador(string guidUtilizador)
        {
            Guid.TryParse(guidUtilizador, out _guidUtilizador);
            nome = "";
            email = "";
            senha = "";
            nivelAcesso = 1;
            ativo = true;
            dthrRegisto = DateTime.Now;
        }
    }
}